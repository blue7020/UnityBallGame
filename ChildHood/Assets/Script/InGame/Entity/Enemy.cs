using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InformationLoader
{
    //TODO 몬스터 생성 시 해당 ID에 맞는 몬스터의 정보와 스프라이트를 출력하게끔

    public static Enemy Instance;

    public eMonsterState State;
    private int mDelayCount;

    [SerializeField]
    public int mID;
    private Sprite[] mMonsterSpriteArr;

    [SerializeField]
    private Transform mGoldPos;
    [SerializeField]
    private Transform mHPBarPos;
    [SerializeField]
    EnemySkill mEnemySkill;
    private Player mPlayer;
    private GaugeBar mHPBar;

    private bool AttackOn;
    private bool HPBarOn;

    private Animator mAnim;
    [SerializeField]
    public float mCurrentHP;
    public float mMaxHP;

    [SerializeField]
    public PlayerStat[] mInfoArr;

    public PlayerStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadJson(out mInfoArr, Path.MONSTER_STAT);
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
    void FixedUpdate()
    {
        if (HPBarOn == true)
        {
            mHPBar.transform.position = mHPBarPos.position;
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
        }
        if (State != eMonsterState.Traking && State != eMonsterState.Die)
        {
            int rand = UnityEngine.Random.Range(0, 2);//0~1
            if (rand == 0)
            {
                State = eMonsterState.Idle;
                mDelayCount = 0;
            }
            else if (rand == 1)
            {
                State = eMonsterState.Move;
                mDelayCount = 0;
            }

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
                        transform.position = transform.position;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Move:
                    if (mDelayCount >= 20)
                    {
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
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Die:
                    if (mDelayCount == 0)
                    {
                        mDelayCount++;

                    }
                    else if (mDelayCount >= 10)
                    {
                        mAnim.SetBool(AnimHash.Enemy_Move, false);
                        //TODO 사망 시 색상 변경->회색
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
        mCurrentHP -= damage;
        //TODO 타격 시 색상 변경 0.5초간 빨강
        if (mHPBar == null)
        {
            mHPBar = GaugeBarPool.Instance.GetFromPool();
        }
        if (mCurrentHP <= 0)
        {
            mHPBar.gameObject.SetActive(false);
            gameObject.SetActive(false);
            mHPBar = null;
            HPBarOn = false;

        }
        else
        {
            mHPBar.gameObject.SetActive(true);
            mHPBar.SetGauge(mCurrentHP, mMaxHP);
            mHPBar.transform.position = mHPBarPos.position;
            HPBarOn = true;
        }
        if (mCurrentHP <= 0)
        {
            State = eMonsterState.Die;
            mDelayCount = 0;
            DropGold mDropGold;
            mDropGold = GameController.Instance.mGoldPool.GetFromPool();
            mDropGold.transform.position = mGoldPos.position;
            GoldDrop(mDropGold,mInfoArr[mID].Gold);

        }
    }

    public void GoldDrop(DropGold dropGold,float Gold)
    {
        if (mInfoArr[mID].Gold>=10&& mInfoArr[mID].Gold<20)
        {
            dropGold.mRenderer.sprite = dropGold.mSprites[1];
        }
        else if (mInfoArr[mID].Gold>=20)
        {
            dropGold.mRenderer.sprite = dropGold.mSprites[2];
        }
    }

    public IEnumerator Attack()
    {
        if(AttackOn == true)
        {
            AttackOn = false;
            WaitForSeconds AtkSpd = new WaitForSeconds(mInfoArr[mID].AtkSpd);

            Player.Instance.Hit(mInfoArr[mID].Atk);

            yield return AtkSpd;
        }
    }

    

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (State != eMonsterState.Die)
            {
                AttackOn = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            State = eMonsterState.Traking;
            mDelayCount = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        if (State==eMonsterState.Traking)
        {
            mEnemySkill.Skill(); //아마도 스킬 부분은 코루틴으로 불어와야할것같음
            WaitForSeconds one = new WaitForSeconds(2f);
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            mAnim.SetBool(AnimHash.Enemy_Move, true);
            transform.position += dir.normalized * mInfoArr[mID].Spd * Time.fixedDeltaTime;
            yield return one;
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            State = eMonsterState.Idle;
            mAnim.SetBool(AnimHash.Enemy_Move, false);
        }
        
        
    }
}
