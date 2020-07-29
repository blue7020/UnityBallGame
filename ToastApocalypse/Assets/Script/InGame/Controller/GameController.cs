using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool pause;
    public bool GotoMain;

    public int Level;//현재 층
    public int MapLevel;//현재 스테이지
    public int StageHP;

    public int SyrupInStage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StageHP = 4;
            MapLevel = GameSetting.Instance.NowStage;
            GotoMain = false;
            pause = false;
            Level = 1;
            SyrupInStage = 0;
            UIController.Instance.CharacterImage();
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

    public void GameOver()
    {

    }

    public void MainMenu()
    {
        DestroyController();
        pause = false;
        Time.timeScale = 1;
        GotoMain = false;
        GameSetting.Instance.Restart();
        SceneManager.LoadScene(1);
    }

    public void DestroyController()
    {
        GotoMain = true;
        CameraMovment.Instance.Delete();
        UIController.Instance.Delete();
        InventoryController.Instance.Delete();
        DontDestroyScreen.Instance.Delete();
        PlayerSkill.Insatnce.Delete();
        SkillController.Instance.Delete();
        Player.Instance.Delete();
        PlayerList.Instance.Delete();
        ItemList.Instance.Delete();
        WeaponController.Instance.Delete();
        ArtifactController.Instance.Delete();
        WeaponController.Instance.Delete();
        Delete();
    }

    private void Update()
    {
        if (GotoMain == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Player.Instance.mStats.Gold += 200;
                Player.Instance.mStats.Atk += 5;
                Level = 4;
                UIController.Instance.ShowGold();
            }
        }
    }

}