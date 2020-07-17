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
        StageHP = 4;
        GotoMain = false;
        pause = false;
        UIController.Instance.CharacterImage();
    }

    private void Start()
    {
        if (GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        MapLevel = 1;//나중에 맵 선택 시 해당 레벨을 부여하는 것으로 수정

        Level = 1;
    }
    public void Delete()
    {
        Destroy(gameObject);
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
        Debug.LogError("GameOver!");
    }

    public void MainMenu()
    {
        GotoMain = true;
        CameraMovment.Instance.Delete();
        UIController.Instance.Delete();
        DontDestroyScreen.Instance.Delete();
        //Skill.Instance.Delete(); TODO 플레이어 스킬 스크립트 추가
        Player.Instance.Delete();
        Delete();
        pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (GotoMain == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Player.Instance.mStats.Gold += 200;
                Player.Instance.mStats.Atk += 5;
                UIController.Instance.ShowGold();
            }
        }
    }

}