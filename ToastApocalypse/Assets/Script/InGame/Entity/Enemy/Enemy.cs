using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InformationLoader
{
    public eMonsterState mState;
    public eEnemyType eType;
    public int mDelayCount;

    public int mID;
    public GameObject[] mSprite;
    public Rigidbody2D mRB2D;
    public Player mTarget;
    public bool Runaway;
    public bool HasBarrier;

    public Transform mHPBarPos;
    public EnemySkill mEnemySkill;
    public EnemyAttackArea mAttackArea;
    public TrackingRange mTrackingRange;

    public GaugeBar mHPBar;
    public GameObject CCState;

    public bool Nodamage;
    public bool Stun;
    public bool IsTraking;
    public float SpeedAmount;

    public bool AttackOn;
    public bool AttackCheck;
    public Coroutine mCoroutine;
    public bool Spawned;
    public bool IsMimic;
    public Animator mAnim;
    public float mCurrentHP;
    public float mMaxHP;

    public MonsterStat mStats;

    private void OnEnable()
    {
        if (GameController.Instance.Level!=5)
        {
            Player.Instance.CurrentRoom.EnemyCount++;
        }
        gameObject.layer = 8;
    }

    private void Awake()
    {
        mStats = EnemyController.Instance.mInfoArr[mID];
        Spawned = false;
        HasBarrier = false;
        IsTraking = true;
        AttackOn = false;
        Nodamage = true;
        AttackCheck = true;
        Stun = false;
                mMaxHP = mStats.Hp + ((GameController.Instance.StageHP + GameController.Instance.MapLevel) * GameController.Instance.Level);
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
    }
    private void Start()
    {
        if (IsMimic == false)
        {
            mState = eMonsterState.Spawning;
            Nodamage = false;
        }
        else
        {
            mState = eMonsterState.Idle;
            gameObject.layer = 0;
            Spawned = true;
            AttackCheck = false;
            Nodamage = false;
            mTrackingRange.gameObject.SetActive(true);
        }
        mDelayCount = 0;
        StartCoroutine(StateMachine());

    }

    public void EnemySpawned()
    {
        mAnim.SetBool(AnimHash.Enemy_Spawn, true);
        mSprite[0].gameObject.SetActive(false);
        mSprite[1].gameObject.SetActive(true);
        gameObject.layer = 0;
        Spawned = true;
        if (GameController.Instance.MapLevel == 5)
        {
            mEnemySkill.IceBarrier();
        }
        AttackCheck = false;
        mState = eMonsterState.Idle;
        Nodamage = false;
        mTrackingRange.gameObject.SetActive(true);
        StartCoroutine(SkillCast());
    }

    public IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.1f);
        while (true)
        {
            switch (mState)
            {
                case eMonsterState.Spawning:
                    mState = eMonsterState.Idle;
                    break;
                case eMonsterState.Idle:
                    if (mDelayCount >= 20)
                    {
                        if (Stun == false)
                        {
                            mAnim.SetBool(AnimHash.Enemy_Walk, false);
                            mAnim.SetBool(AnimHash.Enemy_Attack, false);
                            mRB2D.velocity = Vector3.zero;
                        }
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
                        if (Stun == false)
                        {
                            mAnim.SetBool(AnimHash.Enemy_Walk, true);
                        }
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Die:
                    if (mDelayCount >= 20)
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
                        mRB2D.velocity = Vector3.zero;
                        gameObject.layer = 8;
                        mSprite[1].GetComponent<SpriteRenderer>().color = Color.grey;
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
        if (Spawned == true)
        {
            if (HasBarrier==true)
            {
                HasBarrier = false;
                mEnemySkill.mBarrier.SetActive(false);
                mEnemySkill.mBarrier = null;            }
            else
            {
                if (Nodamage == false)
                {
                    StartCoroutine(HitAnimation());
                    mCurrentHP -= damage;

                    if (mHPBar == null)
                    {
                        mHPBar = GaugeBarPool.Instance.GetFromPool();
                        mHPBar.mEnemy = this;
                    }
                    if (mCurrentHP > 0)
                    {
                        mHPBar.gameObject.SetActive(true);
                        mHPBar.SetGauge(mCurrentHP, mMaxHP);
                        mHPBar.transform.position = mHPBarPos.position;
                    }
                    else if (mCurrentHP <= 0)
                    {
                        mEnemySkill.DieSkill();
                        mAnim.SetBool(AnimHash.Enemy_Walk, false);
                        mAnim.SetBool(AnimHash.Enemy_Attack, false);
                        mAnim.SetBool(AnimHash.Enemy_Death, true);
                        mRB2D.velocity = Vector3.zero;
                        if (mStats.Gold > 0)
                        {
                            DropGold mGold = GoldPool.Instance.GetFromPool();
                            mGold.transform.SetParent(Player.Instance.CurrentRoom.transform);
                            mGold.transform.position = transform.position;
                            mGold.GoldDrop((int)(mStats.Gold * (1 + Player.Instance.mGoldBonus)));
                        }
                        GameController.Instance.SyrupInStage += mStats.Syrup;
                        mHPBar.CloseGauge();
                        mState = eMonsterState.Die;
                    }
                }
            }

        }
    }

    private IEnumerator HitAnimation()
    {
        WaitForSeconds Time = new WaitForSeconds(0.3f);
        Nodamage = true;
        mSprite[1].GetComponent<SpriteRenderer>().color = Color.red;
        yield return Time;
        Nodamage = false;
        mSprite[1].GetComponent<SpriteRenderer>().color = Color.white;
        StopCoroutine(HitAnimation());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Spawned == true)
        {
            AttackCheck = true;

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Spawned == true)
        {
            AttackCheck = false;
        }
    }

    public void Attack()
    {
        if (AttackCheck == true)
        {
            mTarget.Hit(mStats.Atk);
        }
    }
    public void SkillObjAttack()
    {
        if (mEnemySkill.mEnemyObj.TargetSetting == true)
        {
            Player.Instance.Hit(mStats.Atk);
            Player.Instance.DoEffect(6, 0.75f);
        }
    }


    public IEnumerator SkillCast()
    {
        if (mState == eMonsterState.Traking)
        {
            WaitForSeconds cool = new WaitForSeconds(mStats.AtkSpd);
            mAnim.SetBool(AnimHash.Enemy_Walk, false);
            mEnemySkill.Skill();
            yield return cool;
            mCoroutine = null;
        }
    }

    public IEnumerator MoveToPlayer()
    {
        if (mState == eMonsterState.Traking && Spawned == true)
        {
            if (Stun == false&& IsTraking==true)
            {
                WaitForSeconds one = new WaitForSeconds(0.1f);
                mAnim.SetBool(AnimHash.Enemy_Walk, true);
                if (Player.Instance.transform.position.x - transform.position.x > 0)//- 좌
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else//+ 우
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (!Runaway)
                {
                    Vector3 dir = mTarget.transform.position - transform.position;
                    mRB2D.velocity = dir.normalized * (mStats.Spd * (1 + SpeedAmount));
                }
                else
                {
                    Vector3 dir = mTarget.transform.position - transform.position;
                    mRB2D.velocity = -dir.normalized * (mStats.Spd * (1 + SpeedAmount));
                }
                yield return one;
            }
        }
    }


    public IEnumerator SpeedBuff(float duration, float value)
    {
        WaitForSeconds dura = new WaitForSeconds(duration);
        SpeedAmount += value;
        yield return dura;
        SpeedAmount -= value;
    }

    public IEnumerator SpeedNurf(float duration, float value)
    {
        WaitForSeconds dura = new WaitForSeconds(duration);
        SpeedAmount -= value;
        yield return dura;
        SpeedAmount += value;
    }

    public IEnumerator Stuned(float duration)
    {
        WaitForSeconds dura = new WaitForSeconds(duration);
        Stun = true;
        mRB2D.velocity = Vector3.zero;
        mAnim.SetBool(AnimHash.Enemy_Walk, false);
        CCState.SetActive(true);
        yield return dura;
        Stun = false;
        CCState.SetActive(false);
    }
}
