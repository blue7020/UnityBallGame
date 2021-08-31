using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int mScore, mHighScore,mHeight,mStage,mReviveToken;
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

    public void Revive()
    {
        if (mReviveToken>0)
        {
            mReviveToken--;
            UIController.Instance.mGameOverWindow.gameObject.SetActive(false);
            Vector3 pos = PlayerCtrl.Instance.spPoint.transform.position+ new Vector3(0,-3.5f,0);
            PlayerCtrl.Instance.transform.position = pos;
            PlayerCtrl.Instance.isDead = false;
            Time.timeScale = 1;
            StartCoroutine(PlayerCtrl.Instance.ShowReviveTile());
        }
        else
        {
            UIController.Instance.mAdButton.interactable = false;
        }
    }
}
