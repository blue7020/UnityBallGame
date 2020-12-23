using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdmobHandler : MonoBehaviour
{
    public static GoogleAdmobHandler Instance;

    ////배너 광고
    //private BannerView bannerView;
    ////전면 광고
    //private InterstitialAd interstitial;

    //보상형 광고
    private RewardBasedVideoAd rewardBasedVideo;
    private readonly string mAppID = "ca-app-pub-4617216056571941~5027589140";
    private readonly string mRewardAdID = "ca-app-pub-4617216056571941/5367334643";
    private static bool ShowAd;

    public eAdsReward eType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Initialize the Google Mobile Ads SDK.
            //MobileAds.Initialize(InitializationStatus => { });//첫번째 파라미터
#pragma warning disable CS0618 // Type or member is obsolete
            MobileAds.Initialize(mAppID);
#pragma warning restore CS0618 // Type or member is obsolete
            ShowAd = false;
                              //ca-app-pub-4617216056571941~5027589140 = 앱 id
                              //ca-app-pub-4617216056571941/5367334643 = 보상형 광고
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        Handle(rewardBasedVideo);
        Load();
    }

    private void Handle(RewardBasedVideoAd reward)
    {
        // Called when an ad request has successfully loaded.
        reward.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        reward.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        reward.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        reward.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        reward.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        reward.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        reward.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        ShowAd = true;
    }

    private void Load()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice("4395a48f-b8a5-437e-830d-ba6c75889f92").Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, mRewardAdID);//광고 준비
    }

    //adUnitId 는 Google Admob의 광고 코드를 복사해서 수정해야한다!

//    private void RequestBanner()//광고를 로드해서 뿌려주는 기능
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-4617216056571941~5027589140";
//#elif UNITY_IPHONE
//            string adUnitId = "ca-app-pub-4617216056571941/4531946884";
//#else
//            string adUnitId = "unexpected_platform";
//#endif
//        // Create a 320x50 banner at the top of the screen.
//        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);//상단 배너

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();

//        // Load the banner with the request.
//        bannerView.LoadAd(request);//광고 준비
//    }
    //bannerView 지정이 끝나면 참조를 삭제하기 전 bannerView.Destroy();로 정리
    //bannerView.Show();//광고 출력

//    private void RequestInterstitial()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-4617216056571941~5027589140";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-4617216056571941/4531946884";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // Initialize an InterstitialAd.
//        interstitial = new InterstitialAd(adUnitId);

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the interstitial with the request.
//        interstitial.LoadAd(request);//광고 준비
//    }
    //interstitial 지정이 끝나면 참조를 삭제하기 전 interstitial.Destroy();로 정리

    //private void RequestRewardBasedVideo()
    //{
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-4617216056571941/5367334643";
//#elif UNITY_IPHONE
//                    string adUnitId = "ca-app-pub-4617216056571941/4531946884";
//#else
//                    string adUnitId = "unexpected_platform";
//#endif
//        rewardBasedVideo = RewardBasedVideoAd.Instance;

        //// Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        //// Load the rewarded video ad with the request.
        //rewardBasedVideo.LoadAd(request, adUnitId);//광고 준비
    //}
    //rewardBasedVideo.Show();//광고 출력

    private void DeleteRequestRewardBasedVideo(RewardBasedVideoAd reward)
    {
        if (ShowAd==true)
        {
            // Called when an ad request has successfully loaded.
            reward.OnAdLoaded -= HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            reward.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            reward.OnAdOpening -= HandleRewardBasedVideoOpened;
            // Called when the ad starts to play.
            reward.OnAdStarted -= HandleRewardBasedVideoStarted;
            // Called when the user should be rewarded for watching a video.
            reward.OnAdRewarded -= HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            reward.OnAdClosed -= HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
            reward.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
            Handle(reward);
        }
    }

    public void PlayAD()
    {
        if (SaveDataController.Instance.mUser.NoAds == false)
        {
            StartCoroutine(ShowRewardAd());
            //RequestRewardBasedVideo();//보상형
            //if (rewardBasedVideo.IsLoaded())
            //{
            //    rewardBasedVideo.Show();
            //    Debug.Log(rewardBasedVideo.IsLoaded());
            //}
        }
        else
        {
            Debug.Log("Reward!");//보상 지급
            call();
            call = null;
            SaveDataController.Instance.Save();
        }
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
    {
        Debug.Log("Left");//광고를 클릭하여 어플리케이션을 나갔을 때
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        Debug.Log("Video Close");//광고 화면 꺼짐
    }

    delegate void callback();
    callback call;

    public void SetAdRewardCallBack(eAdsReward reward)
    {
        switch (reward)
        {
            case eAdsReward.None:
                call = () => GameSetting.Instance.NoneReward();
                break;
            case eAdsReward.DailySyrup:
                call = () => GameSetting.Instance.DailySyrup();
                break;
            case eAdsReward.DoubleReward:
                call = () => GameSetting.Instance.Double();
                break;
            case eAdsReward.Revive:
                call = () => GameSetting.Instance.NoneReward();
                break;
            case eAdsReward.Syrup:
                call = () => GameSetting.Instance.Syrup();
                break;
            default:
                call = () => GameSetting.Instance.NoneReward();
                break;
        }
    }

    private IEnumerator ShowRewardAd()
    {
        while (!rewardBasedVideo.IsLoaded())
        {
            yield return null;
        }
        rewardBasedVideo.Show();
        Debug.Log(rewardBasedVideo.IsLoaded());
    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        string type = e.Type;//리워드 상품
        double amount = e.Amount;//리워드 수량
        Debug.Log(type+" / "+amount);//보상 지급
        DeleteRequestRewardBasedVideo(rewardBasedVideo);
        call();
        call = null;
        SaveDataController.Instance.Save();
    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
    {
        Debug.Log("Video start");
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
        Debug.Log("Video open");//광고 화면 켜짐(게임 화면 가려짐)
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("Load fail");//광고 준비 안됨
        string text = "";
        if (GameSetting.Instance.Ingame)
        {
            if (GameSetting.Instance.Language == 0)
            {
                text = "광고가 준비되지 않았습니다!";
            }
            else
            {
                text = "The ad is not ready!";
            }
            UIController.Instance.mPopupWindow.ShowWindow(text);
        }
        else
        {
            if (GameSetting.Instance.Language==0)
            {
                text = "광고가 준비되지 않았습니다!";
            }
            else
            {
                text = "The ad is not ready!";
            }
            MainLobbyUIController.Instance.mPopupWindow.ShowWindow(text);
        }
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        Debug.Log("Load success");//광고 준비 완료
    }
}
