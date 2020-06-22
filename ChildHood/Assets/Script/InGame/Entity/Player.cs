using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : InformationLoader
{
    //TODO 캐릭터 선택 시 해당 ID에 맞는 캐릭터의 정보와 스프라이트를 출력하게끔

    public static Player Instance;

    public int mID;
    private Sprite[] mPlayerSpriteArr;
    public float mMaxHP;
    public float mCurrentHP;

    public int Level;//현재 층
    public int MapLevel;//현재 스테이지

    public Room CurrentRoom;
    public int NowEnemyCount;

    [SerializeField]
    public PlayerStat[] mInfoArr;
    [SerializeField]
    public UsingItem NowItem;

    [SerializeField]
    public SpriteRenderer mRenderer;
    private Rigidbody2D mRB2D;
    private Animator mAnim;

    public float hori;
    public float ver;

    public bool IsBuff;//석상의 버프를 받고 있는지

    //public PlayerStat[] GetInfoArr()
    //{
    //    return mInfoArr;
    //}

    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        MapLevel = 1;//나중에 맵 선택 시 해당 레벨을 부여하는 것으로 수정
        mID = 0;//나중에 캐릭터 선택 시 해당 아이디를 부여하는 것으로 수정
        LoadJson(out mInfoArr, Path.PLAYER_STAT);
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
    }

    //TODO 스테이지를 넘어갈 때마다 IsBuff를 false로 바꾸기, 만약 이전 스테이지에서 버프를 받고 있었다면 버프를 제거하고, 플레이어 능력치 원상 복구

    private void Start()
    {
        NowItem = null;
        NowEnemyCount = 0;
        Level = 1;
        IsBuff = false;
        mMaxHP = mInfoArr[mID].Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
    }

    private void Update()
    {
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();
        Moveing();
        
    }
    
    private void Moveing()
    {
        hori = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(hori, ver);
        dir = dir.normalized * mInfoArr[mID].Spd;
        if (hori > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            
        }
        else if (hori < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.identity;
        }
        else if (ver > 0 || ver < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }

        mRB2D.velocity = dir;
    }

    public void Hit(float damage)
    {
        if (damage - mInfoArr[mID].Def <1)
        {
            damage = 0.5f;
            mCurrentHP -= damage;
        }
        else
        {
            mCurrentHP -= damage - mInfoArr[mID].Def;
        }
    }
    public void PlayerSkill()
    {
        //mValue = Player.Instance.mInfoArr[Player.Instance.mID].Atk;
        //벨류는 스킬에 따라 각각 적용하기로
        switch (mID)
        {
            case 0: //구르기
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                Debug.LogError("Wrong Player ID");
                break;

        }
    }

    //buffs
    //TODO 각 버프 중 플레이어한테 이펙트 표시
    public void Heal(float mHealAmount)
    {
        if ((mCurrentHP + mHealAmount) >= mMaxHP)
        {
            mCurrentHP = mMaxHP;
        }
        else
        {
            mCurrentHP += mHealAmount;//+회복량 증가 옵션
        }
        UIController.Instance.ShowHP();
    }

    public IEnumerator Atk(float value,float Cool)
    {
        Instance.IsBuff = true;
        if (Instance.IsBuff == true)
        {
            //TODO 애니메이션 이펙트 추가
            mInfoArr[Instance.mID].Atk += value;
            WaitForSeconds Dura = new WaitForSeconds(Cool);
            yield return Dura;
            mInfoArr[Instance.mID].Atk -= value;
        }
    }

    public IEnumerator Speed(float value, float Cool)
    {
        IsBuff = true;
        if (IsBuff == true)
        {
            mInfoArr[mID].Spd += value;
            mInfoArr[mID].AtkSpd -= value;
            WaitForSeconds Dura = new WaitForSeconds(Cool);
            yield return Dura;
            mInfoArr[mID].Spd -= value;
            mInfoArr[mID].AtkSpd += value;
            IsBuff = false;
        }
    }

    public IEnumerator AtkSpeed(float value, float Cool)
    {
        IsBuff = true;
        if (IsBuff == true)
        {
            mInfoArr[mID].AtkSpd -= value;
            WaitForSeconds Dura = new WaitForSeconds(Cool);
            yield return Dura;
            mInfoArr[mID].AtkSpd += value;
            IsBuff = false;
        }
    }

    public IEnumerator Def(float value, float Cool)
    {
        IsBuff = true;
        if (IsBuff == true)
        {
            mInfoArr[mID].Def += value;
            WaitForSeconds Dura = new WaitForSeconds(Cool);
            yield return Dura;
            mInfoArr[mID].Def -= value;
            IsBuff = false;
        }
    }

}
