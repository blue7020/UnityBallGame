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
            Destroy(gameObject);
        }
        LoadJson(out mInfoArr, Path.PLAYER_STAT);
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
                UIController.Instance.CharacterImage();
                Player.Instance.mStats = mInfoArr[0];
                p0.joyskick = stick;
                Player.Instance.NowPlayerWeapon = WeaponPool.Instance.GetFromPool(0);
                break;
            case 1:
                Player p1 = Instantiate(mPlayer[1], Vector3.zero, Quaternion.identity);
                UIController.Instance.CharacterImage();
                Player.Instance.mStats = mInfoArr[1];
                p1.joyskick = stick;
                Player.Instance.NowPlayerWeapon = WeaponPool.Instance.GetFromPool(1);
                break;
            default:
                Debug.LogError("Wrong Player ID");
                break;
        }
    }
}
