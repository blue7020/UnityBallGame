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

    public Text mCashText,mBGMText, mSEText,mPortalNameText;
    public Button mBGMplus, mBGMminus, mSEplus, mSEminus,mPortalButton;
    public GameObject mDoor, mTosterRoom, NameParents;
    public Transform[] PortalName;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            pause = false;
            IsSelect = true;
            for (int i=0; i<mPartsLock.Length;i++)
            {
                if (SaveDataController.Instance.mUser.StagePartsget[i]==true)
                {
                    mPartsLock[i].gameObject.SetActive(false);
                }
            }
            ShowSyrupText();
            if (SaveDataController.Instance.mUser.GameClear==true)
            {
                mDoor.gameObject.SetActive(false);
                mTosterRoom.gameObject.SetActive(true);
            }
            else
            {
                mDoor.gameObject.SetActive(true);
                mTosterRoom.gameObject.SetActive(false);
            }
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
        for (int i=0; i<PortalName.Length;i++)
        {
            if (SaveDataController.Instance.mUser.StageOpen[i]==true)
            {
                Text text = Instantiate(mPortalNameText, NameParents.transform);
                if (GameSetting.Instance.Language==0)
                {
                    text.text = (i + 1)+"." + GameSetting.Instance.mMapInfoArr[i + 1].Title;
                }
                else if(GameSetting.Instance.Language==1)
                {
                    text.text = (i + 1) +"."+ GameSetting.Instance.mMapInfoArr[i + 1].EngTitle;
                }
                text.transform.localScale = new Vector3(0.07f,0.07f,1);
                text.transform.position = PortalName[i].transform.position + new Vector3 (0, 1.2f,0);
            }
        }
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
        mCashText.text = SaveDataController.Instance.mUser.Syrup.ToString();
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
