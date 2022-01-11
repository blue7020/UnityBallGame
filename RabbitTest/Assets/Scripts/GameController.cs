using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int mScore, mHighScore,mHeight,mStage,mReviveToken,mGoldText;
    public GameObject mFirstTile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mReviveToken = 1;
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
        float x = Random.Range(-5f, 5f) * 0.5f;
        mFirstTile.transform.position = new Vector3(x, mFirstTile.transform.position.y,0);
    }

    private void Update()
    {
        if (mStage<6)
        {
            if (mStage ==0&& mHeight>=250&&mHeight<500)
            {
                mStage++;
                UIController.Instance.ChangeBG();
            }
            else if (mStage == 1 && mHeight >= 500 && mHeight < 750)
            {
                mStage++;
                UIController.Instance.ChangeBG();
            }
            else if (mStage == 2 && mHeight >= 1000 && mHeight < 1500)
            {
                mStage++;
                UIController.Instance.ChangeBG();
            }
            else if (mStage == 3 && mHeight >= 2000)
            {
                mStage++;
                UIController.Instance.ChangeBG();
            }
        }
    }

    public void AddScore(int amount)
    {
        if ((mScore + amount)<= 2147483646)
        {
            if ((mScore + amount) > mHighScore)
            {
                mScore += amount;
                if (mScore < 1)
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
        else
        {
            mScore = 2147483646;
            mHighScore = mScore;
            UIController.Instance.HighScoreMessage();
        }
    }

    public void AddGold(int amount)
    {
        if (SaveDataController.Instance.mUser.Gold+amount<999999999)
        {
            SaveDataController.Instance.mUser.Gold = SaveDataController.Instance.mUser.Gold + amount;
        }
        else
        {
            SaveDataController.Instance.mUser.Gold = 999999999;
        }
        mGoldText = SaveDataController.Instance.mUser.Gold;
    }

    public void Revive()
    {
        if (mReviveToken>0)
        {
            mReviveToken--;
            SoundController.Instance.SESound(7);
            UIController.Instance.mGameOverWindow.gameObject.SetActive(false);
            Vector3 pos = PlayerController.Instance.mPlayer.spPoint.transform.position+ new Vector3(0,-3.5f,0);
            PlayerController.Instance.mPlayer.transform.position = pos;
            PlayerController.Instance.mPlayer.isDead = false;
            Time.timeScale = 1;
            StartCoroutine(PlayerController.Instance.mPlayer.ShowReviveTile());
        }
        else
        {
            UIController.Instance.mAdButton.interactable = false;
        }
    }

    public void ScrollRevive()
    {
        if (SaveDataController.Instance.mUser.RevivalCount > 0)
        {
            SaveDataController.Instance.mUser.RevivalCount -= 1;
            UIController.Instance.LanguageRefresh();
            SoundController.Instance.SESound(7);
            UIController.Instance.mGameOverWindow.gameObject.SetActive(false);
            Vector3 pos = PlayerController.Instance.mPlayer.spPoint.transform.position + new Vector3(0, -3.5f, 0);
            PlayerController.Instance.mPlayer.transform.position = pos;
            PlayerController.Instance.mPlayer.isDead = false;
            Time.timeScale = 1;
            StartCoroutine(PlayerController.Instance.mPlayer.ShowReviveTile());
        }
        else
        {
            UIController.Instance.mScrollButton.interactable = false;
            UIController.Instance.LanguageRefresh();
        }
    }
}
