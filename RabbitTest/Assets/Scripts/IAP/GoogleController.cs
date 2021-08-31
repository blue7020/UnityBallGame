using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GoogleController : MonoBehaviour
{

    public static GoogleController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // enables saving game progress.
           .EnableSavedGames()
           // requests the email address of the player be available.
           // Will bring up a prompt for consent.
           .RequestEmail()
           // requests a server auth code be generated so it can be passed to an
           //  associated back end server application and exchanged for an OAuth token.
           .RequestServerAuthCode(false)
           // requests an ID token be generated.  This OAuth token can be used to
           //  identify the player to other services such as Firebase.
           .RequestIdToken()
           .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();

            // authenticate user:
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                // handle results
                Debug.Log("Google Login: " + result);
            });
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CallSample()
    {
        //UnlockAchvement(GPGSIds.leaderboard_fresh_bread, 100);
    }

    public void UnlockAchvement(string id, long value)
    {
        // unlock achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(id, value, (bool success) => {
            // handle success or failure
        });
    }

    //화면 전체 가리는 그거
    public void ShowAchivement()
    {
        //gamePause창 띄우기
        Social.ShowAchievementsUI();
    }
    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    //

    public void ReportLeaderboard(long CurrentScore, string id)
    {
        Social.ReportScore(CurrentScore, id, (bool success) => {
            // handle success or failure
        });
    }
}
