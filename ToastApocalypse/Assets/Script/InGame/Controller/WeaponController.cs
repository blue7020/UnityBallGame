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
        }
        else
        {
            Delete();
        }
        LoadJson(out mInfoArr, Path.WEAPON_STAT);
        for (int i=0; i<GameSetting.Instance.mWeapons.Length;i++)
        {
            mWeapons.Add(GameSetting.Instance.mWeapons[i]);
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

    public void WeaponSkill(int WeaponID,Enemy Target)
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

        }
    }

    //WeaponSkillData

    public void Mustard(Enemy Target)
    {
        //slow
    }
    public void Baguette(Enemy Target)
    {
        //Target.mRB2D.AddForce((Target.transform.position)/2, ForceMode2D.Impulse);
    }
}
