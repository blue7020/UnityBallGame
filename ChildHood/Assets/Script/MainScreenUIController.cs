using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenUIController : MonoBehaviour
{
    public static MainScreenUIController Instance;

#pragma warning disable 0649
    public Image ScreenLoadDelay;
    public Button mStartButton;
    [SerializeField]
    private Text mStartText, mBGMText, mSEText;
    [SerializeField]
    private Button mEngButton, mKorButton;
#pragma warning restore 0649

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
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        ScreenLoadDelay.gameObject.SetActive(false);
        mStartButton.gameObject.SetActive(true);
        yield return delay;
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
        SceneManager.LoadScene(1);
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
            Application.Quit();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
