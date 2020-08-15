using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public VirtualJoyStick joyskick;

    public int mID;
    public float mMaxHP;
    public float mCurrentHP;

    public Room CurrentRoom;
    public int EnemySwitch;

    public int mNowStage;
    public bool PlayerSkillStand;

    public float[] buffIncrease;//0=공격력, 1=방어력, 2=공격속도, 3=이동속도
    public bool Stun;
    public bool Stunning;
    public GameObject CCState;

    public PlayerStat mStats;
    //공격속도는 플레이어 기본 공격속도(무기) / (1+ 버프 + 증가 스탯 공격속도)
    public float AttackSpeedStat;
    public float mGoldBonus;

    public GameObject mDirection;
    public Weapon NowPlayerWeapon;
    public PlayerSkill NowPlayerSkill;
    public UsingItem NowItem;
    public Artifacts NowActiveArtifact;
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
        buffIncrease = new float[4];
        for (int i = 0; i < buffIncrease.Length; i++)
        {
            buffIncrease[i] = 0;
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        Stun = false;
        Stunning = false;
        PlayerSkillStand = false;
        NowItem = null;
        NowActiveArtifact = null;
        mMaxHP = mStats.Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        Nodamage = false;
        mGoldBonus = 0;
        AttackSpeedStat = 0;
        UIController.Instance.ShowGold();
        UIController.Instance.ShowHP();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void ResetBuff()
    {
        for (int i = 0; i < buffIncrease.Length; i++)
        {
            buffIncrease[i] = 0;
        }
    }

    private void Update()
    {

        Moveing();
    }

    private void Moveing()
    {
        hori = joyskick.Horizontal();
        ver = joyskick.Vectical();
        if (Stun == false)
        {
            Vector2 dir = new Vector2(hori, ver);
            float value = mStats.Spd * (1 + buffIncrease[3]);
            dir = dir.normalized * value;
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

    }

    public void Hit(float damage)
    {
        if (Nodamage ==false)
        {
            StartCoroutine(HitAnimation());
            if (damage - (mStats.Def*(1+buffIncrease[1])) < 1)
            {
                damage = 0.5f;
                mCurrentHP -= damage;
            }
            else
            {
                mCurrentHP -= damage - (mStats.Def * (1 + buffIncrease[1]));
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

    public void Dash(Vector3 dir, int code,int speed)
    {
        PlayerSkillStand = true;
        Nodamage = true;
        BuffController.Instance.SetBuff(7, code, eBuffType.Buff,0.5f);
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
        if (NowActiveArtifact != null)
        {
            NowActiveArtifact.UseArtifact();
            UIController.Instance.ShowHP();
        }
    }

    //buffs
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

    public IEnumerator Atk(float value, int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[0] += value;
        BuffController.Instance.SetBuff(0,code, eBuffType.Buff,duration);
        yield return Dura;
        buffIncrease[0] -= value;
    }

    public IEnumerator Def(float value, int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[1] += value;
        BuffController.Instance.SetBuff(1,code, eBuffType.Buff, duration);
        yield return Dura;
        buffIncrease[1] -= value;
    }

    public IEnumerator AtkSpeed(float value,int code,float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[2] += value;
        BuffController.Instance.SetBuff(2, code, eBuffType.Buff, duration);
        yield return Dura;
        buffIncrease[2] -= value;
    }

    public IEnumerator Speed(float value, int code,float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[3] += value;
        BuffController.Instance.SetBuff(3,code,eBuffType.Buff, duration);
        yield return Dura;
        buffIncrease[3] -= value;

    }

    public IEnumerator Stuned(float duration)
    {
        WaitForSeconds dura = new WaitForSeconds(duration);
        if (Stunning==false)
        {
            Stunning = true;
            Stun = true;
            mRB2D.velocity = Vector3.zero;
            CCState.SetActive(true);
            yield return dura;
            Stun = false;
            CCState.SetActive(false);
            Stunning = false;
        }
    }

    //Artifact
    public void EquipArtifact(Artifacts art)
    {
        art.Equip = true;
        mMaxHP *= (1+ art.mStats.Hp);
        mStats.Atk *= (1 + art.mStats.Atk);
        mStats.Spd *= (1 + art.mStats.Spd);
        mStats.Def *= (1 + art.mStats.Def);
        AttackSpeedStat+=art.mStats.AtkSpd;
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

        if (art.eType == eArtifactType.Active)
        {
            NowActiveArtifact = art;
            UIController.Instance.ShowArtifactImage();
        }
        UIController.Instance.ShowHP();
    }
    public void UnequipArtifact(Artifacts art)
    {
        mMaxHP *= (1 - art.mStats.Hp);
        mStats.Atk *= (1 - art.mStats.Atk);
        mStats.Spd *= (1 - art.mStats.Spd);
        mStats.Def *= (1 - art.mStats.Def);
        AttackSpeedStat -= art.mStats.AtkSpd;
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
