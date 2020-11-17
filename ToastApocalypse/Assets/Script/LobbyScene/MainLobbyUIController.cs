using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLobbyUIController : MonoBehaviour
{

    public static MainLobbyUIController Instance;
    public Image[] mPartsLock;
    public Image mCutSceneImage;
    public OpeningCutScene mCutScene;
    private bool pause;
    public bool IsSelect;

    public Text mCashText,mBGMText, mSEText,mPortalNameText;
    public Button mBGMplus, mBGMminus, mSEplus, mSEminus,mPortalButton;
    public GameObject mDoor, mTosterRoom, NameParents;
    public ToasterPedestal mToster;
    public Transform[] PortalName;
    public PopUpWindow mPopupWindow;
    public string text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            pause = false;
            IsSelect = true;
            SaveDataController.Instance.Save();
            for (int i=0; i<mPartsLock.Length;i++)
            {
                if (SaveDataController.Instance.mUser.StagePartsget[i]==true)
                {
                    mPartsLock[i].gameObject.SetActive(false);
                }
            }
            ShowSyrupText();
            ShowToaterRoom();

            if (SaveDataController.Instance.mUser.StageOpen[7]==false)
            {
                SaveDataController.Instance.mUser.StageOpen[7] = true;
                if (GameSetting.Instance.Language==0)
                {
                    text = "새 스테이지가 개방되었습니다: 고요한 밤\n난이도: 보통";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    text = "New stage open: Silent Night\nDifficulty level: Normal";
                }
                mPopupWindow.ShowWindow(text);
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
                if (i<6)
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        text.text = (i + 1) + "." + GameSetting.Instance.mMapInfoArr[i + 1].Title;
                    }
                    else if (GameSetting.Instance.Language == 1)
                    {
                        text.text = (i + 1) + "." + GameSetting.Instance.mMapInfoArr[i + 1].EngTitle;
                    }
                }
                else
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        text.text = "이벤트: " + GameSetting.Instance.mMapInfoArr[i + 1].Title;
                    }
                    else if (GameSetting.Instance.Language == 1)
                    {
                        text.text = "Event: " + GameSetting.Instance.mMapInfoArr[i + 1].EngTitle;
                    }
                }
                text.transform.localScale = new Vector3(0.07f,0.07f,1);
                text.transform.position = PortalName[i].transform.position + new Vector3 (0, 1.2f,0);
            }
        }
    }

    public void ShowToaterRoom()
    {
        if (SaveDataController.Instance.mUser.GameClear == true)
        {
            mDoor.gameObject.SetActive(false);
            mTosterRoom.gameObject.SetActive(true);
            mToster.gameObject.SetActive(true);
        }
        else
        {
            mDoor.gameObject.SetActive(true);
            mTosterRoom.gameObject.SetActive(false);
            mToster.gameObject.SetActive(true);
        }
    }

    public void ShowOpeningWindow()
    {
        mCutScene.OpeningSetting();
        mCutScene.gameObject.SetActive(true);
        mCutScene.ShowOpening();
    }

    public void OpeningSkip()
    {
        mCutScene.gameObject.SetActive(false);
        SoundController.Instance.BGMChange(0);
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
        SoundController.Instance.BGMChange(1);
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

}
