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
        GameSetting.Instance.ShowAds();
        DateTime timecheck = SaveDataController.Instance.mUser.LastServerTime.AddDays(1);
        if (SaveDataController.Instance.mUser.LastServerTime >= timecheck)
        {
            SaveDataController.Instance.mUser.TodayWatchFirstAD = false;
        }
        if (SaveDataController.Instance.mUser.TodayWatchFirstAD == false)
        {
            SaveDataController.Instance.mUser.LastServerTime = DateTime.Now;
            SaveDataController.Instance.mUser.TodayWatchFirstAD = true;
            if (GameSetting.Instance.Language==0)
            {
                mPopUpWindow.mText.text = "일일보상을 획득했습니다!\n+500 시럽";
            }
            else if (GameSetting.Instance.Language==1)
            {
                mPopUpWindow.mText.text = "You got daily reward!\n+500 Syrup";
            }
            mPopUpWindow.gameObject.SetActive(true);
            SaveDataController.Instance.mUser.Syrup += 500;
            MainLobbyUIController.Instance.ShowSyrupText();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
