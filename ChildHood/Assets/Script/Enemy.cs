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

    private Animator mAnim;
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
        mMaxHP = mInfoArr[mID].Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (State)
        {
            case eMonsterState.Idle:

                break;
            case eMonsterState.Move:

                break;
            case eMonsterState.Skill:
                break;
            case eMonsterState.Die:

                break;
            default:
                Debug.LogError("Wrong State");
                break;
        }
    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mPlayer = other.GetComponent<Player>();
            State = eMonsterState.Move;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //플레이어 추격
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        mPlayer = null;
        State = eMonsterState.Idle;
    }

    
}
