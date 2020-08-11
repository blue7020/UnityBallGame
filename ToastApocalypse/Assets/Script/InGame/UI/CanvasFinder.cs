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
    public Text mSlotPriceText;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mShopPriceText = new Text[3];
            mStatuePriceText = new Text[3];
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
