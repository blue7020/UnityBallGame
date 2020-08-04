using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatueController : InformationLoader
{
    public static StatueController Instance;

    public const int STATUE_COUNT = 3;//맵에 배치되는 석상 개수

    public Statue mStatue;
    public Statue[] mStatueArr;
    public Transform[] mStatuePos;
    public List<int> StatueIDList;
    public int TextIndex;

    public Sprite[] mSprites;

    public Text mPriceText;
    public StatuetStat[] mInfoArr;

    public StatuetStat[] GetInfoArr()
    {
        return mInfoArr;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.STATUE_STAT);
            StatueIDList = new List<int>();
            TextIndex = 0;
            for (int i=0; i< GameSetting.Instance.StatueOpen.Length; i++)
            {
                if (GameSetting.Instance.StatueOpen[i]==true)
                {
                    StatueIDList.Add(i);
                }
                mStatueArr = new Statue[STATUE_COUNT];
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        for (int i = 0; i < mStatueArr.Length; i++)
        {
            mStatueArr[i] = Instantiate(mStatue, mStatuePos[i]);
            int rand = Random.Range(0, StatueIDList.Count);
            mStatueArr[i].mStatueID = StatueIDList[rand];
            mStatueArr[i].mID = i;
            mStatueArr[i].mPriceText = Instantiate(mPriceText, CanvasFinder.Instance.transform);
            CanvasFinder.Instance.mStatuePriceText[i] = mStatueArr[i].mPriceText;
            if (i == 1)
            {
                mStatueArr[i].ePayType = eStatuePay.Free;
                mStatueArr[i].SpendGold = 0;

            }
            else
            {
                mStatueArr[i].ePayType = eStatuePay.Pay;
                mStatueArr[i].SpendGold = 35;
            }
            CanvasFinder.Instance.mStatuePriceText[i].gameObject.SetActive(false);
            CanvasFinder.Instance.mStatuePriceText[i].text = mStatueArr[i].SpendGold.ToString()+"G";
            mStatueArr[i].StatSetting();
            StatueIDList.RemoveAt(rand);
        }
    }

}
