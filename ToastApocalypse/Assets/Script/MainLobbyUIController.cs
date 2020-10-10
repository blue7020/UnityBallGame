using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLobbyUIController : MonoBehaviour
{

    public static MainLobbyUIController Instance;
    public Image[] mPartsLock;
    private bool pause;
    public bool IsSelect;

    public Text mCashText,mBGMText, mSEText;
    public Button mBGMplus, mBGMminus, mSEplus, mSEminus,mPortalButton;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            pause = false;
            IsSelect = true;
            for (int i=0; i<mPartsLock.Length;i++)
            {
                if (GameSetting.Instance.StagePartsget[i]==true)
                {
                    mPartsLock[i].gameObject.SetActive(false);
                }
            }
            ShowSyrupText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mBGMplus.onClick.AddListener(() => { BGMPlus(); });
        mBGMminus.onClick.AddListener(() => { BGMMinus(); });
        mSEplus.onClick.AddListener(() => { SEPlus(); });
        mSEminus.onClick.AddListener(() => { SEMinus(); });
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }

    public void ButtonPush()
    {
        SoundController.Instance.SESoundUI(0);
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
        GameSaver.Instance.GameSave();
        pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        GameSetting.Instance.NowScene = 0;
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(0);
    }

    public void BGMPlus()
    {
        SoundController.Instance.PlusBGM();
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
    }
    public void BGMMinus()
    {
        SoundController.Instance.MinusBGM();
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
    }

    public void SEPlus()
    {
        SoundController.Instance.PlusSE();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }
    public void SEMinus()
    {
        SoundController.Instance.MinusSE();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }

    public void ShowSyrupText()
    {
        mCashText.text = GameSetting.Instance.Syrup.ToString();
    }

    int ClickCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& IsSelect==true)
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2 && IsSelect == true)
        {
            MainStart();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
