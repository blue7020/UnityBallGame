﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InformationLoader
{
    public eMonsterState State;
    public eEnemyType eType;
    public int mDelayCount;

    public int mID;
    public GameObject[] mSprite;
    public Rigidbody2D mRB2D;

    public Transform mHPBarPos;
    public EnemySkill mEnemySkill;
    public EnemyAttackArea mAttackArea;

    public GaugeBar mHPBar;

    private bool AttackOn;
    private bool AttackCheck;
    public Coroutine mCoroutine;
    public bool Spawned;
    public bool IsMimic;

    public Animator mAnim;
    public float mCurrentHP;
    public float mMaxHP;

    public MonsterStat mStats;

    private void Awake()
    {
        mStats =EnemyController.Instance.mInfoArr[mID];
        Spawned = false;
    }
    private void Start()
    {
        mMaxHP = mStats.Hp + ((GameController.Instance.StageHP + GameController.Instance.MapLevel) * GameController.Instance.Level);
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        AttackOn = false;
        AttackCheck = true;
        if (IsMimic==false)
        {
            State = eMonsterState.Spawning;
        }
        else
        {
            State = eMonsterState.Idle;
        }
        mDelayCount = 0;
        StartCoroutine(StateMachine());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mCurrentHP < 1)
        {
            State = eMonsterState.Die;
        }
    }

    public void EnemySpawned()
    {
        mAnim.SetBool(AnimHash.Enemy_Spawn, true);
        mSprite[0].gameObject.SetActive(false);
        mSprite[1].gameObject.SetActive(true);
        State = eMonsterState.Idle;
        Spawned = true;
    }

    public IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.1f);
        while (true)
        {
            switch (State)
            {
                case eMonsterState.Spawning:
                    State = eMonsterState.Traking;
                    break;
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
                case eMonsterState.Skill:
                    if (mDelayCount >= 20)
                    {
                        mDelayCount = 0;
                        State = eMonsterState.Traking;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Die:
                    AttackOn = false;
                    mAnim.SetBool(AnimHash.Enemy_Walk, false);
                    mAnim.SetBool(AnimHash.Enemy_Attack, false);
                    mAnim.SetBool(AnimHash.Enemy_Death, true);
                    mSprite[1].GetComponent<SpriteRenderer>().color = Color.grey;
                    if (mDelayCount >= 6)
                    {
                        if (Player.Instance.CurrentRoom.EnemyCount > 0)
                        {
                            Player.Instance.CurrentRoom.EnemyCount--;
                        }
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
        if (Spawned == true)
        {
            StartCoroutine(HitAnimation());
            mCurrentHP -= damage;

            if (mHPBar == null)
            {
                mHPBar = GaugeBarPool.Instance.GetFromPool();
                mHPBar.mEnemy = this;
            }
            if (mCurrentHP <= 0)
            {
                if (mStats.Gold > 0)
                {
                    DropGold mGold = GoldPool.Instance.GetFromPool();
                    mGold.transform.SetParent(Player.Instance.CurrentRoom.transform);
                    mGold.transform.position = transform.position;
                    mGold.GoldDrop(mGold, mStats.Gold);
                }
                mEnemySkill.DieSkill();
                mHPBar.CloseGauge();
            }
            else
            {
                mHPBar.gameObject.SetActive(true);
                mHPBar.SetGauge(mCurrentHP, mMaxHP);
                mHPBar.transform.position = mHPBarPos.position;
            }
        }
        

    }

    private IEnumerator HitAnimation()
    {
        WaitForSeconds Time = new WaitForSeconds(0.3f);
        mSprite[1].GetComponent<SpriteRenderer>().color = Color.red;
        yield return Time;
        mSprite[1].GetComponent<SpriteRenderer>().color = Color.white;
        StopCoroutine(HitAnimation());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")&&Spawned==true)
        {
            if (AttackCheck == true)
            {
                AttackOn = true;
                AttackCheck = false;
            }
            StartCoroutine(Attack());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Spawned == true)
        {
            AttackOn = false;
            AttackCheck = true;
        }
    }

    public IEnumerator Attack()
    {
        WaitForSeconds Cool = new WaitForSeconds(mStats.AtkSpd);
        if (AttackOn == true)
        {
            AttackOn = false;
            Player.Instance.Hit(mStats.Atk);
        }
        yield return Cool;
        AttackOn = true;
    }


    public IEnumerator SkillCast()
    {
        if (State == eMonsterState.Traking)
        {
            State = eMonsterState.Skill;
            WaitForSeconds cool = new WaitForSeconds(mStats.AtkSpd);
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
            mRB2D.velocity = dir.normalized * mStats.Spd;
            yield return one;

        }

    }

}
