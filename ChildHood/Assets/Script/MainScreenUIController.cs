using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenUIController : MonoBehaviour
{
    public static MainScreenUIController Instance;

    [SerializeField]
    private Text mStartText, mBGMText, mSEText;

    [SerializeField]
    private Button mEngButton, mKorButton;

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
        mKorButton.gameObject.SetActive(false);
    }

    //0 = 한국어 / 1 = 영어
    public void LanguagetoKor()
    {
        mEngButton.gameObject.SetActive(false);
        mKorButton.gameObject.SetActive(true);
        GameSetting.Instance.Language = 1;
        mStartText.text = "Touch to Start";
    }
    public void LanguagetoEng()
    {
        mKorButton.gameObject.SetActive(false);
        mEngButton.gameObject.SetActive(true);
        GameSetting.Instance.Language = 0;
        mStartText.text = "화면을 터치해주세요";
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
        //SceneManager.LoadScene(1);
    }

    public void Toast()
    {
        GameSetting.Instance.PlayerID = 0;
    }
    public void Hamegg()
    {
        GameSetting.Instance.PlayerID = 1;
    }
}
