using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFinder : MonoBehaviour
{
    public static CanvasFinder Instance;

    public Text[] mShopPriceText;
    public Text[] mStatuePriceText;
    public Text mSlotPriceText, mCriticalText;
    public Transform mCanvas;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mShopPriceText = new Text[3];
            mStatuePriceText = new Text[3];
            if (GameController.Instance.IsTutorial == false)
            {
                if (GameSetting.Instance.Language == 0)
                {
                    mCriticalText.text = "치명타!";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mCriticalText.text = "Critical!";
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DeleteShopPrice(int index)
    {
        mShopPriceText[index].gameObject.SetActive(false);
    }
    public void DeleteSlotPrice()
    {
        mSlotPriceText.gameObject.SetActive(false);
    }

    public void DeletdStatuePrice(int index)
    {
        mStatuePriceText[index].gameObject.SetActive(false);
    }
}
