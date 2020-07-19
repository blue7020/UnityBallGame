using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : InformationLoader
{
    public static PlayerList Instance;

    public Player[] mPlayer;

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
                p0.NowPlayerWeapon = WeaponPool.Instance.GetFromPool(0);
                p0.NowPlayerWeapon.EquipWeapon();
                UIController.Instance.CharacterImage();
                UIController.Instance.ShowWeaponImage();
                CameraMovment.Instance.PlayerSetting(p0.gameObject);
                break;
            case 1:
                Player p1 = Instantiate(mPlayer[1], Vector3.zero, Quaternion.identity);
                Player.Instance.mStats = mInfoArr[1];
                p1.joyskick = stick;
                p1.NowPlayerWeapon = WeaponPool.Instance.GetFromPool(1);
                p1.NowPlayerWeapon.EquipWeapon();
                UIController.Instance.CharacterImage();
                UIController.Instance.ShowWeaponImage();
                CameraMovment.Instance.PlayerSetting(p1.gameObject);
                break;
            default:
                Debug.LogError("Wrong Player ID");
                break;
        }
        UIController.Instance.ShowItemImage();
    }
}
