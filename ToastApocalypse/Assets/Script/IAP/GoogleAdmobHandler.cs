using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdmobHandler : MonoBehaviour
{
    public static GoogleAdmobHandler Instance;

    //배너 광고
    public BannerView bannerView;
    //전면 광고
    private InterstitialAd interstitial;
    //보상형 광고
    private RewardBasedVideoAd rewardBasedVideo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(InitializationStatus => { });//첫번째 파라미터
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //여기서 광고 제거 구매 여부를 체크
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        //RequestBanner();//배너
        //RequestInterstitial();//전면
    }


    //adUnitId 는 Google Admob의 광고 코드를 복사해서 수정해야한다!

    private void RequestBanner()//광고를 로드해서 뿌려주는 기능
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4617216056571941~5027589140";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4617216056571941/4531946884";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);//상단 배너

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);//광고 준비
    }
    //bannerView 지정이 끝나면 참조를 삭제하기 전 bannerView.Destroy();로 정리
    //bannerView.Show();//광고 출력

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4617216056571941~5027589140";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4617216056571941/4531946884";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);//광고 준비
    }
    //interstitial 지정이 끝나면 참조를 삭제하기 전 interstitial.Destroy();로 정리

    private void GameOver()//게임 오버 시 전면 광고를 출력
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();//광고 출력
        }
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4617216056571941~5027589140";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4617216056571941/4531946884";
#else
            string adUnitId = "unexpected_platform";
#endif
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitId);//광고 준비
    }
    //rewardBasedVideo.Show();//광고 출력

    public void PlayAD()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        RequestRewardBasedVideo();//보상형
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
    {
        Debug.Log("Left");//광고를 클릭하여 어플리케이션을 나갔을 때
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        Debug.Log("Video Close");//광고 화면 꺼짐
        Time.timeScale = 1;
    }

    delegate void callback();
    callback call;
    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        Debug.Log("Reward!");//보상 지급
        call?.Invoke(); //call(); 와 같다. 하지만 null 체크를 위해 해당 구문을 사용한다
        Time.timeScale = 1;
    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
    {
        Debug.Log("Video start");
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
        Debug.Log("Video open");//광고 화면 켜짐(게임 화면 가려짐)
        Time.timeScale = 0;
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("Load fail");//광고 준비 안됨
        //광고 버튼 비활성화
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        Debug.Log("Load success");//광고 준비 완료
    }
}
