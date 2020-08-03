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
        }
        else
        {
            Delete();
        }
        LoadJson(out mInfoArr, Path.PLAYER_STAT);
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
            default:
                Debug.LogError("Wrong Player ID");
                break;
        }
        mSkill.transform.SetParent(Player.Instance.gameObject.transform);
        UIController.Instance.ShowItemImage();
        UIController.Instance.ShowWeaponImage();
    }
}
