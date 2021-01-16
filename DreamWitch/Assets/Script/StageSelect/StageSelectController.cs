using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectController : InformationLoader
{
    public static StageSelectController Instance;

    public Image mSelectUIImage,mPlayerIcon,mScreenSaver,mMenuWindow;
    public Text mStageTitleText, mStageInfoText, mStageButtonText, mCloseText,mMenuTitleText,mMenuMainButtonText, mMenuLanguageText, mMenuSoundText,mCollectionText;
    public Camera mCamera;
    public Transform Top, End;
    public StageInfo[] mInfoArr;
    public IslandSelect[] IslandSelectArr;

    public bool isSelectDelay, isShowNewStage,isShowMenu,isShowStage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.STAGE_INFO);
            for (int i = 0; i < IslandSelectArr.Length; i++)
            {
                if (SaveDataController.Instance.mUser.StageShow[i] == false)
                {
                    IslandSelectArr[i].gameObject.SetActive(false);
                }
            }
            if (TitleController.Instance.mLanguage == 0)
            {
                mStageButtonText.text = "이동하기";
                mCloseText.text = "닫기: X";
                mMenuTitleText.text = "메뉴";
                mMenuMainButtonText.text = "메인 화면으로";
                mMenuLanguageText.text = "언어";
                mMenuSoundText.text = "소리";
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                mStageButtonText.text = "Enter";
                mCloseText.text = "Close: X";
                mMenuTitleText.text = "MENU";
                mMenuMainButtonText.text = "To the main screen";
                mMenuLanguageText.text = "Language";
                mMenuSoundText.text = "Sound";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (isShowNewStage)
        {
            mCamera.transform.position = IslandSelectArr[TitleController.Instance.NowStage].pos + new Vector3(0, 0, -10); ;
            IslandSelectArr[TitleController.Instance.NowStage].ShowStage();
            StartCoroutine(CameraMovement.Instance.CameraFollowDelay(2.2f));
        }
        else
        {
            TitleController.Instance.NowStage = SaveDataController.Instance.mUser.LastPlayStage;
            StartCoroutine(IslandSelectArr[TitleController.Instance.NowStage].SelectDelay());
        }
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
            mSelectUIImage.transform.localPosition = new Vector3(-632,6,0);
        }
        else
        {
            mSelectUIImage.transform.localPosition = new Vector3(632,6,0);
        }
        mSelectUIImage.gameObject.SetActive(true);
        mCamera.transform.position = IslandSelectArr[TitleController.Instance.NowStage].pos + new Vector3(0, 0, -10);
        CameraMovement.Instance.mFollowing = false;
        isShowStage = true;
    }

    public void EnterStage()
    {
        SaveDataController.Instance.mUser.LastPlayStage = TitleController.Instance.NowStage;
        SaveDataController.Instance.Save();
        switch (TitleController.Instance.NowStage)
        {
            case 0:
                SoundController.Instance.BGMChange(0);
                break;
            default:
                break;
        }
        SceneManager.LoadScene(1);
    }

    public IEnumerator MenuClose()
    {
        WaitForSeconds delay = new WaitForSeconds(0.35f);
        mMenuWindow.GetComponent<Rigidbody2D>().DOMove(Top.position, 0.3f);
        yield return delay;
        mScreenSaver.gameObject.SetActive(false);
        isShowMenu = false;
        if (!isShowStage)
        {
            CameraMovement.Instance.mFollowing = false;
        }
    }

    public void GotoMain()
    {
        SaveDataController.Instance.Save();
        SceneManager.LoadScene(0);
    }

    public void ShowCollection()
    {
        mCollectionText.text = "x" + SaveDataController.Instance.mUser.CollectionAmount.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)&&!isShowMenu)
        {
            mSelectUIImage.gameObject.SetActive(false);
            isShowStage = false;
            CameraMovement.Instance.mFollowing = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
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
                mScreenSaver.gameObject.SetActive(true);
                ShowCollection();
                mMenuWindow.GetComponent<Rigidbody2D>().DOMove(End.position, 0.3f);
                isShowMenu = true;
            }
        }
    }
}
