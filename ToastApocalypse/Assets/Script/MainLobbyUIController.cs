using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLobbyUIController : MonoBehaviour
{

    public static MainLobbyUIController Instance;

    public MainLobbyPlayer[] mPlayerList;
    public Image[] mPartsLock;
    private bool pause;

    public Text mCashText,mBGMText, mSEText;
    public Button mBGMplus, mBGMminus, mSEplus, mSEminus;
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

    private void Start()
    {
        mBGMplus.onClick.AddListener(() => { BGMPlus(); });
        mBGMminus.onClick.AddListener(() => { BGMMinus(); });
        mSEplus.onClick.AddListener(() => { SEPlus(); });
        mSEminus.onClick.AddListener(() => { SEMinus(); });
        GameSetting.Instance.ShowParts();
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            MainStart();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }


    //TODO 나중에 제대로 캐릭터 선택 구현하면 고치거나 삭제 바람
    public void Toast()
    {
        GameSetting.Instance.PlayerID = 0;
    }
    public void Hamegg()
    {
        GameSetting.Instance.PlayerID = 1;
    }
}
