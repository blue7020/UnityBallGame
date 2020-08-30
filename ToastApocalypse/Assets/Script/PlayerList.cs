using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : InformationLoader
{
    public static PlayerList Instance;

    public Player[] mPlayer;
    public PlayerSkill mSkill;

    public VirtualJoyStick stick;
    public PlayerStat[] mInfoArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.PLAYER_STAT);
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        mSkill.mID = GameSetting.Instance.PlayerSkillID;
        switch (GameSetting.Instance.PlayerID)
        {
            case 0:
                Player p0 = Instantiate(mPlayer[0], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[0];
                p0.joyskick = stick;
                UIController.Instance.CharacterImage();
                CameraMovment.Instance.PlayerSetting(p0.gameObject);
                Weapon mWeapon0 = WeaponPool.Instance.GetFromPool(0);
                p0.NowPlayerWeapon = mWeapon0;
                p0.NowPlayerWeapon.EquipWeapon();
                p0.NowPlayerSkill = mSkill;
                break;
            case 1:
                Player p1 = Instantiate(mPlayer[1], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[1];
                p1.joyskick = stick;
                UIController.Instance.CharacterImage();               
                CameraMovment.Instance.PlayerSetting(p1.gameObject);
                Weapon mWeapon1 = WeaponPool.Instance.GetFromPool(1);
                p1.NowPlayerWeapon = mWeapon1;
                p1.NowPlayerWeapon.EquipWeapon();
                p1.NowPlayerSkill = mSkill;
                break;
            case 2:
                Player p2 = Instantiate(mPlayer[2], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[2];
                p2.joyskick = stick;
                UIController.Instance.CharacterImage();
                CameraMovment.Instance.PlayerSetting(p2.gameObject);
                Weapon mWeapon2 = WeaponPool.Instance.GetFromPool(3);
                p2.NowPlayerWeapon = mWeapon2;
                p2.NowPlayerWeapon.EquipWeapon();
                p2.NowPlayerSkill = mSkill;
                break;
            case 3:
                Player p3 = Instantiate(mPlayer[3], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[3];
                p3.joyskick = stick;
                UIController.Instance.CharacterImage();
                CameraMovment.Instance.PlayerSetting(p3.gameObject);
                Weapon mWeapon3 = WeaponPool.Instance.GetFromPool(7);
                p3.NowPlayerWeapon = mWeapon3;
                p3.NowPlayerWeapon.EquipWeapon();
                p3.NowPlayerSkill = mSkill;
                break;
            case 4:
                Player p4 = Instantiate(mPlayer[4], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[4];
                p4.joyskick = stick;
                UIController.Instance.CharacterImage();
                CameraMovment.Instance.PlayerSetting(p4.gameObject);
                Weapon mWeapon4 = WeaponPool.Instance.GetFromPool(10);
                p4.NowPlayerWeapon = mWeapon4;
                p4.NowPlayerWeapon.EquipWeapon();
                p4.NowPlayerSkill = mSkill;
                break;
            case 5:
                Player p5 = Instantiate(mPlayer[5], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[5];
                p5.joyskick = stick;
                UIController.Instance.CharacterImage();
                CameraMovment.Instance.PlayerSetting(p5.gameObject);
                Weapon mWeapon5 = WeaponPool.Instance.GetFromPool(11);
                p5.NowPlayerWeapon = mWeapon5;
                p5.NowPlayerWeapon.EquipWeapon();
                p5.NowPlayerSkill = mSkill;
                break;
            default:
                Debug.LogError("Wrong Player ID");
                break;
        }
        mSkill.transform.SetParent(Player.Instance.gameObject.transform);
        UIController.Instance.ShowSkillImage();
        UIController.Instance.ShowItemImage();
        UIController.Instance.ShowWeaponImage();
    }
}
