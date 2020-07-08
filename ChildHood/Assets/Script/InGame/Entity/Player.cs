using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //TODO 캐릭터 선택 시 해당 ID에 맞는 캐릭터의 정보와 스프라이트를 출력하게끔

    public static Player Instance;

    [SerializeField]
    public VirtualJoyStick joyskick;

    public int mID;
    [SerializeField]
    public Sprite PlayerImage;
    public float mMaxHP;
    public float mCurrentHP;
    [SerializeField]
    public eDirection Look;

    public Room CurrentRoom;
    public int NowEnemyCount;
    public int EnemySwitch;

    [SerializeField]
    public List<Coroutine> NowBuff;
    public List<float> NowBuffValue;
    public List<eBuffType> NowBuffType;
    public List<bool> NowBuffActive;

    public PlayerStat Stats;

    [SerializeField]
    public Weapon NowPlayerWeapon;
    [SerializeField]
    public UsingItem NowItem;
    [SerializeField]
    public Artifacts NowUsingArtifact;
    [SerializeField]
    public Artifacts UseItemInventory;

    

    [SerializeField]
    public SpriteRenderer mRenderer;
    public Rigidbody2D mRB2D;
    public Animator mAnim;

    public bool Nodamage;

    public float hori;
    public float ver;

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
            Delete();
        }
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        NowBuff = new List<Coroutine>();
        NowBuffType = new List<eBuffType>();
        NowBuffValue = new List<float>();
        NowBuffActive = new List<bool>();
        NowItem = null;
        NowUsingArtifact = null;
        NowEnemyCount = 0;
        mMaxHP = Stats.Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        Nodamage = false;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();
        Moveing();
        
    }

    private void Moveing()
    {
        hori = joyskick.Horizontal();
        ver = joyskick.Vectical();
        Vector2 dir = new Vector2(hori, ver);
        dir = dir.normalized * Stats.Spd;
        if (hori > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            Look = eDirection.Right;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            
        }
        else if (hori < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            Look = eDirection.Left;
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
        if (Nodamage ==false)
        {
            if (damage - Stats.Def < 1)
            {
                damage = 0.5f;
                mCurrentHP -= damage;
            }
            else
            {
                mCurrentHP -= damage - Stats.Def;
            }
        }
        
    }
    public void PlayerSkill()
    {
        //TODO mValue = Player.Instance.mInfoArr[Player.Instance.mID].Atk;
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

    public void ItemUse()
    {
        if (NowItem!=null)
        {
            NowItem.UseItem();
            UIController.Instance.ShowHP();
        }
        
    }
    public void ArtifactUse()
    {
        if (NowUsingArtifact != null)
        {
            NowUsingArtifact.UseArtifact();
            UIController.Instance.ShowHP();
        }
    }

    //buffs
    //TODO 각 버프 중 플레이어한테 이펙트 표시
    public void Heal(float mHealAmount, float BonusHeal = 0)
    {
        if ((mCurrentHP + mHealAmount) >= mMaxHP)
        {
            mCurrentHP = mMaxHP;
        }
        else
        {
            mCurrentHP += mHealAmount + BonusHeal;//추가 회복값
        }
        UIController.Instance.ShowHP();
    }

    public IEnumerator Atk(float value = 0, float Cool = 0)
    {
        //TODO 애니메이션 이펙트 추가
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        int ID = NowBuff.Count;
        NowBuffActive.Add(true);
        NowBuffValue.Add(value);
        Stats.Atk += NowBuffValue[ID];
        NowBuffType.Add(eBuffType.Atk);
        yield return Dura;
        if (NowBuffActive[ID] == true)
        {
            Stats.Atk -= NowBuffValue[ID];
            NowBuffActive[ID] = false;
        }
    }

    public IEnumerator Speed(float value, float Cool)
    {
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        int ID = NowBuff.Count;
        NowBuffActive.Add(true);
        NowBuffValue.Add(value);
        Stats.Spd += NowBuffValue[ID];
        NowBuffType.Add(eBuffType.Spd);
        yield return Dura;
        if (NowBuffActive[ID] ==true)
        {
            Stats.Spd -= NowBuffValue[ID];
            NowBuffActive[ID] = false;
        }
        
    }

    public IEnumerator AtkSpeed(float value, float Cool)
    {
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        int ID = NowBuff.Count;
        Debug.Log(ID);
        NowBuffActive.Add(true);
        NowBuffValue.Add(value);
        Stats.AtkSpd -= NowBuffValue[ID];
        NowBuffType.Add(eBuffType.AtkSpd);
        yield return Dura;
        if (NowBuffActive[ID] == true)
        {
            Stats.AtkSpd += NowBuffValue[ID];
            NowBuffActive[ID] = false;
        }
    }

    public IEnumerator Def(float value, float Cool)
    {
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        int ID = NowBuff.Count;
        Debug.Log(ID);
        NowBuffActive.Add(true);
        NowBuffValue.Add(value);
        Stats.Def += NowBuffValue[ID];
        NowBuffType.Add(eBuffType.Def);
        yield return Dura;
        if (NowBuffActive[ID] == true)
        {
            Stats.Def -= NowBuffValue[ID];
            NowBuffActive[ID] = false;
        }
    }

}
