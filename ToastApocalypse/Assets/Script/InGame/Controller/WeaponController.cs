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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
                Baguette(Target);
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
                FrozenMackerel(Target);
                break;

        }
    }

    //WeaponSkillData
    private void Mustard(Enemy Target)
    {
        Target.StartCoroutine(Target.SpeedNurf(1.5f,0.5f));
    }

    public void Baguette(Enemy Target)
    {
        Vector3 Pos = Player.Instance.transform.position - Target.transform.position;
        Target.mRB2D.velocity = Vector3.zero;
        Target.mRB2D.AddForce(-Pos * 20,ForceMode2D.Impulse);
    }

    public void Fliper(Enemy Target,bool isCrit)
    {
        if (isCrit==true)
        {
            StartCoroutine(Target.Stuned(1f));
        }
    }
    public void FrozenMackerel(Enemy Target)
    {
        Vector3 Pos = Player.Instance.transform.position - Target.transform.position;
        Target.mRB2D.velocity = Vector3.zero;
        Target.mRB2D.AddForce(-Pos * 30, ForceMode2D.Impulse);
    }
}
