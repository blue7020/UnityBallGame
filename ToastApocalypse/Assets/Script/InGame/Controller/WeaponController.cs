using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : InformationLoader
{

    public static WeaponController Instance;

    public WeaponStat[] mInfoArr;

    public WeaponStat[] GetInfoArr()
    {
        return mInfoArr;
    }
    public List<Weapon> mWeapons;

    public int mWeaponSkillCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mWeaponSkillCount = 0;
            mWeapons = new List<Weapon>();
            LoadJson(out mInfoArr, Path.WEAPON_STAT);
            for (int i = 0; i < GameSetting.Instance.mWeapons.Length; i++)
            {
                mWeapons.Add(GameSetting.Instance.mWeapons[i]);
            }
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void WeaponSkill(int WeaponID, Enemy Target, bool Checker)
    {
        switch (WeaponID)
        {
            case 0://이쑤시개
                break;
            case 1://케찹
                break;
            case 2://머스타드
                Mustard(Target);
                break;
            case 3://바게트
                KnockBack(Target);
                break;
            case 4://포크
                break;
            case 5://뒤집개
                Fliper(Target, Checker);
                break;
            case 6://아이스크림 스쿱
                break;
            case 7://생크림 레이저
                break;
            case 8://피자 커터
                break;
            case 9://시리얼 디스펜서
                break;
            case 10://나루토마키
                break;
            case 11://냉동 고등어
                KnockBack(Target);
                break;
            case 12://이쑤시개 나이프
                break;
            case 13://팝콘 런처
                break;
            case 14://크럼브샷
                break;
            case 15://머핀 해머
                break;
            case 16://화염방사기
                break;
            case 17://잼블레이드
                JamBlade(Target);
                break;

        }
    }

    //WeaponSkillData
    private void Mustard(Enemy Target)
    {
        if (Target != null)
        {
            Target.StartCoroutine(Target.SpeedNurf(1.5f, 0.5f));
        }
    }

    public void KnockBack(Enemy Target)
    {
        if (Target != null)
        {
            Vector3 Pos = Player.Instance.transform.position - Target.transform.position;
            Target.mRB2D.velocity = Vector3.zero;
            Target.mRB2D.AddForce(-Pos * 15, ForceMode2D.Impulse);
        }
    }

    public void Fliper(Enemy Target, bool isCrit)
    {
        if (isCrit == true&&Target!=null)
        {
            StartCoroutine(Target.Stuned(1f));
        }
    }

    public void JamBlade(Enemy Target)
    {
        KnockBack(Target);
        if (mWeaponSkillCount==2)
        {
            PlayerBullet bolt = PlayerBulletPool.Instance.GetFromPool(10);
            bolt.mWeaponID = 17;
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand <= Player.Instance.mStats.Crit / 100)
            {
                bolt.mDamage = (Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0])) * (1 + Player.Instance.mStats.CritDamage);

            }
            else
            {
                bolt.mDamage = Player.Instance.mStats.Atk * (1 + Player.Instance.buffIncrease[0]);
            }
            bolt.transform.localPosition = Player.Instance.NowPlayerWeapon.transform.position;
            bolt.transform.localScale = bolt.mboltscale * (1 + PassiveArtifacts.Instance.AdditionalBulletSize);
            float angle = Mathf.Atan2(AttackPad.Instance.inputVector.y, AttackPad.Instance.inputVector.x) * Mathf.Rad2Deg;
            bolt.transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);
            bolt.mRB2D.AddForce(Player.Instance.NowPlayerWeapon.mAttackArea.BulletStarter.up * bolt.mSpeed, ForceMode2D.Impulse);
            mWeaponSkillCount = 0;
        }
        else
        {
            mWeaponSkillCount++;
        }
    }
}
