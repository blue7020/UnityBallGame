using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class RewardAdsManager : MonoBehaviour
{
    public static RewardAdsManager Instance;

    private const string android_game_id = "4280981";
    private const string ios_game_id = "4280980";

    private const string RewardVideoID = "Rewarded_Android";
    private int AdType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Advertisement.Initialize(android_game_id);
        AdType = 0;
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Rewards();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                //스킵할때
                break;
            case ShowResult.Failed:
                Debug.Log("The ad failed to be shown.");
                //실패시
                break;
        }
    }

    public void ShowRewardAd(int type)
    {
        AdType = type;
        if (!SaveDataController.Instance.mUser.NoAds)
        {
            if (Advertisement.IsReady(RewardVideoID))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show(RewardVideoID, options);
            }
        }
        else
        {
            Rewards();
        }
    }

    public void Rewards()
    {
        //기능수행
        switch (AdType)
        {
            case 1://일일시럽
                GameSetting.Instance.TimeCheck24Ad();
                DateTime time = SaveDataController.Instance.mUser.DailyTime;
                DateTime timecheck = DateTime.Now;
                Debug.Log(time + " / " + timecheck);
                if (SaveDataController.Instance.mUser.LastWatchingDailyAdsTime.AddDays(1) <= timecheck)
                {
                    SaveDataController.Instance.mUser.TodayWatchFirstAD = false;
                }
                if (SaveDataController.Instance.mUser.TodayWatchFirstAD == false)
                {
                    SaveDataController.Instance.mUser.LastWatchingDailyAdsTime = DateTime.Now;
                    SaveDataController.Instance.mUser.TodayWatchFirstAD = true;
                    GameSetting.Instance.GetSyrup(500);
                    MainLobbyUIController.Instance.ShowSyrupText();
                }
                break;
            case 2://부활
                //부활 후 게임 오버 시에 광고가 나오기 때문에 리워드는 없다.
                break;
            case 3://보상2배
                GameSetting.Instance.Double();
                break;
            default:
                break;
        }
    }
}
