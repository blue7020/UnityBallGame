using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Button mSettingButton, mMenuExitButton,mStartButton,mAdButton,mScrollButton;
    public Text mScoreText, mVersionText,mHighScoreText, mMenuText, mHighScoreShowText,mHigherText,mStartText,mGameOverText,mReviveText,mMainMenuText,mGoldText,mScrollText;
    public Image mMenu, mBG, mLanguageButton,mGameOverWindow;
    public Sprite[] mBGSprite, mLanguageButtonSprite;
    public bool pause;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mVersionText.text = "Version: " + Application.version;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
            mGoldText.text = "골드: " + GameController.Instance.mGoldText+"G";
        }
        else
        {
            mStartText.text = "Touch to Start";
            mScoreText.text = "Score: " + GameController.Instance.mScore;
            mHighScoreShowText.text = "High Score: " + GameController.Instance.mHighScore;
            mHigherText.text = "Height: " + GameController.Instance.mHeight+"m";
            mGoldText.text = "Gold: " + GameController.Instance.mGoldText + "G";
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
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mHighScoreText.text = "최고 기록 갱신!";
            mStartText.text = "터치하여 시작";
            mGameOverText.text = "게임 오버";
            mScrollText.text = "부활 스크롤\n사용\n수량: "+ SaveDataController.Instance.mUser.RevivalCount + "개";
            mReviveText.text = "광고 후\n부활";
            mMainMenuText.text = "메인\n메뉴";
            mMenuText.text = "메뉴";
        }
        else
        {
            mHighScoreText.text = "High Score!";
            mStartText.text = "Touch to Start";
            mGameOverText.text = "GAME OVER";
            mScrollText.text = "Use Revival\nScroll\nAmount: " + SaveDataController.Instance.mUser.RevivalCount;
            mReviveText.text = "Revive\nwith Ads";
            mMainMenuText.text = "Main\nMenu";
            mMenuText.text = "Menu";
        }

    }

    public void ShowReviveWindow()
    {
        if (GameController.Instance.mReviveToken<1)
        {
            mAdButton.interactable = false;
        }
        else
        {
            mAdButton.interactable = true;
        }
        if (SaveDataController.Instance.mUser.RevivalCount<1)
        {
            mScrollButton.interactable = false;
        }
        else
        {
            mScrollButton.interactable = true;
        }
        mGameOverWindow.gameObject.SetActive(true);
    }

    public void ButtonSound()
    {
        SoundController.Instance.SESound(3);
    }
}