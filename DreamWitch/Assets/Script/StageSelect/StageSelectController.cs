using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectController : InformationLoader
{
    public static StageSelectController Instance;

    public Image mSelectUIImage;
    public Text mStageTitleText, mStageInfoText,mStageButtonText,mCloseText;
    public Camera mCamera;

    public StageInfo[] mInfoArr;
    public IslandSelect[] IslandSelectArr;

    public bool isSelectDelay;
    public int NowStage;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr,Path.STAGE_INFO);
            //이후 세이브 시스템 넣으면 마지막으로 플레이한 스테이지의 시작 위치 불러옴
            ShowStageSelectUI(NowStage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowStageSelectUI(int id)
    {
        NowStage = id;
        if (TitleController.Instance.mLanguage==0)
        {
            mStageButtonText.text = "이동하기";
            mStageTitleText.text = mInfoArr[NowStage].title_kor;
            mStageInfoText.text = mInfoArr[NowStage].info_kor;
            mCloseText.text = "닫기: X";
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mStageButtonText.text = "Enter";
            mStageTitleText.text = mInfoArr[NowStage].title_eng;
            mStageInfoText.text = mInfoArr[NowStage].info_eng;
            mCloseText.text = "Close: X";
        }
        mSelectUIImage.gameObject.SetActive(true);
        mCamera.transform.position = IslandSelectArr[NowStage].pos;
    }

    public void EnterStage()
    {
        switch (NowStage)
        {
            case 0:
                SceneManager.LoadScene(1);
                SoundController.Instance.BGMChange(0);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            mSelectUIImage.gameObject.SetActive(false);
        }
    }
}
