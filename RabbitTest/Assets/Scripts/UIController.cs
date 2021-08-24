﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Button mSettingButton, mMenuExitButton,mStartButton;
    public Text mScoreText, mVersionText,mHighScoreText,mHigherText,mStartText;
    public Image mMenu;
    public Button[] mLanguageButton;
    public bool pause;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (SaveDataController.Instance.mLanguage == 1)//korean
            {
                mStartText.text = "터치하여 시작";
            }
            else
            {
                mStartText.text = "Touch to Start";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SaveDataController.Instance.mLanguage == 1)//if now Korean
        {
            mLanguageButton[1].gameObject.SetActive(true);
            mLanguageButton[0].gameObject.SetActive(false);
        }
        else
        {
            mLanguageButton[0].gameObject.SetActive(true);
            mLanguageButton[1].gameObject.SetActive(false);
        }
        LanguageRefresh();
        pause = true;
        Time.timeScale = 0;
    }

    public void ScoreRefresh()
    {
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mStartText.text = "터치하여 시작";
            mScoreText.text = "점수: "+GameController.Instance.mScore;
            mHigherText.text = "높이: " + GameController.Instance.mHeight+"m";
        }
        else
        {
            mStartText.text = "Touch to Start";
            mScoreText.text = "Score: " + GameController.Instance.mScore;
            mHigherText.text = "Height: " + GameController.Instance.mHeight+"m";
        }
    }
    
    public void LanguageChange()
    {
        if (SaveDataController.Instance.mLanguage==1)//if now Korean
        {
            mLanguageButton[1].gameObject.SetActive(true);
            mLanguageButton[0].gameObject.SetActive(false);
            SaveDataController.Instance.mLanguage = 0;
        }
        else
        {
            mLanguageButton[0].gameObject.SetActive(true);
            mLanguageButton[1].gameObject.SetActive(false);
            SaveDataController.Instance.mLanguage = 1;
        }
        LanguageRefresh();
    }


    public void HighScoreMessage()
    {
        mHighScoreText.gameObject.SetActive(true);
    }

    public void GamePause()
    {
        if (pause)
        {
            pause = false;
            mMenu.gameObject.SetActive(false);
            Time.timeScale = 1;

        }
        else
        {
            pause = true;
            mMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void HideStartButton()
    {
        mStartButton.gameObject.SetActive(false);
        pause = false;
        Time.timeScale = 1;
    }

    public void GameExit()
    {
        Time.timeScale = 1;
        SaveDataController.Instance.mUser.HighScore = GameController.Instance.mHighScore;
        SaveDataController.Instance.Save();
        SceneManager.LoadScene(0);
    }

    public void LanguageRefresh()
    {
        ScoreRefresh();
        if (SaveDataController.Instance.mUser.Language == 1)
        {
            mHighScoreText.text = "최고 기록 갱신!";
        }
        else
        {
            mHighScoreText.text = "High Score!";
        }
    }

    public void ButtonSound()
    {
        SoundController.Instance.SESound(3);
    }
}
