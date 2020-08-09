using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenUIController : MonoBehaviour
{
    public static MainScreenUIController Instance;

    public Text mStartText,mEndText, mBGMText, mSEText;
    public Button mEngButton, mKorButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameSetting.Instance.Language==0)
        {
            mStartText.text = "게임 시작";
            mEndText.text = "게임 종료";
            mKorButton.gameObject.SetActive(false);
            mEngButton.gameObject.SetActive(true);

        }
        else if(GameSetting.Instance.Language == 1)
        {
            mStartText.text = "Start";
            mEndText.text = "Quit";
            mEngButton.gameObject.SetActive(false);
            mKorButton.gameObject.SetActive(true);
        }
    }

    //0 = 한국어 / 1 = 영어
    public void LanguagetoKor()
    {
        mEngButton.gameObject.SetActive(false);
        mKorButton.gameObject.SetActive(true);
        GameSetting.Instance.Language = 1;
        mStartText.text = "Start";
        mEndText.text = "Quit";
    }
    public void LanguagetoEng()
    {
        mKorButton.gameObject.SetActive(false);
        mEngButton.gameObject.SetActive(true);
        GameSetting.Instance.Language = 0;
        mStartText.text = "게임 시작";
        mEndText.text = "게임 종료";
    }

    public void BGMPlus()
    {
        if (GameSetting.Instance.BGMSetting<10)
        {
            GameSetting.Instance.BGMSetting++;
        }
        else
        {
            GameSetting.Instance.BGMSetting=10;
        }
        mBGMText.text =GameSetting.Instance.BGMSetting.ToString();
    }
    public void BGMMinus()
    {
        if (GameSetting.Instance.BGMSetting > 0)
        {
            GameSetting.Instance.BGMSetting--;
        }
        else
        {
            GameSetting.Instance.BGMSetting = 0;
        }
        mBGMText.text = GameSetting.Instance.BGMSetting.ToString();
    }
    public void SEPlus()
    {
        if (GameSetting.Instance.SESetting < 10)
        {
            GameSetting.Instance.SESetting++;
        }
        else
        {
            GameSetting.Instance.SESetting = 10;
        }
        mSEText.text = GameSetting.Instance.SESetting.ToString();
    }
    public void SEMinus()
    {
        if (GameSetting.Instance.SESetting > 0)
        {
            GameSetting.Instance.SESetting--;
        }
        else
        {
            GameSetting.Instance.SESetting = 0;
        }
        mSEText.text = GameSetting.Instance.SESetting.ToString();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(2);
    }

    public void GameQuit()
    {
        Application.Quit();
    }


    int ClickCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            GameQuit();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
