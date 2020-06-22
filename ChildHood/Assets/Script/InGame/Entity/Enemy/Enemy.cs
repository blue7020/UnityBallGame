﻿using System.Collections;
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
    private Sprite[] mMonsterSpriteArr;

    public Rigidbody2D mRB2D;

    [SerializeField]
    private Transform mHPBarPos;
    [SerializeField]
    private EnemySkill mEnemySkill;
    [SerializeField]
    private EnemyAttackArea mAttackArea;
    private Player mPlayer;
    private GaugeBar mHPBar;

    private bool AttackOn;
    private bool HPBarOn;
    public Coroutine mCoroutine;

    public Animator mAnim;
    [SerializeField]
    public float mCurrentHP;
    public float mMaxHP;

    [SerializeField]
    public MonsterStat[] mInfoArr;

    public MonsterStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.MONSTER_STAT);
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mMaxHP = mInfoArr[mID].Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
    }
    private void Start()
    {
        State = eMonsterState.Idle;
        mDelayCount = 0;
        StartCoroutine(StateMachine());
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (HPBarOn == true)
        {
            mHPBar.transform.position = mHPBarPos.position;
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
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
                        transform.position = transform.position;
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
                    if (mDelayCount >= 10)
                    {
                        gameObject.SetActive(false);
                    }
                    else if (mDelayCount >= 3)
                    {
                        mAnim.SetBool(AnimHash.Enemy_Walk, false);
                        mAnim.SetBool(AnimHash.Enemy_Attack, false);
                        mAnim.SetBool(AnimHash.Enemy_Death, true);
                        //TODO 사망 시 색상 변경->회색
                        if (eType == eEnemyType.Boss)
                        {
                            PortalTrigger.Instance.BossDeath = true;
                        }
                        mDelayCount++;
                    }
                    else
                    {
                        AttackOn = false;
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
        mCurrentHP -= damage;
        //TODO 타격 시 색상 변경 0.5초간 빨강
        if (mHPBar == null)
        {
            mHPBar = GaugeBarPool.Instance.GetFromPool();
        }
        if (mCurrentHP <= 0)
        {
            mHPBar.gameObject.SetActive(false);
            mHPBar = null;
            HPBarOn = false;
            //
            DropGold mGold = GoldPool.Instance.GetFromPool();
            mGold.transform.position = transform.position;
            mGold.GoldDrop(mGold, mInfoArr[mID].Gold);
            mAnim.SetBool(AnimHash.Enemy_Attack, false);
            //
            if (Player.Instance.NowEnemyCount>0)
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

    public IEnumerator Attack() 
    {
        if (AttackOn == true)
        {
            AttackOn = false;
            WaitForSeconds AtkSpd = new WaitForSeconds(mInfoArr[mID].AtkSpd);

            Player.Instance.Hit(mInfoArr[mID].Atk);

            yield return AtkSpd;
            AttackOn = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (State != eMonsterState.Die)
            {
                AttackOn = true;
            }
        }
    }


    public IEnumerator SkillCast()
    {
        if (State == eMonsterState.Traking)
        {
            WaitForSeconds cool = new WaitForSeconds(mInfoArr[mID].AtkSpd);
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
            mRB2D.velocity = dir.normalized * mInfoArr[mID].Spd;
            yield return one;

        }

    }

}
