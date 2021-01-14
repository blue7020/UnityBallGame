using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyrupShopController : MonoBehaviour
{
    public static SyrupShopController Instance;
    public Image mWindow;
    public Text mTitle;
    public Text[] SyrupText;
    public int[] SyrupAmount;
    public Text[] PriceText;
    public int[] KRWPrice;
    public float[] USDPrice;
    public Button[] mButtons;
    public PopUpWindow mPopupWindow;
    public string text;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (GameSetting.Instance.Language==0)
            {
                mTitle.text = "시럽 추출기";
                text = "시럽 보유량이 최대입니다!";
                for (int i = 0; i < PriceText.Length; i++)
                {
                    PriceText[i].text = "KRW\n"+KRWPrice[i].ToString();
                }
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mTitle.text = "Syrup Extractor";
                text = "You have the maximum amount of syrup!";
                for (int i = 0; i < PriceText.Length; i++)
                {
                    PriceText[i].text = "USD\n" + USDPrice[i].ToString();
                }
            }
            for (int i=0; i<SyrupText.Length;i++)
            {
                SyrupText[i].text = SyrupAmount[i].ToString();
            }
            GameSetting.Instance.TimeCheck24Ad();
            if (SaveDataController.Instance.mUser.TodayWatchFirstAD==true)
            {
                mButtons[0].interactable = false;
            }
            else
            {
                mButtons[0].interactable = true;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SyrupBuy(int id)
    {
        if (SaveDataController.Instance.mUser.Syrup>=Constants.MAX_SYRUP)
        {
            mPopupWindow.ShowWindow(text);
        }
        else
        {
            switch (id)
            {
                case 0:
                    GameSetting.Instance.ShowAds(eAdsReward.Syrup);
                    break;
                case 1:
                    IAPController.Instance.BuySyrup01();
                    break;
                case 2:
                    IAPController.Instance.BuySyrup02();
                    break;
                case 3:
                    IAPController.Instance.BuySyrup03();
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
