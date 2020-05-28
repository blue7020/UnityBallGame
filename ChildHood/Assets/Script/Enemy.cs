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

    private Vector3 Pos;

    private bool Attack;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Attack)
        {
            StartCoroutine(RandomState());
        }
        Pos = Player.Instance.transform.position;

        switch (State)
        {
            case eMonsterState.Idle:
            case eMonsterState.Move:
                mAnim.SetBool(AnimHash.Move, false);
                mRB2D.velocity = Vector2.zero;
                break;
            case eMonsterState.Skill:
                
                break;
            case eMonsterState.Die:
                gameObject.SetActive(false);
                Debug.Log("dead");
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

    private IEnumerator RandomState()
    {
        WaitForSeconds Cool = new WaitForSeconds(2f);
        int rand = Random.Range(0, 2);//0~1
        if (rand ==0)
        {
            State = eMonsterState.Idle;
        }
        else
        {
            State = eMonsterState.Move;
        }
        yield return Cool;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.Instance.Hit(mInfoArr[mID].Atk);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(RandomState());
            mPlayer = other.GetComponent<Player>();
            State = eMonsterState.Skill;
            Attack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (State==eMonsterState.Skill)
        {
            mAnim.SetBool(AnimHash.Move, true);
            while (true)
            {
                Vector3 direction = Pos - transform.position;
                direction = direction.normalized;
                mRB2D.velocity = direction * mInfoArr[mID].Spd;
                transform.Translate(Pos);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Attack = false;
        mPlayer = null;
        mRB2D.velocity = Vector2.zero;
        mAnim.SetBool(AnimHash.Move, false);
    }

    
}
