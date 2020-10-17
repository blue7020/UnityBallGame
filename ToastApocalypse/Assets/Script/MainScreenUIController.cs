using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenUIController : MonoBehaviour
{
    public static MainScreenUIController Instance;

    public Text mStartText, mEndText, mBGMText, mSEText,CautionText;
    public Button mEngButton, mKorButton;

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
                mEngButton.interactable = true;
                mKorButton.interactable = false;

            }
            else if (GameSetting.Instance.Language == 1)
            {
                mStartText.text = "Start";
                mEndText.text = "Quit";
                CautionText.text = "<color=#FF2222>[Caution]</color> Game data saving\non this device.\nIf you delete the game,\nyou will not be able\nto recover data!";
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
        mBGMText.text = SoundController.Instance.UIBGMVol.ToString();
        mSEText.text = SoundController.Instance.UISEVol.ToString();
    }

    public void ButtonPush()
    {
        SoundController.Instance.SESoundUI(0);
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
    }
    public void LanguagetoEng()
    {
        mEngButton.interactable = true;
        mKorButton.interactable = false;
        GameSetting.Instance.Language = 0;
        mStartText.text = "게임 시작";
        mEndText.text = "게임 종료";
        CautionText.text = "<color=#FF2222>[주의]</color> 게임 데이터는 장치에\n저장되며, 게임을 삭제하면\n데이터를 복구할 수 없습니다!";
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
