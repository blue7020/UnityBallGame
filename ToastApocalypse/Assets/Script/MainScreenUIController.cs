using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenUIController : InformationLoader
{
    public static MainScreenUIController Instance;

    public Text mStartText, mEndText, mBGMText, mSEText,CautionText,mCodeText,mNoticeTitle, mNoticeText;
    public Button mEngButton, mKorButton;
    public Image mCutSceneBG,mNoteScreen;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (GameSetting.Instance.Language == 0)
            {
                mStartText.text = "게임 시작";
                mEndText.text = "게임 종료";
                CautionText.text = "<color=#FF2222>[주의]</color> 게임 데이터는 장치에\n저장되며, 게임을 삭제하면\n데이터를 복구할 수 없습니다!";
                mCodeText.text = "코드 사용";
                mNoticeTitle.text = "공지";
                mNoticeText.text = "-플레이 해 주셔서 감사합니다!\n\n-버그 제보 환영합니다.\n\n불쌍한 개발자를 위해 이 게임을 많이 홍보해주세요.\n\n-광고 기능들이 제대로 작동하지 않을 수 있습니다.";
                mEngButton.interactable = true;
                mKorButton.interactable = false;

            }
            else if (GameSetting.Instance.Language == 1)
            {
                mStartText.text = "Start";
                mEndText.text = "Quit";
                CautionText.text = "<color=#FF2222>[Caution]</color> Game data saving\non this device.\nIf you delete the game,\nyou will not be able\nto recover data!";
                mCodeText.text = "Using Code";
                mNoticeTitle.text = "Notice";
                mNoticeText.text = "-Thanks for playing!\n\n-Please report the error.\n\nPlease promote this game a lot for poor developer.\n\n-Ad functions may not work properly.";
                mEngButton.interactable = false;
                mKorButton.interactable = true;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (SaveDataController.Instance.mUser.TutorialEnd==true)
        {
            OpeningSkip();
        }
        else
        {
            SoundController.Instance.BGMChange(0);
        }
        NoticeTimeCheck();
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }

    public void NoticeTimeCheck()
    {
        DateTime timecheck = SaveDataController.Instance.mUser.DailyTime.AddDays(1);
        if (SaveDataController.Instance.mUser.DailyTime >= timecheck)
        {
            SaveDataController.Instance.mUser.TodayWatchFirstNotice = false;
        }
        if (SaveDataController.Instance.mUser.TodayWatchFirstNotice == false)
        {
            SaveDataController.Instance.mUser.TodayWatchFirstNotice = true;
            mNoteScreen.gameObject.SetActive(true);
        }
        else
        {
            mNoteScreen.gameObject.SetActive(false);
        }
    }

    public void ButtonPush()
    {
        SoundController.Instance.SESoundUI(0);
    }

    public void OpeningSkip()
    {
        mCutSceneBG.gameObject.SetActive(false);
        SoundController.Instance.BGMChange(1);
    }

    //0 = 한국어 / 1 = 영어
    public void LanguagetoKor()
    {
        mEngButton.interactable = false;
        mKorButton.interactable = true;
        GameSetting.Instance.Language = 1;
        mStartText.text = "Start";
        mEndText.text = "Quit";
        CautionText.text = "<color=#FF2222>[Caution]</color> Game data saving\non this device.\nIf you delete the game,\nyou will not be able\nto recover data!";
        mCodeText.text = "Using Code";
    }
    public void LanguagetoEng()
    {
        mEngButton.interactable = true;
        mKorButton.interactable = false;
        GameSetting.Instance.Language = 0;
        mStartText.text = "게임 시작";
        mEndText.text = "게임 종료";
        CautionText.text = "<color=#FF2222>[주의]</color> 게임 데이터는 장치에\n저장되며, 게임을 삭제하면\n데이터를 복구할 수 없습니다!";
        mCodeText.text = "코드 사용";
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

    public void FirstGameStart()
    {
        if (SaveDataController.Instance.mUser.TutorialEnd==false)
        {
            GoTutorial();
        }
        else
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(0);
    }
    
    public void GoTutorial()
    {
        SceneManager.LoadScene(4);
        SoundController.Instance.mBGM.Stop();
        SoundController.Instance.BGMChange(0);
    }

    public void GameQuit()
    {
        Application.Quit();
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
            GameQuit();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
