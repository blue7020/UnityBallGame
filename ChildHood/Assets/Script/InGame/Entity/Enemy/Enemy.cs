﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InformationLoader
{
    //TODO 몬스터 생성 시 해당 ID에 맞는 몬스터의 정보와 스프라이트를 출력하게끔
    public eMonsterState State;
    [SerializeField]
    public eEnemyType eType;
    public int mDelayCount;

    [SerializeField]
    public int mID;
    [SerializeField]
    private GameObject mSprite;

    public Rigidbody2D mRB2D;

    [SerializeField]
    private Transform mHPBarPos;
    [SerializeField]
    private EnemySkill mEnemySkill;
    [SerializeField]
    private EnemyAttackArea mAttackArea;
    private GaugeBar mHPBar;

    private bool AttackOn;
    private bool AttackCheck;
    private bool HPBarOn;
    public Coroutine mCoroutine;

    public Animator mAnim;
    [SerializeField]
    public float mCurrentHP;
    public float mMaxHP;

    public MonsterStat Stats;

    private void Awake()
    {
        Stats=EnemyController.Instance.mInfoArr[mID];
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mMaxHP = Stats.Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
    }
    private void Start()
    {
        AttackOn = false;
        AttackCheck = true;
        State = eMonsterState.Idle;
        mDelayCount = 0;
        StartCoroutine(StateMachine());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HPBarOn == true)
        {
            mHPBar.transform.position = mHPBarPos.position;
        }
        if (mCurrentHP < 1)
        {
            State = eMonsterState.Die;
        }
    }

    public IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.1f);
        while (true)
        {
            switch (State)
            {
                case eMonsterState.Idle:
                    if (mDelayCount >= 20)
                    {
                        mAnim.SetBool(AnimHash.Enemy_Walk, false);
                        mAnim.SetBool(AnimHash.Enemy_Attack, false);
                        mRB2D.velocity = Vector3.zero;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Traking:
                    if (mDelayCount >= 20)
                    {
                        mAnim.SetBool(AnimHash.Enemy_Walk, false);

                        mDelayCount = 0;
                    }
                    else
                    {
                        mAnim.SetBool(AnimHash.Enemy_Walk, true);
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Die:
                    AttackOn = false;
                    mAnim.SetBool(AnimHash.Enemy_Walk, false);
                    mAnim.SetBool(AnimHash.Enemy_Attack, false);
                    mAnim.SetBool(AnimHash.Enemy_Death, true);
                    mSprite.GetComponent<SpriteRenderer>().color = Color.grey;
                    if (mDelayCount >= 6)
                    {
                        if (eType == eEnemyType.Boss)
                        {
                            PortalTrigger.Instance.BossDeath();
                        }
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                    
                default:
                    Debug.LogError("Wrong State");
                    break;
            }
            yield return pointOne;
        }

    }

    public void Hit(float damage)
    {
        StartCoroutine(HitAnimation());
        mCurrentHP -= damage;

        if (mHPBar == null)
        {
            mHPBar = GaugeBarPool.Instance.GetFromPool();
        }
        if (mCurrentHP <= 0)
        {
            mHPBar.gameObject.SetActive(false);
            mHPBar = null;
            HPBarOn = false;

            DropGold mGold = GoldPool.Instance.GetFromPool();
            mGold.transform.position = transform.position;
            mGold.GoldDrop(mGold, Stats.Gold);
            mEnemySkill.DieSkill();
            if (Player.Instance.NowEnemyCount > 0)
            {
                Player.Instance.NowEnemyCount--;
            }
        }
        else
        {
            mHPBar.gameObject.SetActive(true);
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
            mHPBar.transform.position = mHPBarPos.position;
            HPBarOn = true;
        }

    }

    private IEnumerator HitAnimation()
    {
        WaitForSeconds Time = new WaitForSeconds(0.3f);
        mSprite.GetComponent<SpriteRenderer>().color = Color.red;
        yield return Time;
        mSprite.GetComponent<SpriteRenderer>().color = Color.white;
        StopCoroutine(HitAnimation());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (AttackCheck == true)
            {
                AttackOn = true;
            }
            StartCoroutine(Attack());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AttackOn = false;
            AttackCheck = true;
        }
    }

    public IEnumerator Attack()
    {
        WaitForSeconds Cool = new WaitForSeconds(Stats.AtkSpd);
        if (AttackOn == true)
        {
            AttackOn = false;
            Player.Instance.Hit(Stats.Atk);
        }
        yield return Cool;
        AttackOn = true;
    }


    public IEnumerator SkillCast()
    {
        if (State == eMonsterState.Traking)
        {
            WaitForSeconds cool = new WaitForSeconds(Stats.AtkSpd);
            mAnim.SetBool(AnimHash.Enemy_Walk, false);
            mAnim.SetBool(AnimHash.Enemy_Attack, true);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            mEnemySkill.Skill();
            yield return cool;

        }
        mCoroutine = null;
    }

    public IEnumerator MoveToPlayer()
    {
        if (State == eMonsterState.Traking)
        {
            WaitForSeconds one = new WaitForSeconds(0.1f);
            mAnim.SetBool(AnimHash.Enemy_Walk, true);
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            mRB2D.velocity = dir.normalized * Stats.Spd;
            yield return one;

        }

    }

}
