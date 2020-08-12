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
    public Weapon[] mWeapons;

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
        mWeapons = GameSetting.Instance.mWeapons;
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
    private void Mustard(Enemy target)
    {
        target.StartCoroutine(target.SpeedNurf(1.5f,0.5f));
    }

    public void Baguette(Enemy Target)
    {
        int rand = Random.Range(0, 1);
        Target.mRB2D.AddForce(new Vector3(rand,rand,0)*2f, ForceMode2D.Impulse);
    }
}
