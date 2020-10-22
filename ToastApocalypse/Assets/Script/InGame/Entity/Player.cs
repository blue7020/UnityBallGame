using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public VirtualJoyStick joyskick;

    public int mID;
    public int mTotalKillCount;
    public float mMaxHP;
    public float mCurrentHP;
    public const int MAX_AIR = 100;
    public int mCurrentAir;
    public bool OnAir;

    public bool TrapResistance,MapSeeker,NoCC,InfiniteAmmo;

    public Room CurrentRoom;
    public int EnemySwitch;

    public int mNowStage;
    public bool PlayerSkillStand;

    public float[] buffIncrease;//0=공격력, 1=방어력, 2=공격속도, 3=이동속도, 4=치명타, 5=치명타 데미지, 6=상태이상 저항
    public float BonusHeal;
    public bool Stun;
    public int PlusBoltCount;
    public Coroutine[] NowDebuffArr;
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

    public Enemy LastHitEnemy;
    public int DeathBy;

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
            mTotalKillCount = 0;
            BonusHeal = 0;
            TrapResistance = false;
            MapSeeker = false;
            InfiniteAmmo = false;
            Stun = false;
            NoCC = false;
            Nodamage = true;
            OnAir = false;
            mCurrentAir = MAX_AIR;
            PlayerSkillStand = false;
            NowItem = null;
            NowActiveArtifact = null;
            mGoldBonus = 0;
            AttackSpeedStat = 0;
            buffIncrease = new float[7];
            for (int i = 0; i < buffIncrease.Length; i++)
            {
                buffIncrease[i] = 0;
            }
            NowDebuffArr = new Coroutine[9];
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false && GameController.Instance.IsTutorial == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        mMaxHP = mStats.Hp;
        mCurrentHP = mMaxHP;//최대 체력에 변동이 생기면 mmaxHP를 조작
        StartCoroutine(StartInvincible());
        if (GameController.Instance.IsTutorial == false)
        {
            if (GameSetting.Instance.NowStage == 4)
            {
                StartCoroutine(Air());
            }
            else if (GameSetting.Instance.NowStage == 6)
            {
                mMaxHP = mStats.Hp - (mStats.Hp * 0.15f);
                mStats.AtkSpd += mStats.AtkSpd * 0.15f;
                mStats.Spd -= mStats.Spd * 0.15f;
            }
            UIController.Instance.ShowGold();
            UIController.Instance.ShowHP();
        }
        else
        {
            TutorialUIController.Instance.ShowHP();
        }
    }

    private IEnumerator StartInvincible()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        BuffController.Instance.SetBuff(7, 10, eBuffType.Buff, 1.5f);
        Nodamage = true;
        yield return delay;
        Nodamage = false;
    }

    public void IsEnemyDeathPassiveSkill()
    {
        if (NowPlayerWeapon.mID==22)
        {
            WeaponController.Instance.SandwichSword();
        }
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
        BuffController.Instance.RemoveAll();
    }

    private void FixedUpdate()
    {
        Moveing();
    }

    private IEnumerator Air()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        while (true)
        {
            if (mCurrentAir < 1)
            {
                Hit(mMaxHP * 0.1f,3,true);
                UIController.Instance.ShowHP();
            }
            if (OnAir == false)
            {
                if (mCurrentAir >= 5)
                {
                    mCurrentAir -= 5;
                }
                UIController.Instance.ShowAirGaugeBar();
            }

            yield return delay;
        }
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
                mRenderer.flipX = true;

            }
            else if (hori < 0)
            {
                mAnim.SetBool(AnimHash.Walk, true);
                mRenderer.flipX = false;
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

    public void Hit(float damage,int damagetype=0,bool trueDamage=false)
    {
        if (Nodamage ==false)
        {
            StartCoroutine(HitAnim());
            int rand = UnityEngine.Random.Range(0, 2);
            SoundController.Instance.SESound(rand);
            if (trueDamage==false)
            {
                if (damage - (mStats.Def * (1 + buffIncrease[1])) < 1)
                {
                    damage = 0.5f;
                    mCurrentHP -= damage;
                }
                else
                {
                    mCurrentHP -= damage - (mStats.Def * (1 + buffIncrease[1]));
                }
            }
            else
            {
                mCurrentHP -= damage;
            }
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.ShowHP();
            }
            else
            {
                TutorialUIController.Instance.ShowHP();
            }
            if (damagetype==1)
            {
                LastHitEnemy = null;
                DeathBy = 1;
            }
            else if (damagetype == 2)
            {
                LastHitEnemy = null;
                DeathBy = 2;
            }
            else if (damagetype == 3)
            {
                LastHitEnemy = null;
                DeathBy = 3;
            }
            else
            {
                DeathBy = 0;
            }
        }
        if (mCurrentHP <= 0)
        {
            if (GameController.Instance.IsTutorial==true)
            {
                GameOver();
            }
            else
            {
                Nodamage = true;
                if (GameController.Instance.ReviveCode == 0)
                {
                    UIController.Instance.mReviveWindow.gameObject.SetActive(true);
                    GameController.Instance.GamePause();
                }
                else
                {
                    GameOver();
                }
            }
        }
    }

    private IEnumerator HitAnim()
    {
        WaitForSeconds Time = new WaitForSeconds(0.3f);
        if (Nodamage==false&&mCurrentHP>0)
        {
            HitAnimation.Instance.mHitAnim.SetBool(AnimHash.HitAnim, true);
            mRenderer.color = Color.red;
            Nodamage = true;
            yield return Time;
            mRenderer.color = Color.white;
            Nodamage = false;
            HitAnimation.Instance.mHitAnim.SetBool(AnimHash.HitAnim, false);
            StopCoroutine(HitAnim());
        }
        else
        {
            StopCoroutine(HitAnim());
        }
    }


    public IEnumerator Revive()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        SoundController.Instance.SESoundUI(10);
        UIController.Instance.mReviveWindow.gameObject.SetActive(false);
        BuffController.Instance.SetBuff(7, 7, eBuffType.Buff, 2f);
        Nodamage = true;
        mCurrentHP = mMaxHP;
        UIController.Instance.ShowHP();
        if (GameSetting.Instance.NowStage == 4)
        {
            mCurrentAir = MAX_AIR;
            UIController.Instance.ShowAirGaugeBar();
        }
        yield return delay;
        Nodamage = false;
    }

    public void GameOver()
    {
        if (GameController.Instance.IsTutorial == false)
        {
            UIController.Instance.mReviveWindow.gameObject.SetActive(false);
            Nodamage = true;
            StopCoroutine(HitAnim());
            GameController.Instance.pause = true;
            SoundController.Instance.mBGM.Stop();
            mRB2D.velocity = Vector3.zero;
            Stun = true;
            mRenderer.sortingLayerName = "UI";
            mRenderer.sortingOrder = 4;
            UIController.Instance.mDeathUI.gameObject.SetActive(true);
            mAnim.SetBool(AnimHash.Death, true);
        }
        else
        {
            transform.position =CurrentRoom.mStartPos.position;
            mCurrentHP = mMaxHP;
            TutorialUIController.Instance.ShowHP();
            if (CurrentRoom.eType==eRoomType.Boss)
            {
                LastHitEnemy.mCurrentHP = LastHitEnemy.mMaxHP;
                LastHitEnemy.mHPBar.SetGauge(LastHitEnemy.mCurrentHP, LastHitEnemy.mMaxHP);
            }
        }
    }
    public void DeathWindow()
    {
        gameObject.SetActive(false);
        UIController.Instance.ShowDeathWindow();
    }


    public void Dash(Vector3 dir, int code,int speed)
    {
        PlayerSkillStand = true;
        BuffController.Instance.SetBuff(7, code, eBuffType.Buff,0.6f);
        mRB2D.velocity = Vector3.zero;
        mRB2D.velocity = dir.normalized * speed;
        StartCoroutine(StandingCool());
    }

    private IEnumerator StandingCool()
    {
        WaitForSeconds delay = new WaitForSeconds(0.25f);
        Nodamage = true;
        yield return delay;
        PlayerSkillStand = false;
        yield return delay;
        Nodamage = false;
        mAnim.SetBool(AnimHash.Tumble, false);
        transform.rotation = Quaternion.identity;
    }

    public void ItemUse()
    {
        if (NowItem!=null)
        {
            SoundController.Instance.SESoundUI(4);
            NowItem.UseItem();
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.ShowHP();
            }
            else
            {
                TutorialUIController.Instance.ShowHP();
            }
        }
        
    }
    public void ArtifactUse()
    {
        if (NowActiveArtifact != null)
        {
            NowActiveArtifact.UseArtifact();
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.ShowHP();
            }
            else
            {
                TutorialUIController.Instance.ShowHP();
            }
        }
    }


    public void DoEffect(int Effectcode,float duration, int code=0, float value =0)//사라지는 오브젝트들은 코루틴이 중단되니 여기서 해결
    {
        switch (Effectcode)
        {
            case 1:
                if (value<0)
                {
                    NowDebuffArr[0] = StartCoroutine(Atk(value, code, duration));
                }
                else
                {
                    StartCoroutine(Atk(value, code, duration));
                }
                break;
            case 2:
                if (value < 0)
                {
                    NowDebuffArr[0] = StartCoroutine(Def(value, code, duration));
                }
                else
                {
                    StartCoroutine(Def(value, code, duration));
                }
                break;
            case 3:
                if (value < 0)
                {
                    NowDebuffArr[0] = StartCoroutine(AtkSpeed(value, code, duration));
                }
                else
                {
                    StartCoroutine(AtkSpeed(value, code, duration));
                }
                break;
            case 4:
                if (value < 0)
                {
                    NowDebuffArr[0] = StartCoroutine(Speed(value, code, duration));
                }
                else
                {
                    StartCoroutine(Speed(value, code, duration));
                }
                break;
            case 5:
                StartCoroutine(CCreduce(value, code, duration));
                break;
            case 6://stun
                NowDebuffArr[0] = StartCoroutine(Stuned(code,duration));
                break;
        }
    }
    //buffs
    public void Heal(float mHealAmount)
    {
        if ((mCurrentHP + mHealAmount) >= mMaxHP)
        {
            mCurrentHP = mMaxHP;
        }
        else
        {
            mCurrentHP += mHealAmount + BonusHeal;//추가 회복값
        }
        if (GameController.Instance.IsTutorial == false)
        {
            UIController.Instance.ShowHP();
        }
        else
        {
            TutorialUIController.Instance.ShowHP();
        }
    }

    public IEnumerator Atk(float value, int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[0] += value;
        if (value > 0)
        {
            BuffController.Instance.SetBuff(0, code, eBuffType.Buff, duration);
        }
        else
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand > mStats.CCReduce)
            {
                BuffController.Instance.SetBuff(9, code, eBuffType.Debuff, duration);
            }
        }
        yield return Dura;
        buffIncrease[0] -= value;
    }

    public IEnumerator Def(float value, int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[1] += value;
        if (value > 0)
        {
            BuffController.Instance.SetBuff(1, code, eBuffType.Buff, duration);
        }
        else
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand > mStats.CCReduce)
            {
                BuffController.Instance.SetBuff(10, code, eBuffType.Debuff, duration);
            }
        }
        yield return Dura;
        buffIncrease[1] -= value;
    }

    public IEnumerator AtkSpeed(float value,int code,float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[2] += value;
        if (value > 0)
        {
            BuffController.Instance.SetBuff(2, code, eBuffType.Buff, duration);
        }
        else
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand > mStats.CCReduce)
            {
                BuffController.Instance.SetBuff(11, code, eBuffType.Debuff, duration);
            }
        }
        yield return Dura;
        buffIncrease[2] -= value;
    }
    public IEnumerator Speed(float value, int code,float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[3] += value;
        if (value>0)
        {
            BuffController.Instance.SetBuff(3, code, eBuffType.Buff, duration);
        }
        else
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand > mStats.CCReduce)
            {
                BuffController.Instance.SetBuff(12, code, eBuffType.Debuff, duration);
            }
        }
        yield return Dura;
        buffIncrease[3] -= value;

    }

    public IEnumerator Critical(float value, int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[5] += value;
        if (value > 0)
        {
            BuffController.Instance.SetBuff(4, code, eBuffType.Buff, duration);
        }
        else
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand > mStats.CCReduce)
            {
                BuffController.Instance.SetBuff(13, code, eBuffType.Debuff, duration);
            }
        }
        yield return Dura;
        buffIncrease[5] -= value;

    }

    public IEnumerator NoUseAmmo(int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        InfiniteAmmo = true;
        BuffController.Instance.SetBuff(14, code, eBuffType.Buff, duration);
        yield return Dura;
        InfiniteAmmo = false;

    }

    public IEnumerator PlusBolt(int value,int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        PlusBoltCount += value;
        BuffController.Instance.SetBuff(15, code, eBuffType.Buff, duration);
        yield return Dura;
        PlusBoltCount -= value;

    }

    public IEnumerator CCreduce(float value, int code, float duration)
    {
        WaitForSeconds Dura = new WaitForSeconds(duration);
        buffIncrease[6] += value;
        BuffController.Instance.SetBuff(8, code, eBuffType.Buff, duration);
        yield return Dura;
        buffIncrease[6] -= value;
    }

    //debuff
    public IEnumerator Stuned(int code,float duration)
    {
        float rand = UnityEngine.Random.Range(0, 1f);
        if (rand>mStats.CCReduce)
        {
            WaitForSeconds Dura = new WaitForSeconds(duration);
            if (Stun == false)
            {
                Stun = true;
                BuffController.Instance.SetBuff(5, code, eBuffType.Debuff, duration);
                mRB2D.velocity = Vector3.zero;
                CCState.SetActive(true);
                yield return Dura;
                Stun = false;
                CCState.SetActive(false);
                NowDebuffArr[0] = null;
            }
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
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.ShowArtifactImage();
            }
            else
            {
                TutorialUIController.Instance.ShowArtifactImage();
            }
        }
        if (GameController.Instance.IsTutorial == false)
        {
            UIController.Instance.ShowHP();
        }
        else
        {
            TutorialUIController.Instance.ShowHP();
        }
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
        if (GameController.Instance.IsTutorial == false)
        {
            UIController.Instance.ShowHP();
        }
        else
        {
            TutorialUIController.Instance.ShowHP();
        }
        art.Equip = false;
    }

    public void EquipWeapon(Weapon weapon)
    {
        weapon.Equip = true;
        mStats.Atk += weapon.mStats.Atk;
        mStats.AtkSpd += weapon.mStats.AtkSpd;
        mStats.Crit += weapon.mStats.Crit / 100;
        mStats.CritDamage += weapon.mStats.CritDamage;
        BonusHeal += weapon.mStats.BonusHeal;
        NowPlayerWeapon = weapon;
        if (GameController.Instance.IsTutorial == false)
        {
            UIController.Instance.ShowWeaponImage();
        }
        else
        {
            TutorialUIController.Instance.ShowWeaponImage();
        }
    }

    public void UnequipWeapon(Weapon weapon)
    {
        mStats.Atk -= weapon.mStats.Atk;
        mStats.AtkSpd -= weapon.mStats.AtkSpd;
        mStats.Crit -= weapon.mStats.Crit / 100;
        mStats.CritDamage -= weapon.mStats.CritDamage;
        BonusHeal -= weapon.mStats.BonusHeal;
        NowPlayerWeapon = null;
        if (GameController.Instance.IsTutorial == false)
        {
            UIController.Instance.ShowWeaponImage();
        }
        else
        {
            TutorialUIController.Instance.ShowWeaponImage();
        }
        weapon.Equip = false;
    }
}
