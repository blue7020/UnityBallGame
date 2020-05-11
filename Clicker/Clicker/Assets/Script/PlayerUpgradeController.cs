using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; //Unity의 내장 json은 enum을 못 넣기 때문에 Assets의 Plugin에 들어간 파일을 넣어줘야한다.

public class PlayerUpgradeController : MonoBehaviour
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private PlayerStat[] minfoArr;
    [SerializeField]
    private PlayerStat[] Test;

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
        //이런 방법도 있다.
        //minfoArr = new PlayerStat[5];
        //minfoArr[0] = new PlayerStat();
        //minfoArr[0].ID = 0;
        //minfoArr[0].CurrentLevel = 1;
        //minfoArr[0].CostType = eCostType.Gold;

        string data = JsonConvert.SerializeObject(minfoArr);//minfoArr을 Json화 시키기(=텍스트 파일로 변환)
        Debug.Log(data);
        Test = JsonConvert.DeserializeObject<PlayerStat[]>(data); //텍스트 파일로 변환된 data를 불러오기

        //save data load
        mElementList = new List<UIElement>();
        for (int i=0; i<minfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            elem.Init(i, null, "test1", "1", "power up", "2", LevelUP);//엘리먼트와 enum의 id값은 같게 설정.
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
                GameController.Instance.GoldCallback = callback; //+=를 사용하면 중첩될 수 있기 때문에 쓰면 안된다. 대부분의 경우 중첩할 필요가 없다.
                GameController.Instance.Gold -= minfoArr[id].CostCurrent;
                break;
            case eCostType.Ruby:
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
    }
}
