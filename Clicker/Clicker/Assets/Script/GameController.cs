using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int mCurrentStage;

    [SerializeField]
    private double mProgressWeight = 1.08d;//소숫점의 기본은 Double이다. 안해도 상관은 없지만 구분을 위해 소숫점 뒤에 d를 붙인다.
    private double mCurrentProgress;
    private double mMaxProgress;

    private double mTouchPower;

    [SerializeField]//임시(temp)
    private GemPool mGemPool;

    [SerializeField]
    private Gem mCurrentGem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //TODO Load Save Data
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mCurrentStage = 0;
        mMaxProgress = 10;
        mTouchPower = 1;
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
        //Math는 C#에서의 Random이므로 UnityEngine의 Random을 사용한다.
    }

    private void ClacNectStage()
    {
        mCurrentStage++;
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);//Mathf는 float이기 때문에 return 값이 double인 Using System - Math를 사용한다.
        mCurrentGem.gameObject.SetActive(false);
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
        mCurrentProgress = 0;

    }

    public void Touch()
    {
        if (mCurrentProgress >= mMaxProgress)
        {
            ClacNectStage();
        }
        else
        {
            mCurrentProgress += mTouchPower;
            float progress = (float)(mCurrentProgress / mMaxProgress);//이미지의 fillamount가 있기 때문에 float을 사용한다
            mCurrentGem.SetProgress(progress);
        }
        UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mCurrentStage++;
            mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);
            UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
        }
        
    }
}
