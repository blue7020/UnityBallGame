using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : InformationLoader
{
    public static MainController Instance;
    public Text mStartText,mRankingText,mBestRankingText,mGuideText,mGuideToolTipText,mPageText, mVersionText,mMenuText,mGoldText,mRatioText;
    public Image mTitleImage,mRankingImage,mTitleBGImage,mGuideWindowImage,mGuideIconImage, mLanguageButton,mCheckBox;
    public Sprite[] mTitleSprite,mTitleBGSprite,mGuideIconSprite, mLanguageButtonSprite,mCheckBoxSprite;
    public Button mStartButton;
    public int TitleClickCount,mGuidePageCount;
    public bool suprise;

    private GuideText[] mInfoArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Paths.GUIDE_TEXT);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveDataController.Instance.LoadGame();
        RankingController.Instance.UserID = SaveDataController.Instance.mUser.ID;
        mVersionText.text = "Version: " + Application.version;
        SaveDataController.Instance.mLanguage = SaveDataController.Instance.mUser.Language;
        SaveDataController.Instance.mMute = SaveDataController.Instance.mUser.Mute;
        LanguageRefresh();
        mTitleBGImage.sprite = mTitleBGSprite[0];
        TitleClickCount = 0;
        suprise = false;
        if (SaveDataController.Instance.DeadAds)
        {
            if (SaveDataController.Instance.AdsCount==10)
            {
                if (!SaveDataController.Instance.mUser.NoAds)
                {
                    UnityAdsHelper.Instance.Show();
                }
                SaveDataController.Instance.AdsCount = 0;
            }
            else
            {
                SaveDataController.Instance.AdsCount++;
            }
            SaveDataController.Instance.DeadAds = false;
        }
        SoundController.Instance.BGMChange(0);
        StartCoroutine(DelayStartButton());
        if (SaveDataController.Instance.mUser.CharacterOpen[0]==false)
        {
            SaveDataController.Instance.mUser.CharacterOpen[0] = true;
            SaveDataController.Instance.Save();
        }
    }

    public void UpdateRanking()
    {
        RankingController.Instance.UpdateRecode();
        mRankingImage.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        SaveDataController.Instance.Save();
        SceneManager.LoadScene(1);
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
            ExitGame();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }

    public void ExitGame()
    {
        SaveDataController.Instance.Save();
        Application.Quit();
    }

    public void ButtonSound()
    {
        SoundController.Instance.SESound(3);
    }

    public void TitleClick()
    {
        if (TitleClickCount < 10)
        {
            TitleClickCount++;
        }
        else
        {
            ShowSomething();
        }
    }

    public void ShowSomething()
    {
        if (!suprise)
        {
            mTitleBGImage.sprite = mTitleBGSprite[1];
            SoundController.Instance.BGMChange(1);
            suprise = true;
        }
        else
        {
            mTitleBGImage.sprite = mTitleBGSprite[0];
            SoundController.Instance.BGMChange(0);
            suprise = false;
        }
        TitleClickCount = 0;
    }

    public void ResetPageCount()
    {
        mGuidePageCount = 0;
    }

    public void PlusPage()
    {
        if (mGuidePageCount+1<= mGuideIconSprite.Length-1)
        {
            mGuidePageCount++;
            RefreshGuidePage();
        }
    }

    public void MinusPage()
    {
        if (mGuidePageCount -1 >= 0)
        {
            mGuidePageCount--;
            RefreshGuidePage();
        }
    }

    public void RefreshGuidePage()
    {
        mPageText.text = (mGuidePageCount + 1)+" / "+ mGuideIconSprite.Length;
        mGuideIconImage.sprite = mGuideIconSprite[mGuidePageCount];
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mGuideToolTipText.text = mInfoArr[mGuidePageCount].text_kor;
            mGuideText.text = "가이드: "+ mInfoArr[mGuidePageCount].title_kor;
        }
        else
        {
            mGuideToolTipText.text = mInfoArr[mGuidePageCount].text_eng;
            mGuideText.text = "Guide: " + mInfoArr[mGuidePageCount].title_eng;
        }
    }

    public void RefreshGold()
    {
        mGoldText.text = SaveDataController.Instance.mUser.Gold+"G";
    }

    public IEnumerator DelayStartButton()
    {
        WaitForSeconds time = new WaitForSeconds(1f);
        yield return time;
        mStartButton.gameObject.SetActive(true);
    }

    public void LanguageButtonRefresh()
    {
        if (SaveDataController.Instance.mLanguage == 1)//if now Korean
        {
            mLanguageButton.sprite = mLanguageButtonSprite[0];
        }
        else
        {
            mLanguageButton.sprite = mLanguageButtonSprite[1];

        }
    }

    public void LanguageChange()
    {
        if (SaveDataController.Instance.mLanguage == 1)//if now Korean
        {
            SaveDataController.Instance.mLanguage = 0;
        }
        else
        {
            SaveDataController.Instance.mLanguage = 1;
        }
        LanguageButtonRefresh();
        LanguageRefresh();
    }

    public void LanguageRefresh()
    {
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mStartText.text = "터치하여 시작";
            mMenuText.text = "메뉴";
            mRatioText.text = "폴더블 스마트폰이라면\n왼쪽의 설정을 체크한 후,\n화면을 반으로 접어주세요.\n(9:21 해상도 대응)";
            mTitleImage.sprite = mTitleSprite[1];
        }
        else
        {
            mStartText.text = "Touch to Start";
            mMenuText.text = "Menu";
            mRatioText.text = "If you are a foldable\nsmartphone, check the\ncheck box on the left\nand fold the screen in half.\n(9:21 resolution response)";
            mTitleImage.sprite = mTitleSprite[0];
        }
        if (SaveDataController.Instance.mFolderbleSetting)
        {
            mCheckBox.sprite = mCheckBoxSprite[1];
        }
        else
        {
            mCheckBox.sprite = mCheckBoxSprite[0];
        }
    }

    public void FolderbleSetting()
    {
        if (!SaveDataController.Instance.mFolderbleSetting)
        {
            SaveDataController.Instance.mFolderbleSetting = true;
            mCheckBox.sprite = mCheckBoxSprite[1];
        }
        else
        {
            SaveDataController.Instance.mFolderbleSetting = false;
            mCheckBox.sprite = mCheckBoxSprite[0];
        }
        if (SaveDataController.Instance.mFolderbleSetting)
        {
            mCheckBox.sprite = mCheckBoxSprite[1];
        }
        else
        {
            mCheckBox.sprite = mCheckBoxSprite[0];
        }
    }
}
