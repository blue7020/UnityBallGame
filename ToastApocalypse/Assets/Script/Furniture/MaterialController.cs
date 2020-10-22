using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : InformationLoader
{
    public static MaterialController Instance;

    public Text mTitle, mMaterialTitle, mLore;
    public Image mMaterialWindow;

    public MaterialStat[] mInfoArr;
    public MaterialStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.MATERIAL_STAT);
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mTitle.text = "냉장고";
            mMaterialTitle.text = "재료";
            mLore.text = "재료를 터치하면 설명이 표시됩니다";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mTitle.text = "Fridger";
            mMaterialTitle.text = "Material";
            mLore.text = "Touch the Material to show description";
        }
    }

    public void ShowDescription(int id)
    {
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mMaterialTitle.text = mInfoArr[id].Title;
            mLore.text = mInfoArr[id].ContensFormat;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mMaterialTitle.text = mInfoArr[id].EngTitle;
            mLore.text = mInfoArr[id].EngContensFormat;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        mMaterialWindow.gameObject.SetActive(true);
    }
}
