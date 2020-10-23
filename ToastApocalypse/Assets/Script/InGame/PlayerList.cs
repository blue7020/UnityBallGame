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
    public Weapon mWeapon;
    public Player player;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.PLAYER_STAT);
            if (GameSetting.Instance.NowStage!=0)
            {
                player = Instantiate(mPlayer[GameSetting.Instance.PlayerID], Vector3.zero, Quaternion.identity);
                player.mStats = mInfoArr[GameSetting.Instance.PlayerID];
            }
            else
            {
                player = Instantiate(mPlayer[0], Vector3.zero, Quaternion.identity);
                player.mStats = mInfoArr[0];
            }
            player.NowPlayerSkill = mSkill;
            mSkill.transform.SetParent(player.gameObject.transform);
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
        if (GameController.Instance.IsTutorial == false)
        {
            mSkill.mID = GameSetting.Instance.PlayerSkillID;
            mWeapon = Instantiate(GameSetting.Instance.mWeaponArr[GameSetting.Instance.PlayerWeaponID], player.transform);
            player.NowPlayerWeapon = mWeapon;
            player.NowPlayerWeapon.EquipWeapon();
            UIController.Instance.CharacterImage();
            UIController.Instance.ShowSkillImage();
            UIController.Instance.ShowItemImage();
            UIController.Instance.ShowWeaponImage();
        }
        else
        {
            TutorialUIController.Instance.CharacterImage();
        }
        CameraMovment.Instance.PlayerSetting(Player.Instance.gameObject);
        player.joyskick = stick;
    }
}
