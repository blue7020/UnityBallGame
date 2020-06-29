using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool pause;
    public bool GotoMain;

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
        GotoMain = false;
        pause = false;
        UIController.Instance.CharacterImage();
    }

    private void Start()
    {
        if (GotoMain==false)
        {
            DontDestroyOnLoad(gameObject);
        }
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

    public void MainMenu()
    {
        GotoMain = true;
        CameraMovment.Instance.Delete();
        UIController.Instance.Delete();
        DontDestroyScreen.Instance.Delete();
        //Skill.Instance.Delete(); TODO 플레이어 스킬 스크립트 추가
        Player.Instance.Delete();
        Delete();
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (GotoMain==false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Player.Instance.mInfoArr[Player.Instance.mID].Gold += 200;
                Player.Instance.mInfoArr[Player.Instance.mID].Atk += 5;
            }
        }
    }

}