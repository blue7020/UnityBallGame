using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private double mGold;
    public Delegates.VoidCallback GoldCallback;//골드를 성공적으로 썼을때만 발동
    public double Gold
    {
        get { return mGold; } //get을 하지 않아주면 +=같은 수식이 먹히지 않는다.
        set
        {
            if (value >=0)
            {
                mGold = value;
                GoldCallback?.Invoke(); //Unity의 Invoke와 Delegate의 Invoke와는 다르다!
                //위와 아래는 같은 코드
                //if (GoldCallback!=null)
                //{
                //    GoldCallback();
                //}

            }
            else
            {
                Debug.Log("골드가 부족합니다.");
            }
            GoldCallback = null;
        }
    }

    [SerializeField]
    private double mIncomeWeight = 1.04d; //골드 배수
    private double mIncome;

    private int mCurrentStage;

    [SerializeField]
    private double mProgressWeight = 1.08d;//프로그래스 최댓값 배수
    //소숫점의 기본은 Double이다. 안해도 상관은 없지만 구분을 위해 소숫점 뒤에 d를 붙인다.
    private double mCurrentProgress;
    private double mMaxProgress;

    private double mTouchPower;
    public double TouchPower
    {
        get { return mTouchPower; }
        set
        {
            if(value >= 0)
            {
                mTouchPower = value;
            }
            else
            {
                Debug.LogError("Touch Power Update ERROR "+ value);
            }
        }
    }

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
        mTouchPower = 1;

        ClacStage();
        UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
        
    }

    private void ClacStage()
    { 
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);
        //Mathf는 float이기 때문에 return 값이 double인 Using System - Math를 사용한다.
        if (mCurrentGem !=null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
        mIncome = 5 * Math.Pow(mIncomeWeight, mCurrentStage);

    }

    public void Touch()
    {
        if (mCurrentProgress >= mMaxProgress)
        {
            mGold += mIncome;
            mCurrentStage++;
            mCurrentProgress = 0;
            ClacStage();
        }
        else
        {
            mCurrentProgress += mTouchPower;
            if (mCurrentProgress >mMaxProgress)
            {
                mCurrentProgress = mMaxProgress;
            }

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
