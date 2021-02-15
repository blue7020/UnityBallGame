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

    public Text mCashText,mBGMText, mSEText,mPortalNameText,text,eventtext;
    public Button mBGMplus, mBGMminus, mSEplus, mSEminus,mPortalButton;
    public GameObject mDoor, mTosterRoom, NameParents;
    public ToasterPedestal mToster;
    public Transform[] PortalName;
    public PopUpWindow mPopupWindow;
    public string mtext;
    public int NowEventStageNum;

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

            if (SaveDataController.Instance.mUser.CharacterOpen[16]==false)
            {
                SaveDataController.Instance.mUser.CharacterOpen[16] = true;
            }
            if (SaveDataController.Instance.mUser.WeaponOpen[34]==false&& SaveDataController.Instance.mUser.WeaponOpen[35] == false)
            {
                SaveDataController.Instance.mUser.WeaponOpen[34] = true;
                SaveDataController.Instance.mUser.WeaponOpen[35] = true;
                if (GameSetting.Instance.Language==0)
                {
                    mtext = "새 캐릭터 1종과 무기 2종이 추가되었습니다! 확인해보세요!";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mtext = "1 new character and 2 new weapons have been added! Check it out!";
                }
                mPopupWindow.ShowWindow(mtext);
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
                if (i<6)
                {
                    text = Instantiate(mPortalNameText, NameParents.transform);
                    if (GameSetting.Instance.Language == 0)
                    {
                        text.text = (i + 1) + "." + GameSetting.Instance.mMapInfoArr[i + 1].Title;
                    }
                    else if (GameSetting.Instance.Language == 1)
                    {
                        text.text = (i + 1) + "." + GameSetting.Instance.mMapInfoArr[i + 1].EngTitle;
                    }
                    text.transform.localScale = new Vector3(0.07f, 0.07f, 1);
                    text.transform.position = PortalName[i].transform.position + new Vector3(0, 1.2f, 0);
                }
                else
                {

                }
            }
        }
        PortalTextRefresh();
    }

    public void PortalTextRefresh()
    {
        if (SaveDataController.Instance.mUser.NowEventMapID == 0)
        {
            SaveDataController.Instance.mUser.NowEventMapID = 2;
            Debug.Log(SaveDataController.Instance.mUser.NowEventMapID);
        }
        int map = 6 + SaveDataController.Instance.mUser.NowEventMapID;
        if (eventtext!=null)
        {
            Destroy(eventtext);
        }
        eventtext = Instantiate(mPortalNameText, NameParents.transform);
        eventtext.gameObject.SetActive(true);
        if (GameSetting.Instance.Language == 0)
        {
            eventtext.text = "이벤트: " + GameSetting.Instance.mMapInfoArr[map].Title;
        }
        else if (GameSetting.Instance.Language == 1)
        {
            eventtext.text = "Event: " + GameSetting.Instance.mMapInfoArr[map].EngTitle;
        }
        eventtext.transform.localScale = new Vector3(0.07f, 0.07f, 1);
        eventtext.transform.position = PortalName[6].transform.position + new Vector3(0, 1.2f, 0);
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
