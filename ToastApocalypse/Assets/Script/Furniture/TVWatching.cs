using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TVWatching : MonoBehaviour
{
    public Image mWindow;
    public Text mTooltipText, mButtonText;
    public Button mAdButton;
    public PopUpWindow mPopUpWindow;

    private void Awake()
    {
        if (GameSetting.Instance.Language==0)
        {
            mTooltipText.text = "광고를 보시겠습니까?\n(매일 한번 광고 시청 시 시럽 지급)";
            mButtonText.text = "본다";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTooltipText.text = "Would you like to see the AD?\n(Syrup is given once a day when watching an AD.)";
            mButtonText.text = "Yes";
        }
    }

    public void ShowAds()
    {
        DateTime time = SaveDataController.Instance.mUser.LastWatchingDailyAdsTime;
        DateTime timecheck = DateTime.Now;
        Debug.Log(time + " / " + timecheck);
        if (SaveDataController.Instance.mUser.LastWatchingDailyAdsTime.AddDays(1) <= timecheck)
        {
            SaveDataController.Instance.mUser.TodayWatchFirstAD = false;
        }
        if (SaveDataController.Instance.mUser.TodayWatchFirstAD == false)
        {

            if (SaveDataController.Instance.mUser.NoAds)
            {
                RewardAdsManager.Instance.DailySyrup(500);
            }
            else
            {
                RewardAdsManager.Instance.ShowRewardAd(1);
            }
            if (GameSetting.Instance.Language == 0)
            {
                mPopUpWindow.mText.text = "일일보상을 획득했습니다!\n+500 시럽";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mPopUpWindow.mText.text = "You got daily reward!\n+500 Syrup";
            }
        }
        else
        {
            if (GameSetting.Instance.Language == 0)
            {
                mPopUpWindow.mText.text = "이미 일일 보상을 획득했습니다";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mPopUpWindow.mText.text = "You've already earned your reward";
            }
        }
        mPopUpWindow.gameObject.SetActive(true);
    }

    public void TimeReset()
    {
        Time.timeScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
