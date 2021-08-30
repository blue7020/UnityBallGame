using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAdsManager : MonoBehaviour
{
    public static RewardAdsManager Instance;

    private const string android_game_id = "4280981";
    private const string ios_game_id = "4280980";

    private const string RewardVideoID = "Rewarded_Android";

    private void Awake()
    {
        if (Instance==null)
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
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                GameController.Instance.Revive();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                GameController.Instance.mReviveToken--;
                UIController.Instance.mAdButton.interactable = false;
                break;
            case ShowResult.Failed:
                Debug.Log("The ad failed to be shown.");
                GameController.Instance.mReviveToken--;
                UIController.Instance.mAdButton.interactable = false;
                break;
        }
    }

    public void ShowRewardAd()
    {
        if (Advertisement.IsReady(RewardVideoID))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(RewardVideoID,options);
        }
    }
}
