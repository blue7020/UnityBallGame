using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerController : InformationLoader //LoadJson을 사용하기 위해 상속
{
    public static CoworkerController Instance;
    [SerializeField]
    private CoworkerInfo[] mInfoArr;
    [SerializeField]
    private CoworkerTextInfo[] mTextInforArr;

    private int[] mLevelArr;

#pragma warning disable 0649
    [SerializeField]
    private Sprite[] mIconArr;

    [SerializeField]
    private Coworker[] mCoworkerArr;
    

    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;
#pragma warning restore 0649
    private List<UIElement> mElementList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadJson(out mInfoArr, Paths.COWORKER_INFO_TABLE);
        LoadJson(out mTextInforArr, Paths.COWORKER_TEXT_INFO_TABLE);

        mLevelArr = GameController.Instance.GetCoworkerLevelArr();

        mElementList = new List<UIElement>();
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            if (mLevelArr[i]<0)
            {
                continue;
            }
            mInfoArr[i].CurrentLevel = mLevelArr[i];
            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWight, 10) - 1) / (mInfoArr[i].CostWight-1);

            CalcData(i);

            if (mInfoArr[i].CurrentLevel>0)
            {
                mCoworkerArr[i].gameObject.SetActive(true);//0번 동료 소환
                mCoworkerArr[i].Startwork(i, mInfoArr[i].PeriodCurrent);
            }

            UIElement element = Instantiate(mElementPrefab, mElementArea);
            string valueStr;
            if (mInfoArr[i].ValueCalcType == eCalculationType.Exp)
            {
                valueStr = UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent);
            }
            else
            {
                valueStr = mInfoArr[i].ValueCurrent.ToString("N1");
            }

            element.Init(i, mIconArr[i],
                mTextInforArr[i].Title,
                mInfoArr[i].CurrentLevel.ToString(),
                string.Format(mTextInforArr[i].ContentsFormat,
                              valueStr,
                              mInfoArr[i].PeriodCurrent.ToString()),
                UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),LevelUP);

            mElementList.Add(element);
        }
    }


    public void JobFinish(int id, Vector3 effectPos)
    {
        switch (id)
        {
            case 0:
                Debug.Log(mInfoArr[id].ValueCurrent);
                GameController.Instance.Gold += mInfoArr[id].ValueCurrent;
                break;
            case 1:
                GameController.Instance.PowerTouch(mInfoArr[id].ValueCurrent);
                break;
            case 2://주기 동작이 아닌 동료
                //스킬 쿨타임 감소
                break;
            default:
                Debug.LogError("wrong coworker id " + id);
                break;
        }
    }


    public void LevelUP(int id, int amount)
    {
        //int id = mSelectedID;
        //int level =mSelectedAmount;
        Delegates.VoidCallback callback = () => { LevelUPCallback(id, amount); };
        switch (mInfoArr[id].CostType)
        {
            case eCostType.Gold:
                {
                    GameController.Instance.GoldCallback = callback; //+=를 사용하면 중첩될 수 있기 때문에 쓰면 안된다. 대부분의 경우 중첩할 필요가 없다.
                    double cost = mInfoArr[id].CostCurrent;
                    if (amount == 10)
                    {
                        cost *= mInfoArr[id].CostTenWeight;
                    }
                    GameController.Instance.Gold -= cost;
                }
                break;
            case eCostType.Ruby:
                {
                    double cost = 10 * amount;
                }
                break;
            case eCostType.Soul:
                break;
            default:
                Debug.LogError("worng cost type " + mInfoArr[id].CostType);
                break;
        }


    }



    public void LevelUPCallback(int id, int level)
    {
        mInfoArr[id].CurrentLevel += level;
        if (mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetbuttonActive(false);
        }
        if (mInfoArr[id].CurrentLevel + 10 > mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetbuttonActive(false);
        }
        mLevelArr[id] = mInfoArr[id].CurrentLevel;

        

        if (mInfoArr[id].CurrentLevel==1)
        {
            mCoworkerArr[id].gameObject.SetActive(true);//다른 동료 소환
            if (id + 1 < mInfoArr.Length)
            {
                int nextID = id + 1;
                mLevelArr[nextID] = mInfoArr[nextID].CurrentLevel = 0;
                CalcData(nextID);

                UIElement element = Instantiate(mElementPrefab, mElementArea);
                string valueStrNext;
                if (mInfoArr[nextID].ValueCalcType == eCalculationType.Exp)
                {
                    valueStrNext = UnitSetter.GetUnitStr(mInfoArr[nextID].ValueCurrent);
                }
                else
                {
                    valueStrNext = mInfoArr[nextID].ValueCurrent.ToString("N1");
                }

                element.Init(nextID, mIconArr[nextID],
                    mTextInforArr[nextID].Title,
                    mInfoArr[nextID].CurrentLevel.ToString(),
                    string.Format(mTextInforArr[nextID].ContentsFormat,
                                  valueStrNext,
                                  mInfoArr[nextID].PeriodCurrent.ToString()),
                    UnitSetter.GetUnitStr(mInfoArr[nextID].CostCurrent),
                    UnitSetter.GetUnitStr(mInfoArr[nextID].CostCurrent * mInfoArr[nextID].CostTenWeight), null);

                mElementList.Add(element);
            }
            
        }

        CalcData(id);

        mCoworkerArr[id].Startwork(id, mInfoArr[id].PeriodCurrent);
        //계산된 값 적용 UI, GameLogic
        string valueStr;
        if (mInfoArr[id].ValueCalcType == eCalculationType.Exp)
        {
            valueStr = UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent);
        }
        else
        {
            valueStr = mInfoArr[id].ValueCurrent.ToString("N1");
        }
        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                    string.Format(mTextInforArr[id].ContentsFormat,
                                  valueStr,
                                  mInfoArr[id].PeriodCurrent.ToString()), //string.Format과 한 덩어리라서 이렇게 들여쓰는 것
                    UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent),
                    UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent *
                                  mInfoArr[id].CostTenWeight));
    }

    private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                     Math.Pow(mInfoArr[id].CostTenWeight, mInfoArr[id].CurrentLevel);
        if (mInfoArr[id].ValueCalcType ==eCalculationType.Sum)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        }

        float periodSub = mInfoArr[id].PeriodUpgreadeAmount *
                                     (int)(mInfoArr[id].CurrentLevel / mInfoArr[id].PeriodLevelStep);
        //레벨이 0보다 클 때 적용
        if (mInfoArr[id].CurrentLevel > 0)
        {
            mInfoArr[id].PeriodCurrent = mInfoArr[id].PeriodBase - periodSub;

            switch (id)
            {
                case 0://주기 동작을 하는 동료
                    
                    break;
                case 1:
                    break;//주기 동작을 하는 동료
                case 2:
                    //TODO 스킬 쿨타임 감소
                    break;
                default:
                    Debug.LogError("wrong id value on Coworker" + id);
                    break;

            }
        }


    }
}