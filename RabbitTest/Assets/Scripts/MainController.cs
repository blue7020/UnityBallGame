﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : InformationLoader
{
    public static MainController Instance;
    public Text mStartText,mRankingText,mBestRankingText,mGuideText,mGuideToolTipText,mPageText, mVersionText,mMenuText,mGoldText;
    public Image mTitleImage,mRankingImage,mTitleBGImage,mGuideWindowImage,mGuideIconImage, mLanguageButton;
    public Sprite[] mTitleSprite,mTitleBGSprite,mGuideIconSprite, mLanguageButtonSprite;
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
        mVersionText.text = "Version: " + SaveDataController.Instance.mVersion;
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
    }

    public void UpdateRanking()
    {
        RankingController.Instance.UpdateRecode();
        mRankingImage.gameObject.SetActive(true);
    }

    public void GameStart()
    {
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
        if (mGuidePageCount+1<=3)
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
        mPageText.text = (mGuidePageCount + 1)+" / 4";
        mGuideIconImage.sprite = mGuideIconSprite[mGuidePageCount];
        if (SaveDataController.Instance.mLanguage == 1)//korean
        {
            mGuideToolTipText.text = mInfoArr[mGuidePageCount].text_kor;
        }
        else
        {
            mGuideToolTipText.text = mInfoArr[mGuidePageCount].text_eng;
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
            mGuideText.text = "가이드";
            mMenuText.text = "메뉴";
            mTitleImage.sprite = mTitleSprite[1];
        }
        else
        {
            mStartText.text = "Touch to Start";
            mGuideText.text = "Guide";
            mMenuText.text = "Menu";
            mTitleImage.sprite = mTitleSprite[0];
        }
    }
}