using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class MainLobbyUIController : MonoBehaviour
{

    public static MainLobbyUIController Instance;

    public MainLobbyPlayer[] mPlayerList;
    private bool pause;

    public Image[] mParts;
    public Image[] mPartsLock;
    public Text mCashText,mBGMText, mSEText;
    //public Tooltip tooltip;



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
        pause = false;
        ShowSyrupText();
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

    public void MainStart()
    {
        //TODO 저장기능
        pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void BGMPlus()
    {
        if (GameSetting.Instance.BGMSetting < 10)
        {
            GameSetting.Instance.BGMSetting++;
        }
        else
        {
            GameSetting.Instance.BGMSetting = 10;
        }
        mBGMText.text = GameSetting.Instance.BGMSetting.ToString();
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

    public void ShowSyrupText()
    {
        mCashText.text = GameSetting.Instance.Syrup.ToString();
    }

    public void ShowParts()
    {
        for (int i=0; i < 6; i++)
        {
            if (GameSetting.Instance.StagePartsget[i]==true)
            {
                mPartsLock[i].color = Color.clear;
            }
        }
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
