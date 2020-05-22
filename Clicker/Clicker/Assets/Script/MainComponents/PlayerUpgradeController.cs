using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : InformationLoader
    //InformationLoader가 MonoBehaviour를 상속 받고 있으니 이 스크립트도 MonoBehaviour의 기능을 사용 가능하다.
{
    public static PlayerUpgradeController Instance;

    public int[] mLevelArr;
    [SerializeField]
    private PlayerStat[] mInfoArr;

    [SerializeField]
    private PlayerStatText[] mTextInforArr;
    private Sprite[] mIconArr;

    private List<UIElement> mElementList;
#pragma warning disable 0649 //인지한 애들만 warning을 제외하기
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    //모든 테이블이 준비되면 JsonGeneator를 비활성화
    public PlayerStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    public PlayerStatText[] GetTextInfoArr()
    {
        return mTextInforArr;
    }

    [SerializeField]
    private SkillButton[] mSkillButtonArr;
#pragma warning restore 0649
    [SerializeField]
    private float[] mSkillCooltimeArr, mSkillMaxCooltimeArr;
    [SerializeField]
    private List<int> mSkillIndexList;
    //쿨타임 감소
    private float mSkillDiscount;
    public float SkillDiscount
    {
        get { return mSkillDiscount; }
        set
        {
            mSkillDiscount = value;
            for (int i=0; i<mSkillMaxCooltimeArr.Length;i++)
            {
                mSkillMaxCooltimeArr[i] = mInfoArr[mSkillIndexList[i]].Cooltime - mSkillDiscount;
            }
        }
    }


    private int mSelectedID, mSelectedAmount;//드래그 시 잡고 있는 것의 정보를 들고 있다.

    private void Awake()
    {
        if (Instance ==null)
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
        //제너릭 : 다양한 타입에 대해 대응하기 위해 사용
        LoadJson(out mInfoArr, Paths.PLAYER_ITEM_TABLE);
        LoadJson(out mTextInforArr, Paths.PLAYER_ITEM_TEXT_TABLE);

        mIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_ITEM_ICON); //컨스트 변수는 항상 대문자에 _ 를 써주는 것이 좋다.
                                                                      //얘는 바꿀 수 없는 변수다 라는것을 명시하는 것)
        //세이브 데이터 불러오기
        mLevelArr = GameController.Instance.GetPlayerItemLevelArr();
        mSkillCooltimeArr = GameController.Instance.GetSkillCoolTimeArr();
        mSkillMaxCooltimeArr = GameController.Instance.GetSkillMaxCoolTimeArr();

        mSkillIndexList = new List<int>();
        for (int i = 0; i < mInfoArr.Length; i++)
        {
                if (mInfoArr[i].Cooltime > 0)
                {
                    mSkillIndexList.Add(i);
                }

                mInfoArr[i].CurrentLevel = mLevelArr[i];
                mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWight, 10) - 1) / (mInfoArr[i].CostWight - 1);

                CalcData(i);
            
        }

        mElementList = new List<UIElement>();
        for (int i=0; i<mInfoArr.Length; i++)
        {
            
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            string valueStr = mInfoArr[i].IsPercent ? mInfoArr[i].ValueCurrent.ToString("P0") : UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent);//참이면 왼쪽 거짓이면 오른쪽
            //if문으로 만들어도 됨

            elem.Init(i, mIconArr[i],
                mTextInforArr[i].Title,
                mInfoArr[i].CurrentLevel.ToString(),
                string.Format(mTextInforArr[i].ContentsFormat,
                              valueStr, 
                              mInfoArr[i].Duration.ToString()),
                UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),
                LevelUP);//엘리먼트와 enum의 id값은 같게 설정.
            mElementList.Add(elem);
        }

        for (int i=0; i<mSkillButtonArr.Length; i++)
        {
            int SkillID = mSkillIndexList[i];
            if (mInfoArr[SkillID].CurrentLevel > 0)
            {
                mSkillButtonArr[i].SetButtonActive(true);
            }
            StartCoroutine(CooltimeRoutine(i, mSkillMaxCooltimeArr[i]));
        }
    }

    public void ActiveSkill(int buttonID)
    {
        int infoID = mSkillIndexList[buttonID];

        switch (infoID)
        {
            case 3:
                GameController.Instance.PowerTouch(mInfoArr[infoID].ValueCurrent);
                break;
            case 4:
                StartCoroutine(GoldBonusRoutine(mInfoArr[infoID].Duration, mInfoArr[infoID].ValueCurrent));
                break;
            default:
                Debug.LogError("wrong skill info id: " + infoID);
                break;
        }

        mSkillCooltimeArr[buttonID] = mSkillMaxCooltimeArr[buttonID];
        StartCoroutine(CooltimeRoutine(buttonID, mSkillMaxCooltimeArr[buttonID]));
        
    }

    private IEnumerator GoldBonusRoutine(float duration, double value)
    {
        GameController.Instance.IncomeBonus += value;
        yield return new WaitForSeconds(duration);
        GameController.Instance.IncomeBonus -= value;
    }

    //Max Cooltime
    private IEnumerator CooltimeRoutine(int buttonId, float cooltime)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();

        while (mSkillCooltimeArr[buttonId]>=0)
        {
            yield return frame;
            mSkillCooltimeArr[buttonId] -= Time.fixedDeltaTime;
            mSkillButtonArr[buttonId].ShowCooltime(mSkillCooltimeArr[buttonId], cooltime);
        }
    }

    public void LevelUP(int id,int amount)
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

        CalcData(id);

        //계산된 값 적용 UI, GameLogic
        string valueStr = mInfoArr[id].IsPercent ? mInfoArr[id].ValueCurrent.ToString("P0") : UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent);//참이면 왼쪽 거짓이면 오른쪽
        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                    string.Format(mTextInforArr[id].ContentsFormat,
                                  valueStr,
                                  mInfoArr[id].Duration.ToString()), //string.Format과 한 덩어리라서 이렇게 들여쓰는 것
                    UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent),
                    UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent *
                                  mInfoArr[id].CostTenWeight));
    }

    private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                     Math.Pow(mInfoArr[id].CostTenWeight, mInfoArr[id].CurrentLevel);
        if (mInfoArr[id].IsPercent)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        }

        //레벨이 0보다 클 때 적용
        if (mInfoArr[id].CurrentLevel>0)
        {

            switch (id)
            {
                case 0:
                    GameController.Instance.TouchPower = mInfoArr[id].ValueCurrent;
                    break;
                case 1:
                    GameController.Instance.CriticalRate = mInfoArr[id].ValueCurrent;
                    break;
                case 2:
                    GameController.Instance.CriticalValue = mInfoArr[id].ValueCurrent;
                    break;
                case 3:
                case 4:
                    int buttonID = 0;
                    for (int i = 0; i < mSkillIndexList.Count; i++)
                    {
                        if (mSkillIndexList[i] == id)
                        {
                            buttonID = i;
                            break;
                        }
                    }
                    mSkillMaxCooltimeArr[buttonID] = mInfoArr[id].Cooltime - mSkillDiscount;
                    mSkillButtonArr[buttonID].SetButtonActive(true);
                    break;
                default:
                    Debug.LogError("wrong id value on player stats" + id);
                    break;

            }
        }


    }

}
