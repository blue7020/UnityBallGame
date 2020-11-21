using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool pause;
    public bool GotoMain;

    public int StageLevel;//현재 층
    public float StageHP;
    public Sprite[] mStatueSprites;
    public float MaterialDropRate;
    public int ReviveCode;//0== 부활안함 1==광고 2==일반 부활

    public int SyrupInStage;
    public bool IsTutorial;
    private SaveData mUser;

    public List<int> RescueNPCList;

    public List<Weapon> mWeaponList;
    public List<Artifacts> mActiveArtifactList;
    public List<Artifacts> mPassiveArtifactList;
    private List<Artifacts> mArtifactsList;
    private Weapon[] mWeaponArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GotoMain = false;
            pause = false;
            ReviveCode = 0;
            StageLevel = 1;
            SyrupInStage = 0;
            if (!IsTutorial)
            {
                MaterialDropRate = 0.3f;
                StageHP = 2.5f;
                RescueNPCList = new List<int>();
                mArtifactsList = new List<Artifacts>();
                //기본 유물
                for (int i = 0; i < 20; i++)
                {
                    mArtifactsList.Add(GameSetting.Instance.mArtifacts[i]);
                }
                //이벤트 유물들
                if (GameSetting.Instance.NowStage == 7 || SaveDataController.Instance.mUser.ArtifactOpen[0] == true)
                {
                    for (int i = 20; i < 28; i++)
                    {
                        mArtifactsList.Add(GameSetting.Instance.mArtifacts[i]);
                    }
                }
                if (GameSetting.Instance.NowStage == 8 || SaveDataController.Instance.mUser.ArtifactOpen[1] == true)
                {
                    for (int i = 28; i < 36; i++)
                    {
                        mArtifactsList.Add(GameSetting.Instance.mArtifacts[i]);
                    }
                }
                Debug.Log(mArtifactsList.Count);
                mWeaponArr = GameSetting.Instance.mWeaponArr;
            }
            mUser = SaveDataController.Instance.mUser;
        }
        else
        {
            Delete();
        }
    }

    private void Start()
    {
        if (GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (IsTutorial == false)
        {
            SetWeapon();
            SetPassiveArtifacts();
            SetActiveArtifacts();
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
    
    public void SetWeapon()
    {
        mWeaponList = new List<Weapon>();
        for (int i = 0; i < mWeaponArr.Length; i++)
        {
            if (Player.Instance.NowPlayerWeapon != mWeaponArr[i])
            {
                mWeaponList.Add(mWeaponArr[i]);
            }
        }
    }
    public void SetPassiveArtifacts()
    {
        mPassiveArtifactList = new List<Artifacts>();
        for (int i = 0; i < mArtifactsList.Count; i++)
        {
            if (mArtifactsList[i].eType == eArtifactType.Passive)
            {
                mPassiveArtifactList.Add(mArtifactsList[i]);
            }
        }
    }
    public void SetActiveArtifacts()
    {
        mActiveArtifactList = new List<Artifacts>();
        for (int i = 0; i < mArtifactsList.Count; i++)
        {
            if (mArtifactsList[i].eType == eArtifactType.Active)
            {
                if (Player.Instance.NowActiveArtifact!= mArtifactsList[i])
                {
                    mActiveArtifactList.Add(mArtifactsList[i]);
                }
            }
        }
    }

    public void PlusReviveCode()
    {
        ReviveCode = 2;
    }

    public void GamePause()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;

        }
        else
        {
            pause = true;
            Time.timeScale = 0;
        }
    }

    public void MainMenu()
    {
        DestroyController();
        pause = false;
        SaveDataController.Instance.Save();
        Time.timeScale = 1;
        GameSetting.Instance.NowScene = 1;
        GameSetting.Instance.Restart();
        SceneManager.LoadScene(1);
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(0);
    }

    public void MainStart()
    {
        DestroyController();
        pause = false;
        SaveDataController.Instance.Save();
        Time.timeScale = 1;
        GameSetting.Instance.NowScene = 0;
        GameSetting.Instance.Restart();
        SceneManager.LoadScene(0);
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(1);
    }

    public void DestroyController()
    {
        GotoMain = true;

        if (!IsTutorial)
        {
            CameraMovment.Instance.Delete();
            TextEffectPool.Instance.Delete();
            UIController.Instance.Delete();
            InventoryController.Instance.Delete();
            DontDestroyScreen.Instance.Delete();
            AttackPad.Instance.Delete();
            PlayerSkill.Insatnce.Delete();
            Player.Instance.Delete();
            PlayerList.Instance.Delete();
            BuffController.Instance.Delete();
            ArtifactController.Instance.Delete();
            WeaponController.Instance.Delete();
            ActiveArtifacts.Instance.Delete();
            PassiveArtifacts.Instance.Delete();
            StageMaterialController.Instance.Delete();
        }
        else
        {
            CameraMovment.Instance.Delete();
            TextEffectPool.Instance.Delete();
            InventoryController.Instance.Delete();
            PlayerSkill.Insatnce.Delete();
            Player.Instance.Delete();
            PlayerList.Instance.Delete();
            AttackPad.Instance.Delete();
            BuffController.Instance.Delete();
            WeaponController.Instance.Delete();
            ArtifactController.Instance.Delete();
            ActiveArtifacts.Instance.Delete();
            PassiveArtifacts.Instance.Delete();
        }
        Delete();

    }

    private void Update()
    {
        if (mUser.DeveloperID==true)
        {
            if (GotoMain == false && IsTutorial == false)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Player.Instance.mStats.Gold += 200;
                    Player.Instance.mStats.Atk += 5;
                    UIController.Instance.ShowGold();
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    StageLevel = GameSetting.STAGELEVEL_COUNT;
                    Player.Instance.ResetBuff();
                    Player.Instance.Nodamage = false;
                    for (int i = 0; i < BuffEffectController.Instance.EffectList.Count; i++)
                    {
                        BuffEffectController.Instance.EffectList[i].gameObject.SetActive(false);
                    }
                    SceneManager.LoadScene(3);
                    MapNPCController.Instance.NPCSpawn();
                    Player.Instance.transform.position = new Vector2(0, -10.5f);
                }
            }
        }
    }


}