using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;//버튼 써야할 때 주로 씀

public class Item
{
    public string title;
    public string Contents;
    public int Level;
    public double Cost;
    public double Value;
    public int Type;
}
public class PlayerSkill
{
    public int ID;
    public string title;
    public string Contents;
    public int Level;
    public int MaxLevel;
}

public class UIElement : MonoBehaviour
{
    private int mID;
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mContentsText, mLevelText, mCostText;
    [SerializeField]
    private Button mPurchaseButton;

    public void Init(int id, string name, string contents, int level, double cost, Delegates.IntInVoidRetrun callback)
    {
        mID = id;
        mTitleText.text = name;
        mContentsText.text = contents;
        mLevelText.text = "Lv."+level.ToString();
        mCostText.text = cost.ToString();
        mPurchaseButton.onClick.AddListener(()=>{callback(mID); });
    }
}
