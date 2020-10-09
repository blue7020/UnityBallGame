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
    public int StageHP;

    public float MaterialDropRate;
    public int ReviveCode;//0== 부활안함 1==광고 2==일반 부활

    public int SyrupInStage;
    public bool IsTutorial;
    public GameObject BulletTrash;

    public List<int> RescueNPCList;

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
                StageHP = 3;
                RescueNPCList = new List<int>();
            }
        }
        else
        {
            Delete();
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    public void PlusReviveCode()
    {
        ReviveCode = 2;
    }
    private void Start()
    {
        if (GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
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
        Time.timeScale = 1;
        GameSetting.Instance.NowScene = 1;
        GameSetting.Instance.Restart();
        //TODO 저장기능
        SceneManager.LoadScene(1);
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(0);
    }

    public void MainStart()
    {
        DestroyController();
        pause = false;
        Time.timeScale = 1;
        GameSetting.Instance.NowScene = 0;
        GameSetting.Instance.Restart();
        //TODO 저장기능
        SceneManager.LoadScene(0);
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(0);
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
        if (GotoMain == false&&IsTutorial==false)
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