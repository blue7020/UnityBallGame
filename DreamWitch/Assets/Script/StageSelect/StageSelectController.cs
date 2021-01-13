using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectController : InformationLoader
{
    public static StageSelectController Instance;

    public Image mSelectUIImage,mPlayerIcon;
    public Text mStageTitleText, mStageInfoText, mStageButtonText, mCloseText;
    public Camera mCamera;

    public StageInfo[] mInfoArr;
    public IslandSelect[] IslandSelectArr;

    public bool isSelectDelay, isShowNewStage;

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
            mStageButtonText.text = "이동하기";
            mStageTitleText.text = mInfoArr[TitleController.Instance.NowStage].title_kor;
            mStageInfoText.text = mInfoArr[TitleController.Instance.NowStage].info_kor;
            mCloseText.text = "닫기: X";
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mStageButtonText.text = "Enter";
            mStageTitleText.text = mInfoArr[TitleController.Instance.NowStage].title_eng;
            mStageInfoText.text = mInfoArr[TitleController.Instance.NowStage].info_eng;
            mCloseText.text = "Close: X";
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
        mCamera.transform.position = IslandSelectArr[TitleController.Instance.NowStage].pos + new Vector3(0, 0, -10); ;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            mSelectUIImage.gameObject.SetActive(false);
        }
    }
}
