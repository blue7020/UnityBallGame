﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerController : InformationLoader //LoadJson을 사용하기 위해 상속
{
    public static CoworkerController Instance;
    [SerializeField]
    private CoworkerInfo[] mInfoArr;
    [SerializeField]
    private CoworkerTextInfo[] mTextInfoArr;
#pragma warning disable 0649
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
        LoadJson(out mTextInfoArr, Paths.COWORKER_TEXT_INFO_TABLE);

        

        mElementList = new List<UIElement>();
        for (int i = 0; i < 3; i++)
        {

            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWight, 10) - 1) / (mInfoArr[i].CostWight-1);

            //calc
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

            element.Init(i, null,
                mTextInfoArr[i].Title,
                mInfoArr[i].CurrentLevel.ToString(),
                string.Format(mTextInfoArr[i].ContentsFormat,
                              valueStr,
                              mInfoArr[i].PeriodCurrent.ToString()),
                UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),null);

            //내용 채우기
            mElementList.Add(element);
        }
    }
}