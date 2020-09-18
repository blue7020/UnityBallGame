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

    public Sprite[] mStatueSprites;

    public Text mPriceText;
    public StatueStat[] mStatInfoArr;

    public StatueStat[] GetStatInfoArr()
    {
        return mStatInfoArr;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mStatInfoArr, Path.STATUE_STAT);
            StatueIDList = new List<int>();
            mStatueSprites = GameSetting.Instance.mStatueSprites;
            for (int i = 0; i < GameSetting.Instance.StatueOpen.Length; i++)
            {
                if (GameSetting.Instance.StatueOpen[i] == true)
                {
                    StatueIDList.Add(i);
                }
            }
            mStatueArr = new Statue[STATUE_COUNT];
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        if (GameSetting.Instance.NowStage != 0)
        {
            for (int i = 0; i < STATUE_COUNT; i++)
            {
                mStatueArr[i] = Instantiate(mStatue, mStatuePos[i]);
                int rand = Random.Range(0, StatueIDList.Count);
                mStatueArr[i].mStatueID = StatueIDList[rand];
                StatueIDList.RemoveAt(rand);
                mStatueArr[i].mID = rand;
                mStatueArr[i].mPriceText = Instantiate(mPriceText, CanvasFinder.Instance.transform);
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
                CanvasFinder.Instance.mStatuePriceText[i] = mStatueArr[i].mPriceText;
                CanvasFinder.Instance.mStatuePriceText[i].text = mStatueArr[i].SpendGold.ToString() + "G";
                CanvasFinder.Instance.mStatuePriceText[i].gameObject.SetActive(false);
                mStatueArr[i].StatSetting(rand);
            }
        }
        else
        {
            for (int i = 0; i < STATUE_COUNT; i++)
            {
                mStatueArr[i] = Instantiate(mStatue, mStatuePos[i]);
                mStatueArr[i].mID = i+1;
                mStatueArr[i].mPriceText = Instantiate(mPriceText, CanvasFinder.Instance.transform);
                mStatueArr[i].ePayType = eStatuePay.Free;
                mStatueArr[i].SpendGold = 0;
                CanvasFinder.Instance.mStatuePriceText[i] = mStatueArr[i].mPriceText;
                CanvasFinder.Instance.mStatuePriceText[i].text = mStatueArr[i].SpendGold.ToString() + "G";
                CanvasFinder.Instance.mStatuePriceText[i].gameObject.SetActive(false);
                mStatueArr[i].StatSetting(i+1);
            }
        }

    }

}
