using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : InformationLoader
    //InformationLoader가 MonoBehaviour를 상속 받고 있으니 이 스크립트도 MonoBehaviour의 기능을 사용 가능하다.
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private PlayerStat[] minfoArr;

    [SerializeField]
    private PlayerStatText[] mTextInforArr;
    private Sprite[] mIconArr;

    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

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
        LoadJson(out minfoArr, Paths.PLAYER_ITEM_TABLE);
        LoadJson(out mTextInforArr, Paths.PLAYER_ITEM_TEXT_TABLE);

        mIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_ITEM_ICON); //컨스트 변수는 항상 대문자에 _ 를 써주는 것이 좋다.
                                                                      //얘는 바꿀 수 없는 변수다 라는것을 명시하는 것임

        //세이브 데이터 불러오기

        for (int i = 0; i < minfoArr.Length; i++)
        {
            minfoArr[i].CostTenWeight = (Math.Pow(minfoArr[i].CostWight, 10) - 1) / (minfoArr[i].CostWight - 1);
        }

        mElementList = new List<UIElement>();
        for (int i=0; i<minfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            elem.Init(i, mIconArr[i],
                mTextInforArr[i].Title,
                minfoArr[i].CurrentLevel.ToString(),
                string.Format(mTextInforArr[i].ContentsFormat,
                              UnitSetter.GetUnitStr(minfoArr[i].ValueCurrent),
                              minfoArr[i].Duration.ToString()),
                UnitSetter.GetUnitStr(minfoArr[i].CostCurrent),
                UnitSetter.GetUnitStr(minfoArr[i].CostCurrent * minfoArr[i].CostTenWeight),
                LevelUP);//엘리먼트와 enum의 id값은 같게 설정.
            mElementList.Add(elem);
        }
        
        
    }

    private int mSelectedID, mSelectedAmount;//드래그 시 잡고 있는 것의 정보를 들고 있다.

    
    public void LevelUP(int id,int amount)
    {
        //int id = mSelectedID;
        //int level =mSelectedAmount;
        Delegates.VoidCallback callback = () => { LevelUPCallback(id, amount); };
        switch (minfoArr[id].CostType)
        {
            case eCostType.Gold:
                {
                    GameController.Instance.GoldCallback = callback; //+=를 사용하면 중첩될 수 있기 때문에 쓰면 안된다. 대부분의 경우 중첩할 필요가 없다.
                    double cost = minfoArr[id].CostCurrent;
                    if (amount == 10)
                    {
                        cost *= minfoArr[id].CostTenWeight;
                    }
                    GameController.Instance.Gold -= minfoArr[id].CostCurrent;
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
                Debug.LogError("worng cost type " + minfoArr[id].CostType);
                break;
        }
    }

    public void LevelUPCallback(int id, int level)
    {
        //int id = mSelectedID;
        //int level =mSelectedAmount;
        minfoArr[id].CurrentLevel += level;
        if (minfoArr[id].CurrentLevel <= minfoArr[id].MaxLevel)
        {
            //레벨업 잠금
        }
        minfoArr[id].CostCurrent = minfoArr[id].CostBase * Math.Pow(minfoArr[id].CostWight, minfoArr[id].CurrentLevel);

        if (minfoArr[id].IsPercent)
        {
            minfoArr[id].ValueCurrent = minfoArr[id].ValueBase + minfoArr[id].ValueWeight * minfoArr[id].CurrentLevel;
        }
        else
        {
            minfoArr[id].ValueCurrent = minfoArr[id].ValueBase * Math.Pow(minfoArr[id].ValueWeight, minfoArr[id].CurrentLevel);
        }

        //계산된 값 적용 UI, GameLogic
        if(minfoArr[id].Cooltime <= 0)
        {
            switch (id)
            {
                case 0:
                    GameController.Instance.TouchPower = minfoArr[id].ValueCurrent;
                    break;
                case 1:
                    GameController.Instance.CriticalRate = minfoArr[id].ValueCurrent;
                    break;
                case 2:
                    GameController.Instance.CriticalValue = minfoArr[id].ValueCurrent;
                    break;
                default:
                    Debug.LogError("wrong cooltime value on player stats" + id);
                    break;

            }
            mElementList[id].Refresh(minfoArr[id].CurrentLevel.ToString(),
                string.Format(mTextInforArr[id].ContentsFormat,
                              UnitSetter.GetUnitStr(minfoArr[id].ValueCurrent),
                              minfoArr[id].Duration.ToString()), //string.Format과 한 덩어리라서 이렇게 들여쓰는 것
                UnitSetter.GetUnitStr(minfoArr[id].CostCurrent),
                UnitSetter.GetUnitStr(minfoArr[id].CostCurrent *
                              minfoArr[id].CostTenWeight));
        }
    }
}
