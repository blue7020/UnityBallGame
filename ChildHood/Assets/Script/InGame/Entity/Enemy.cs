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
    public int mID;//나중에 캐릭터 선택 시 해당 아이디를 부여하는 것으로 수정
    private Sprite[] mMonsterSpriteArr;

    [SerializeField]
    private Transform mGoldPos;
    [SerializeField]
    private Transform mHPBarPos;
    private Player mPlayer;
    private GaugeBar mHPBar;

    private bool AttackOn;
    private bool HPBarOn;

    private Rigidbody2D mRB2D;
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
        mRB2D = GetComponent<Rigidbody2D>();
        mMaxHP = mInfoArr[mID].Hp;
    }

    private void OnEnable()
    {
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        State = eMonsterState.Idle;
        mDelayCount = 0;
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
        if (State!=eMonsterState.Skill&&State!=eMonsterState.Die)
        {
                int rand = Random.Range(0, 2);//0~1
            if (rand == 0)
            {
                State = eMonsterState.Idle;
                mDelayCount = 0;
            }
            else
            {
                State = eMonsterState.Move;
                mDelayCount = 0;
            }
            
        }   
    }

    public IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        while (true)
        {
            switch (State)
            {
                case eMonsterState.Idle:
                    if (mDelayCount >= 20)
                    {
                        mRB2D.velocity = Vector2.zero;
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
                        mRB2D.velocity = Vector2.zero;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eMonsterState.Skill:
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
                        mAnim.SetBool(AnimHash.Move, false);
                        //사망 시 색상 변경->회색
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
        //타격 시 색상 변경 0.5초간 빨강
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
        if (dropGold.GoldStack == 1)
        {
            dropGold.mRenderer.sprite = dropGold.mSprites[1];
        }
        else if (dropGold.GoldStack == 3)
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

    //TODO 플레이어가 방을 나갔을 때 원래 위치로 돌아가는 BackHome 기능 추가하기

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            State = eMonsterState.Skill;
            mDelayCount = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        if (State==eMonsterState.Skill)
        {

            WaitForSeconds one = new WaitForSeconds(2f);
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            mAnim.SetBool(AnimHash.Move, true);
            transform.position += dir.normalized * mInfoArr[mID].Spd * Time.fixedDeltaTime;
            yield return one;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            State = eMonsterState.Idle;
            mAnim.SetBool(AnimHash.Move, false);
        }
        
        
    }
}
