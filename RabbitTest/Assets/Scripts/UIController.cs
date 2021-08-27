using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Button mSettingButton, mMenuExitButton,mStartButton,mGameOverButton;
    public Text mScoreText, mVersionText,mHighScoreText, mHighScoreShowText,mHigherText,mStartText,mGameOverText;
    public Image mMenu, mBG, mLanguageButton;
    public Sprite[] mBGSprite, mLanguageButtonSprite;
    public bool pause;
    public float mVersion;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mVersionText.text = "Version: "+mVersion;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mStartText.text = "터치하여 시작";
            mGameOverText.text = "게임 오버";
        }
        else
        {
            mStartText.text = "Touch to Start";
            mGameOverText.text = "GAME OVER";
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
            mHighScoreShowText.text = "최고 점수: " + GameController.Instance.mHighScore;
            mHigherText.text = "높이: " + GameController.Instance.mHeight+"m";
        }
        else
        {
            mStartText.text = "Touch to Start";
            mScoreText.text = "Score: " + GameController.Instance.mScore;
            mHighScoreShowText.text = "High Score: " + GameController.Instance.mHighScore;
            mHigherText.text = "Height: " + GameController.Instance.mHeight+"m";
        }
    }

    public void LanguageButtonRefresh()
    {
        if (SaveDataController.Instance.mLanguage == 1)//if now Korean
        {
            mLanguageButton.sprite = mLanguageButtonSprite[0];
        }
        else
        {
            mLanguageButton.sprite = mLanguageButtonSprite[1];

        }
    }

    public void LanguageChange()
    {
        if (SaveDataController.Instance.mLanguage == 1)//if now Korean
        {
            SaveDataController.Instance.mLanguage = 0;
        }
        else
        {
            SaveDataController.Instance.mLanguage = 1;
        }
        LanguageButtonRefresh();
        LanguageRefresh();
    }


    public void ChangeBG()
    {
        SoundController.Instance.SESound(4);
        mBG.sprite = mBGSprite[GameController.Instance.mStage];
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
        RankingController.Instance.RecodeRank();
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
            mGameOverText.text = "게임 오버";
        }
        else
        {
            mHighScoreText.text = "High Score!";
            mGameOverText.text = "GAME OVER";
        }
    }

    public void ButtonSound()
    {
        SoundController.Instance.SESound(3);
    }
}