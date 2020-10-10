using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : InformationLoader
{
    public eMonsterState mState;
    public eEnemyType eType;
    public int mDelayCount;

    public int mID;
    public Sprite UISprite;
    public GameObject[] mSprite;
    public Rigidbody2D mRB2D;
    public Player mTarget;

    public Transform mHPBarPos;
    public EnemySkill mEnemySkill;
    public EnemyAttackArea mAttackArea;
    public TrackingRange mTrackingRange;

    public GaugeBar mHPBar;
    public GameObject CCState;

    public bool Nodamage, Stun, isFire,IsTraking;
    public bool AttackOn, AttackCheck, Spawned, Runaway, HasBarrier;
    public Coroutine mCoroutine;
    public Animator mAnim;
    public float mCurrentHP, mMaxHP, SpeedAmount;
    public Room CurrentRoom;

    public MonsterStat mStats;
    public Text mCrititcalText;

    private void OnEnable()
    {
        if (GameController.Instance.IsTutorial == false&&GameController.Instance.StageLevel!=5)
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
        isFire = false;
        mMaxHP = mStats.Hp + ((GameController.Instance.StageHP + GameSetting.Instance.NowStage) * GameController.Instance.StageLevel);
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        mDelayCount = 0;
        if (eType==eEnemyType.Boss)
        {
            CurrentRoom = PortalTrigger.Instance.room;
        }
        else if (eType == eEnemyType.Mimic)
        {
            CurrentRoom = MimicSpawner.Instance.room;
        }
    }
    private void Start()
    {
        if (eType == eEnemyType.Mimic)
        {
            mState = eMonsterState.Spawning;
            EnemySpawned();
        }
        else
        {
            Stun = true;
            mState = eMonsterState.Idle;
            gameObject.layer = 0;
            Spawned = true;
            AttackCheck = false;
            Nodamage = false;
            mTrackingRange.gameObject.SetActive(true);
        }
        StartCoroutine(StateMachine());
    }

    public void EnemySpawned()
    {
        if (eType != eEnemyType.Mimic)
        {
            mAnim.SetBool(AnimHash.Enemy_Spawn, true);
            mSprite[0].gameObject.SetActive(false);
            mSprite[1].gameObject.SetActive(true);
        }
        gameObject.layer = 0;
        Spawned = true;
        if (GameSetting.Instance.NowStage == 5)
        {
            mEnemySkill.IceBarrier();
        }
        AttackCheck = false;
        mState = eMonsterState.Idle;
        Stun = false;
        Nodamage = false;
        mTrackingRange.gameObject.SetActive(true);
        StartCoroutine(SkillCast());
    }

    public IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(0.11f);
        while (true)
        {
            if (GameController.Instance.pause == false)
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
                        Death();
                        if (Player.Instance.CurrentRoom.EnemyCount > 0)
                        {
                            Player.Instance.CurrentRoom.EnemyCount--;
                        }
                        if (eType == eEnemyType.Boss)
                        {
                            PortalTrigger.Instance.BossDeath();
                        }
                        if (mEnemySkill.BulletTrash!=null)
                        {
                            mEnemySkill.BulletTrash.SetActive(false);
                        }
                        gameObject.SetActive(false);
                        break;

                    default:
                        Debug.LogError("Wrong State");
                        break;
                }
            }
            yield return pointOne;
        }

    }

    private void Death()
    {
        Stun = true;
        mRB2D.velocity = Vector3.zero;
        SoundController.Instance.SESound(5);
        AttackOn = false;
        gameObject.layer = 8;
        mSprite[1].GetComponent<SpriteRenderer>().color = Color.grey;
        mAnim.SetBool(AnimHash.Enemy_Walk, false);
        mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mAnim.SetBool(AnimHash.Enemy_Death, true);
        mEnemySkill.DieSkill();
        Player.Instance.mTotalKillCount++;
        if (mStats.Gold > 0)
        {
            DropGold mGold = GoldPool.Instance.GetFromPool();
            mGold.transform.SetParent(Player.Instance.CurrentRoom.transform);
            mGold.transform.position = transform.position;
            mGold.GoldDrop((int)(mStats.Gold * (1 + Player.Instance.mGoldBonus)));
        }
        Player.Instance.IsEnemyDeathPassiveSkill();
        GameController.Instance.SyrupInStage += mStats.Syrup;
    }

    public void Hit(float damage,int weapontype,bool iscrit)
    {
        if (Spawned == true)
        {
            if (HasBarrier == true)
            {
                SoundController.Instance.SESound(22);
                HasBarrier = false;
                mEnemySkill.mBarrier.SetActive(false);
                mEnemySkill.mBarrier = null;
            }
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
                        mHPBar.CloseGauge();
                        mState = eMonsterState.Die;
                    }
                    switch (weapontype)
                    {
                        case 0://근접
                            if (iscrit == true)
                            {
                                ShowCriticalText();
                                SoundController.Instance.SESound(3);
                            }
                            else
                            {
                                SoundController.Instance.SESound(2);
                            }
                            break;
                        case 1://원거리
                            if (iscrit == true)
                            {
                                ShowCriticalText();
                                SoundController.Instance.SESound(5);
                            }
                            else
                            {
                                SoundController.Instance.SESound(4);
                            }
                            break;
                        case 2://지속
                            if (iscrit == true)
                            {
                                ShowCriticalText();
                                SoundController.Instance.SESound(5);
                            }
                            else
                            {
                                SoundController.Instance.SESound(4);
                            }
                            break;
                    }
                }
            }

        }
    }

    private void ShowCriticalText()
    {
        mCrititcalText = Instantiate(CanvasFinder.Instance.mCriticalText, CanvasFinder.Instance.transform);
        mCrititcalText.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        mCrititcalText.transform.localScale = new Vector3(0.1f, 0.1f, 0);
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
            Player.Instance.LastHitEnemy = this;
        }
    }
    public void SkillObjAttack()
    {
        if (mEnemySkill.mEnemyObj.TargetSetting == true)
        {
            Player.Instance.Hit(mStats.Atk);
            Player.Instance.LastHitEnemy = this;
            Player.Instance.DoEffect(6, 0.75f);
        }
    }


    public IEnumerator SkillCast()
    {
        if (mState == eMonsterState.Traking&& GameController.Instance.pause == false)
        {
            WaitForSeconds cool = new WaitForSeconds(mStats.AtkSpd);
            mAnim.SetBool(AnimHash.Enemy_Walk, false);
            mEnemySkill.Skill();
            yield return cool;
            mEnemySkill.RemoveBulletParents();
            mCoroutine = null;
        }
    }

    public IEnumerator MoveToPlayer()
    {
        if (GameController.Instance.pause == false)
        {
            if (mState == eMonsterState.Traking && Spawned == true)
            {
                if (Stun == false && IsTraking == true)
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
    }

    public IEnumerator FireDamage(float damage)
    {
        WaitForSeconds dura = new WaitForSeconds(0.33f);
        isFire = true;
        Hit(damage / 3, 2,false);
        yield return dura;
        Hit(damage / 3, 2,false);
        yield return dura;
        Hit(damage / 3, 2,false);
        isFire = false;
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
