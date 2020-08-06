using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public VirtualJoyStick joyskick;

    public int mID;
    public Sprite PlayerImage;
    public float mMaxHP;
    public float mCurrentHP;

    public Room CurrentRoom;
    public int EnemySwitch;

    public int mNowStage;
    public bool PlayerSkillStand;

    public List<BuffList> PlayerBuffList;
    public int buffIndex;

    public PlayerStat mStats;
    public float mGoldBonus;

    public GameObject mDirection;
    public Weapon NowPlayerWeapon;
    public PlayerSkill NowPlayerSkill;
    public UsingItem NowItem;
    public Artifacts NowUsingArtifact;
    public Artifacts UseItemInventory;

    public PlayerStat GetPlayerStats()
    {
        return mStats;
    }

    public void SetPlayerStats(PlayerStat stat)
    {
        mStats = stat;
    }

    public SpriteRenderer mRenderer;
    public Rigidbody2D mRB2D;
    public Animator mAnim;

    public bool Nodamage;

    public float hori;
    public float ver;

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
        PlayerSkillStand = false;
        NowItem = null;
        NowUsingArtifact = null;
        mMaxHP = mStats.Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        Nodamage = false;
        mGoldBonus = 0;
        buffIndex = 0;
        UIController.Instance.ShowGold();
        UIController.Instance.ShowHP();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Moveing();
    }

    private void Moveing()
    {

        hori = joyskick.Horizontal();
        ver = joyskick.Vectical();
        Vector2 dir = new Vector2(hori, ver);
        dir = dir.normalized * mStats.Spd;
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
        if (PlayerSkillStand == false)
        {
            mRB2D.velocity = dir;
        }

    }

    public void Hit(float damage)
    {
        if (Nodamage ==false)
        {
            StartCoroutine(HitAnimation());
            if (damage - mStats.Def < 1)
            {
                damage = 0.5f;
                mCurrentHP -= damage;
            }
            else
            {
                mCurrentHP -= damage - mStats.Def;
            }
            UIController.Instance.ShowHP();
        }
        if (mCurrentHP <= 0)
        {
            GameController.Instance.GameOver();
        }
    }

    private IEnumerator HitAnimation()
    {
        WaitForSeconds Time = new WaitForSeconds(0.3f);
        mRenderer.color = Color.red;
        Nodamage = true;
        yield return Time;
        mRenderer.color = Color.white;
        Nodamage = false;
        StopCoroutine(HitAnimation());
    }

    public void Dash(Vector3 dir,int speed)
    {
        PlayerSkillStand = true;
        Nodamage = true;
        mRB2D.velocity = Vector3.zero;
        mRB2D.velocity = dir.normalized * speed;
        StartCoroutine(StandingCool());
    }

    private IEnumerator StandingCool()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        yield return delay;
        PlayerSkillStand = false;
        Nodamage = false;
        mAnim.SetBool(AnimHash.Tumble, false);
        transform.rotation = Quaternion.identity;
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

    public IEnumerator Atk(float value, float Cool)
    {
        //TODO 애니메이션 이펙트 추가
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        mStats.Atk += value;
        yield return Dura;
        mStats.Atk -= value;
    }

    public IEnumerator Speed(float value, float Cool)
    {
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        mStats.Spd += value;
        yield return Dura;
        mStats.Spd -= value;

    }

    public IEnumerator AtkSpeed(float value, float Cool)
    {
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        mStats.AtkSpd += value;
        yield return Dura;
        mStats.AtkSpd -= value;
    }

    public IEnumerator Def(float value, float Cool)
    {
        WaitForSeconds Dura = new WaitForSeconds(Cool);
        if (mStats.Def < 1)
        {
            value = 1;
        }
        mStats.Def += value;
        yield return Dura;
        mStats.Def -= value;
    }


    //Artifact
    public void EquipArtifact(Artifacts art)
    {
        art.Equip = true;
        mMaxHP *= (1+ art.mStats.Hp);
        mStats.Atk *= (1 + art.mStats.Atk);
        if (mStats.AtkSpd * (1 - art.mStats.AtkSpd) < 0.1f)
        {
            mStats.AtkSpd = 0.1f;
        }
        else
        {
            mStats.AtkSpd *= (-1 *(1 + art.mStats.AtkSpd));
        }
        mStats.Spd *= (1 + art.mStats.Spd);
        mStats.Def *= (1 + art.mStats.Def);
        if (mStats.Crit + art.mStats.Crit > 1)
        {
            mStats.Crit = 1f;
        }
        else
        {
            mStats.Crit += art.mStats.Crit;
        }
        if (mStats.CritDamage + art.mStats.Crit > 2)
        {
            mStats.CritDamage = 2f;
        }
        else
        {
            mStats.CritDamage += art.mStats.CritDamage;
        }
        if (mStats.CCReduce + art.mStats.CCReduce > 0.5f)
        {
            mStats.CCReduce = 0.5f;
        }
        else
        {
            mStats.CCReduce += art.mStats.CCReduce;
        }
        if (mStats.CooltimeReduce + art.mStats.CooltimeReduce > 0.5f)
        {
            mStats.CooltimeReduce = 0.5f;
        }
        else
        {
            mStats.CooltimeReduce += art.mStats.CooltimeReduce;
        }

        if (art.mType == eArtifactType.Use)
        {
            NowUsingArtifact = art;
            UIController.Instance.ShowArtifactImage();
        }
        UIController.Instance.ShowHP();
    }
    public void UnequipArtifact(Artifacts art)
    {
        mMaxHP -= mMaxHP * (1 + art.mStats.Hp);
        mStats.Atk = -mStats.Atk * (1 + art.mStats.Atk);
        mStats.AtkSpd = -mStats.AtkSpd * (1 + art.mStats.AtkSpd);
        mStats.Spd = -mStats.Spd * (1 + art.mStats.Spd);
        mStats.Def = -mStats.Def * (1 + art.mStats.Def);
        mStats.Crit -= art.mStats.Crit;
        mStats.CritDamage -= art.mStats.CritDamage;
        mStats.CCReduce -= art.mStats.CCReduce;
        mStats.CooltimeReduce -= art.mStats.CooltimeReduce;
        UIController.Instance.ShowHP();
        art.Equip = false;
    }

    public void EquipWeapon(Weapon weapon)
    {
        weapon.Equip = true;
        mStats.Atk += weapon.mStats.Atk;
        mStats.AtkSpd += weapon.mStats.AtkSpd;
        mStats.Crit += weapon.mStats.Crit / 100;
        mStats.CritDamage += weapon.mStats.CritDamage;
        NowPlayerWeapon = weapon;
        UIController.Instance.ShowWeaponImage();
    }

    public void UnequipWeapon(Weapon weapon)
    {
        mStats.Atk -= weapon.mStats.Atk;
        mStats.AtkSpd -= weapon.mStats.AtkSpd;
        mStats.Crit -= weapon.mStats.Crit / 100;
        mStats.CritDamage -= weapon.mStats.CritDamage;
        NowPlayerWeapon = null;
        UIController.Instance.ShowWeaponImage();
        weapon.Equip = false;
    }
}
