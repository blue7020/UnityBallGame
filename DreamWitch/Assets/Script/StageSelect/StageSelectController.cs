using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageSelectController : InformationLoader
{
    public static StageSelectController Instance;

    public Image mSelectUIImage,mPlayerIcon,mScreenSaver,mMenuWindow,mSoundMenu,mLanguageMenu,mPopupImage;
    public Text mStageTitleText, mStageInfoText, mStageButtonText, mCloseText,mMenuTitleText,mMenuMainButtonText, mMenuLanguageText, mMenuSoundText,mCollectionText,mMenuCloseText;
    public Text mSoundText, mBGMText, mSEText,mBGMVolumeText, mSEVolumeText,mSoundBackText, mLanguageText, mLanguageBackText,mPopupText,mVersionText;
    public Camera mCamera;
    public Transform Top, End;
    public StageInfo[] mInfoArr;
    public IslandSelect[] IslandSelectArr;

    public bool isLanguageCooltime;

    public Button mKorButton, mEngButton;

    public bool isSelectDelay, isShowNewStage,isShowMenu,isShowStage,isShowPopup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Paths.STAGE_INFO);
            mBGMVolumeText.text = SoundController.Instance.BGMVolume.ToString();
            mSEVolumeText.text = SoundController.Instance.SEVolume.ToString();
            TitleController.Instance.NowStage = SaveDataController.Instance.mUser.LastPlayStage;
            StartCoroutine(IslandSelectArr[TitleController.Instance.NowStage].SelectDelay());
            switch (TitleController.Instance.mLanguage)
            {
                case 0:
                    mKorButton.interactable = false;
                    mEngButton.interactable = true;
                    break;
                case 1:
                    mKorButton.interactable = true;
                    mEngButton.interactable = false;
                    break;
                default:
                    break;
            }
            mVersionText.text = TitleController.Instance.mGameVer + "B ver";
            SoundController.Instance.BGMChange(1);
            LanguageSetting();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundController.Instance.VolumeRefresh();
    }

    public void ShowNewStage(int id)
    {
        mSelectUIImage.gameObject.SetActive(false);
        isShowPopup = true;
        isShowStage = false;
        TitleController.Instance.NowStage = id;
        CameraMovement.Instance.mFollowing = false;
        mCamera.transform.position = IslandSelectArr[TitleController.Instance.NowStage].pos;
        IslandSelectArr[TitleController.Instance.NowStage].ShowStage();
        isShowNewStage = false;
    }

    public void ShowStageSelectUI(int id, bool dir)
    {
        TitleController.Instance.NowStage = id;
        if (TitleController.Instance.mLanguage == 0)
        {
            mStageTitleText.text = mInfoArr[TitleController.Instance.NowStage].title_kor;
            mStageInfoText.text = mInfoArr[TitleController.Instance.NowStage].info_kor;
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mStageTitleText.text = mInfoArr[TitleController.Instance.NowStage].title_eng;
            mStageInfoText.text = mInfoArr[TitleController.Instance.NowStage].info_eng;
        }
        if (dir)
        {
            mSelectUIImage.transform.localPosition = new Vector3(-480,6,0);
        }
        else
        {
            mSelectUIImage.transform.localPosition = new Vector3(480,6,0);
        }
        mSelectUIImage.gameObject.SetActive(true);
        mCamera.transform.position = IslandSelectArr[TitleController.Instance.NowStage].pos + new Vector3(0, 0, -10);
        CameraMovement.Instance.mFollowing = false;
        isShowStage = true;
    }

    public void EnterStage()
    {
        SaveDataController.Instance.mUser.LastPlayStage = TitleController.Instance.NowStage;
        switch (TitleController.Instance.NowStage)
        {
            case 0:
                SoundController.Instance.BGMChange(0);
                SaveDataController.Instance.Save(false);
                Loading.Instance.StartLoading(1,false);
                break;
            case 1:
                SoundController.Instance.BGMChange(0);
                SaveDataController.Instance.Save(false);
                Loading.Instance.StartLoading(1, false);
                break;
            case 2:
                isShowPopup = true;
                if (TitleController.Instance.mLanguage==0)
                {
                    mPopupText.text = "...섣불리 가지 말자.\n아직 준비가 필요해.";
                }
                else if (TitleController.Instance.mLanguage == 1)
                {
                    mPopupText.text = "...Don't rush.\nI need to get ready.";
                }
                mPopupImage.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void PopUpClose()
    {
        isShowPopup = false;
        mPopupImage.gameObject.SetActive(false);
    }

    public IEnumerator MenuClose()
    {
        WaitForSeconds delay = new WaitForSeconds(0.35f);
        mMenuWindow.GetComponent<Rigidbody2D>().DOMove(Top.position, 0.3f);
        yield return delay;
        mScreenSaver.gameObject.SetActive(false);
        isShowMenu = false;
        if (isShowStage)
        {
            CameraMovement.Instance.mFollowing = false;
        }
        else
        {
            CameraMovement.Instance.mFollowing = true;
        }
    }

    public void GotoMain()
    {
        Time.timeScale = 1;
        SaveDataController.Instance.Save(true);
        Loading.Instance.StartLoading(0,false);
    }

    public void MenuShow()
    {
        mCollectionText.text = "x" + SaveDataController.Instance.mUser.CollectionAmount.ToString();
        mSoundMenu.gameObject.SetActive(false);
        mLanguageMenu.gameObject.SetActive(false);
        mScreenSaver.gameObject.SetActive(true);
        mMenuWindow.GetComponent<Rigidbody2D>().DOMove(End.position, 0.3f);
        isShowMenu = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)&&!isShowMenu)
        {
            mSelectUIImage.gameObject.SetActive(false);
            isShowStage = false;
            CameraMovement.Instance.mFollowing = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape)&& !isShowPopup)
        {
            if (isShowMenu)
            {
                StartCoroutine(MenuClose());
            }
            else
            {
                if (!isShowStage)
                {
                    CameraMovement.Instance.mFollowing = false;
                }
                MenuShow();
            }
        }
    }

    public void BGMPlus()
    {
        SoundController.Instance.BGMPlus();
        mBGMVolumeText.text = SoundController.Instance.BGMVolume.ToString();
    }
    public void BGMMinus()
    {
        SoundController.Instance.BGMMinus();
        mBGMVolumeText.text = SoundController.Instance.BGMVolume.ToString();
    }
    public void SEPlus()
    {
        SoundController.Instance.SEPlus();
        mSEVolumeText.text = SoundController.Instance.SEVolume.ToString();
    }
    public void SEMinus()
    {
        SoundController.Instance.SEMinus();
        mSEVolumeText.text = SoundController.Instance.SEVolume.ToString();
    }

    public void SetLanguage(int code)
    {
        if (!isLanguageCooltime)
        {
            isLanguageCooltime = true;
            StartCoroutine(LanguageCooltime(code));
        }
    }

    private IEnumerator LanguageCooltime(int code)
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        TitleController.Instance.mLanguage = code;
        mKorButton.interactable = false;
        mEngButton.interactable = false;
        LanguageSetting();
        yield return delay;
        switch (TitleController.Instance.mLanguage)
        {
            case 0:
                mEngButton.interactable = true;
                break;
            case 1:
                mKorButton.interactable = true;
                break;
            default:
                break;
        }
        isLanguageCooltime = false;
    }


    private void LanguageSetting()
    {
        if (TitleController.Instance.mLanguage == 0)
        {
            mStageTitleText.text = mInfoArr[TitleController.Instance.NowStage].title_kor;
            mStageInfoText.text = mInfoArr[TitleController.Instance.NowStage].info_kor;
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mStageTitleText.text = mInfoArr[TitleController.Instance.NowStage].title_eng;
            mStageInfoText.text = mInfoArr[TitleController.Instance.NowStage].info_eng;
        }
        if (TitleController.Instance.mLanguage == 0)
        {
            mStageButtonText.text = "이동하기";
            mCloseText.text = "닫기: X";
            mMenuTitleText.text = "메뉴";
            mMenuCloseText.text = "닫기: ESC";
            mMenuMainButtonText.text = "메인 화면으로";
            mMenuLanguageText.text = "언어 설정";
            mMenuSoundText.text = "소리 설정";
            mLanguageText.text = "언어 설정";
            mSoundText.text = "소리 설정";
            mSoundBackText.text = "뒤로";
            mLanguageBackText.text = "뒤로";
            mBGMText.text = "배경 음악";
            mSEText.text = "효과음";

        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mStageButtonText.text = "Enter";
            mCloseText.text = "Close: X";
            mMenuTitleText.text = "MENU";
            mMenuCloseText.text = "Close: ESC";
            mMenuMainButtonText.text = "To the main screen";
            mMenuLanguageText.text = "Language Setting";
            mMenuSoundText.text = "Sound Setting";
            mLanguageText.text = "Language Setting";
            mSoundText.text = "Sound Setting";
            mSoundBackText.text = "Back";
            mLanguageBackText.text = "Back";
            mBGMText.text = "BGM";
            mSEText.text = "SE";
        }
        SaveDataController.Instance.Save(true);
    }
}
