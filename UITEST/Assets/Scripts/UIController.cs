using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private Text mPlayerStatName, mLevelText, mHPText, mMPText,mEXPText;
    [SerializeField]
    private Text mPlayerStatMenuName, mStats;
    public Sprite nullImage;
    private int mLevel, mAtk, mDef;
    private float mMaxHP, mCurrentHP, mMaxMP,mCurrentMP, mMaxEXP,mCurrentEXP;

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

    public void ShowPlayerStat()
    {
        Player.Instance.GetStats(mLevel, mAtk, mDef, mMaxHP, mCurrentHP, mMaxMP, mCurrentMP, mMaxEXP, mCurrentEXP);
        mPlayerStatName.text = Player.Instance.Name;
        mPlayerStatMenuName.text = Player.Instance.Name;
        mLevelText.text = mLevel.ToString();
        mStats.text = "HP: "+mCurrentHP+"/"+mMaxHP+"\tMP: "+mCurrentMP+"/"+mMaxMP+ "\nAtk: "+mAtk+"\tDef: "+mDef; 
    }

    public void ShowPlayerGaugeText()
    {
        mHPText.text = "HP: " + mCurrentHP + "/" + mMaxHP;
        mMPText.text = "MP: " + mCurrentMP + " / " + mMaxMP;
        mEXPText.text = mCurrentEXP + "/" + mMaxEXP;
    }
}
