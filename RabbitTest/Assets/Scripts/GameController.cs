using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int mScore, mHighScore,mHeight,mStage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Time.timeScale = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mHighScore = SaveDataController.Instance.mUser.HighScore;
        if (SaveDataController.Instance.DeadAds)
        {
            //AdsManager.Instance.ShowAd();
            SaveDataController.Instance.DeadAds = false;
        }
    }

    private void Update()
    {
        if (mStage<6)
        {
            if (mStage ==0&& mHeight>=250&&mHeight<500)
            {
                UIController.Instance.ChangeBG();
                mStage++;
            }
            else if (mStage == 1 && mHeight >= 500 && mHeight < 750)
            {
                UIController.Instance.ChangeBG();
                mStage++;
            }
            else if (mStage == 2 && mHeight >= 1000 && mHeight < 1500)
            {
                UIController.Instance.ChangeBG();
                mStage++;
            }
            else if (mStage == 3 && mHeight >= 2000)
            {
                UIController.Instance.ChangeBG();
                mStage++;
            }
        }
    }

    public void AddScore(int amount)
    {
        if ((mScore + amount) > mHighScore)
        {
            mScore += amount;
            if (mScore<1)
            {
                mScore = 0;
            }
            mHighScore = mScore;
            UIController.Instance.HighScoreMessage();
        }
        else
        {
            mScore += amount;
            if (mScore < 1)
            {
                mScore = 0;
            }
        }
    }
}
