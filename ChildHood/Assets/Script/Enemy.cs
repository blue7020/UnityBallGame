using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InformationLoader
{
    //TODO 몬스터 생성 시 해당 ID에 맞는 몬스터의 정보와 스프라이트를 출력하게끔

    public static Enemy Instance;

    public eMonsterState State;

    public int mID = 0;//나중에 캐릭터 선택 시 해당 아이디를 부여하는 것으로 수정
    private Sprite[] mMonsterSpriteArr;

    private Player mPlayer;

    private Rigidbody2D mRB2D;
    private Animator mAnim;
    [SerializeField]
    private float mCurrentHP;
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
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        State = eMonsterState.Idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (State!=eMonsterState.Skill)
        {
            int rand = Random.Range(0, 2);//0~1
            if (rand == 0)
            {
                State = eMonsterState.Idle;
            }
            else
            {
                State = eMonsterState.Move;
            }
        }
        
        switch (State)
        {
            case eMonsterState.Idle:
            case eMonsterState.Move:
                mRB2D.velocity = Vector2.zero;
                break;
            case eMonsterState.Skill:
                mAnim.SetBool(AnimHash.Move, true);
                break;
            case eMonsterState.Die:
                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Wrong State");
                break;
        }
        
    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
        if (mCurrentHP <= 0)
        {
            State = eMonsterState.Die;
        }
    }

    public void Attack()
    {
        Player.Instance.mCurrentHP -= mInfoArr[mID].Atk;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Attack();
    }

    //TODO 플레이어가 방을 나갔을 때 원래 위치로 돌아가는 BackHome 기능 추가하기

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            State = eMonsterState.Skill;
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
