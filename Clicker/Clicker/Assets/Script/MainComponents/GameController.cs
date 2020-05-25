using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : SaveDataController
{
    private int[] mLevelArr;
    public static GameController Instance;

    public Delegates.VoidCallback GoldCallback;//골드를 성공적으로 썼을때만 발동
    public double Gold
    {
        get { return mUser.Gold; } //get을 하지 않아주면 +=같은 수식이 먹히지 않는다.
        set
        {
            if (value >=0)
            {
                mUser.Gold = value;
                //GoldCallback?.Invoke(); //Unity의 Invoke와 Delegate의 Invoke와는 다르다!
                //위와 아래는 같은 코드
                if (GoldCallback!=null)
                {
                    GoldCallback();
                }

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
    public double IncomeBonus { get; set; }

    [SerializeField]
    private double mProgressWeight = 1.08d;//프로그래스 최댓값 배수
    //소숫점의 기본은 Double이다. 안해도 상관은 없지만 구분을 위해 소숫점 뒤에 d를 붙인다.
    private double mMaxProgress;
    [SerializeField]
    private double mTouchPower;
    public double TouchPower
    {
        get { return mTouchPower; }
        set
        {
            //Debug.LogError("SettingTouchPower");
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

    public double CriticalRate { get; set; }

    public double CriticalValue { get; set; }

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
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called beforethe first frame update
    void Start()
    {
        CalcStage(mUser.LastGemID);
        mCurrentGem.SetProgress((float)(mUser.Progress / mMaxProgress));
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
        
    }

    
    private void OnApplicationQuit()
    {
        //게임이 종료될 때 적용
        Save();
    }

    public int[] GetPlayerItemLevelArr()
    {
        return mUser.PlayerItemLevelArr;
    }

    public float[] GetSkillCoolTimeArr()
    {
        return mUser.SkillCooltimeArr;
    }

    public float[] GetSkillMaxCoolTimeArr()
    {
        return mUser.SkillMaxCooltimeArr;
    }

    public int[] GetCoworkerLevelArr()
    {
        return mUser.CoworkerLevelArr;
    }

    public void Rebirth()
    {
        if (mUser.Stage >= 100)
        {
            #region 소울 지급
            double reward = mUser.Stage * 2;
            int levelTotal = 0;
            for (int i = 0; i < mUser.PlayerItemLevelArr.Length; i++)
            {
                levelTotal += mUser.PlayerItemLevelArr[i];
            }
            for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
            {
                if (mUser.CoworkerLevelArr[i] > 0)
                {
                    levelTotal += mUser.CoworkerLevelArr[i];
                }
            }
            reward += levelTotal;
            mUser.Soul += reward;
            #endregion

            #region 레벨 및 스킬 쿨타임 초기화
            mUser.PlayerItemLevelArr = new int[Constants.PLAYER_ITEM_COUNT];
            mUser.PlayerItemLevelArr[0] = 1;//터치하는 것이기 때문
            mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
            mUser.SkillMaxCooltimeArr = new float[Constants.SKILL_COUNT];

            mUser.CoworkerLevelArr = new int[Constants.COWORKER_COUNT];
            for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
            {
                mUser.CoworkerLevelArr[i] = -1;
            }
            mUser.CoworkerLevelArr[0] = 0;
            #endregion

            CoworkerController.Instance.Rebirth(mUser.CoworkerLevelArr);
        }
        else
        {
            Debug.Log("환생 조건이 충족되지 않았습니다.");//Popup
        }
        //UI 초기화(Player Item)
        //Gold 초기화
        //Stage, CurrentProgress 초기화

    }

    private void CalcStage(int id = -1)
    { 
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage);
        //Mathf는 float이기 때문에 return 값이 double인 Using System - Math를 사용한다.
        if (mCurrentGem !=null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        if (mUser.LastGemID <0)
        {
            mUser.LastGemID = UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT);
        }
        mCurrentGem = mGemPool.GetFromPool(mUser.LastGemID);
        mIncome = 5 * Math.Pow(mIncomeWeight, mUser.Stage);

    }

    public void PowerTouch(double value)
    {
        if (value < 0)
        {
            Debug.LogError("wrong power touch value " +value);
        }
        mUser.Progress += value;

        if(mUser.Progress >= mMaxProgress)
        {
            mUser.Gold += mIncome;
            mUser.Stage++;
            mUser.Progress = 0;
            CalcStage();
        }

        float progress = (float)(mUser.Progress / mMaxProgress);//이미지의 fillamount가 있기 때문에 float을 사용한다
        mCurrentGem.SetProgress(progress);
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

    public void Touch()
    {
        if (mUser.Progress >= mMaxProgress)
        {
            mUser.Gold += mIncome;
            mUser.Stage++;
            mUser.Progress = 0;
            CalcStage();
        }
        else
        {
            double touchPower = mTouchPower;//총 데미지
            if (CriticalRate > UnityEngine.Random.Range(0, 1f))
            {
                touchPower = touchPower * (1 + CriticalValue);
            }

            mUser.Progress += mTouchPower;
            if (mUser.Progress >mMaxProgress)
            {
                mUser.Progress = mMaxProgress;
            }

            float progress = (float)(mUser.Progress / mMaxProgress);//이미지의 fillamount가 있기 때문에 float을 사용한다
            mCurrentGem.SetProgress(progress);
        }
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

}
