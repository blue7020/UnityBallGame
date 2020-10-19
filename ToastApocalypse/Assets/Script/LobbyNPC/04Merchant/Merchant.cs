using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Merchant : MonoBehaviour
{
    public static Merchant Instance;

    public Image mWindow,mItemImage;
    public Text mTitle, mItemTitle, mItemText,mBuyText;
    public Button RightButton, LeftButton, BuyButton;
    public Sprite[] mspt;
    public int NowItemID;
    public string[] PriceKor;
    public string[] PriceEng;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            NowItemID = 0;
            ItemSetting();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameSetting.Instance.Language==0)
        {
            mTitle.text = "상점";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitle.text = "Shop";
        }
        ItemSetting();
    }

    public void BuyItem()
    {
        switch (NowItemID)
        {
            case 0://광고 제거
                break;
            case 1://히어로
                break;
            case 2://카스테라
                break;
            case 3://쉬림프닌자
                break;
            case 4://데몬토스트
                break;
        }
        SaveDataController.Instance.Save();
    }

    public void LeftCharacterSelect()
    {
        NowItemID -= 1;
        ItemSetting();
        if (NowItemID - 1 < 0)
        {
            LeftButton.gameObject.SetActive(false);
        }
        RightButton.gameObject.SetActive(true);
    }
    public void RightCharacterSelect()
    {
        NowItemID += 1;
        ItemSetting();
        if (NowItemID + 1 >= mspt.Length)
        {
            RightButton.gameObject.SetActive(false);
        }
        LeftButton.gameObject.SetActive(true);
    }

    public void ItemSetting()
    {
        mItemImage.sprite = mspt[NowItemID];
        switch (NowItemID)
        {
            case 0://
                if (SaveDataController.Instance.mUser.NoAds==false)
                {
                    if (Application.systemLanguage == SystemLanguage.Korean)
                    {
                        mItemTitle.text = "광고 제거";
                        mItemText.text = "모든 보상을 광고 없이 획득할 수 있습니다";
                        mBuyText.text = PriceKor[0];
                    }
                    else
                    {
                        mItemTitle.text = "광고 제거";
                        mItemText.text = "모든 보상을 광고 없이 획득할 수 있습니다";
                        mBuyText.text = PriceEng[0];
                    }
                    BuyButton.interactable = true;
                }
                else
                {
                    if(GameSetting.Instance.Language==0){
                        mBuyText.text = "보유중";
                    }
                    else if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "Get";
                    }
                    BuyButton.interactable = false;
                }
                break;
            case 1://히어로
                if (SaveDataController.Instance.mUser.CharacterHas[8] == false)
                {
                    ShowStat(8);
                    if (Application.systemLanguage == SystemLanguage.Korean)
                    {
                        mBuyText.text = PriceKor[1];
                    }
                    else
                    {
                        mBuyText.text = PriceEng[1];
                    }
                    BuyButton.interactable = true;
                }
                else
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "보유중";
                    }
                    else if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "Get";
                    }
                    BuyButton.interactable = false;
                }
                break;
            case 2://카스테라
                if (SaveDataController.Instance.mUser.CharacterHas[9] == false)
                {
                    ShowStat(9);
                    if (Application.systemLanguage == SystemLanguage.Korean)
                    {
                        mBuyText.text = PriceKor[2];
                    }
                    else
                    {
                        mBuyText.text = PriceEng[2];
                    }
                    BuyButton.interactable = true;
                }
                else
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "보유중";
                    }
                    else if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "Get";
                    }
                    BuyButton.interactable = false;
                }
                break;
            case 3://쉬림프 닌자
                if (SaveDataController.Instance.mUser.CharacterHas[10] == false)
                {
                    ShowStat(10);
                    if (Application.systemLanguage == SystemLanguage.Korean)
                    {
                        mBuyText.text = PriceKor[3];
                    }
                    else
                    {
                        mBuyText.text = PriceEng[3];
                    }
                    BuyButton.interactable = true;
                }
                else
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "보유중";
                    }
                    else if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "Get";
                    }
                    BuyButton.interactable = false;
                }
                break;
            case 4://데몬 토스트
                if (SaveDataController.Instance.mUser.CharacterHas[11] == false)
                {
                    ShowStat(11);
                    if (Application.systemLanguage == SystemLanguage.Korean)
                    {
                        mBuyText.text = PriceKor[4];
                    }
                    else
                    {
                        mBuyText.text = PriceEng[4];
                    }
                    BuyButton.interactable = true;
                }
                else
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "보유중";
                    }
                    else if (GameSetting.Instance.Language == 0)
                    {
                        mBuyText.text = "Get";
                    }
                    BuyButton.interactable = false;
                }
                break;
        }
    }

    public void ShowStat(int ID)
    {
        if (GameSetting.Instance.Language == 0)
        {//한국어
            string Stat = string.Format("최대 체력: {0}\n" +
                                      "공격력: {1}\n" +
                                      "방어력: {2}\n" +
                                      "이동속도: {3}\n" +
                                      "치명타 확률: {4}\n" +
                                      "치명타 피해: {5}\n" +
                                      "\n" +
                                      "쿨타임 감소: {6}\n" +
                                      "상태이상 저항: {7}", SaveDataController.Instance.mPlayerInfoArr[ID].Hp.ToString("N1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].Atk.ToString("N1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].Def.ToString("N1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].Spd.ToString("N1"), SaveDataController.Instance.mPlayerInfoArr[ID].Crit.ToString("P1"),
                                      "1"+SaveDataController.Instance.mPlayerInfoArr[ID].CritDamage.ToString("P1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].CooltimeReduce.ToString("P0"), SaveDataController.Instance.mPlayerInfoArr[ID].CCReduce.ToString("P0"));
            mItemText.text = Stat;
            mItemTitle.text = SaveDataController.Instance.mPlayerInfoArr[ID].Name;
        }
        else if (GameSetting.Instance.Language == 1)
        {//영어
            string Stat = string.Format("Max HP: {0}\n" +
                                      "Atk: {1}\n" +
                                      "Def: {2}\n" +
                                      "Movement Spd: {3}\n" +
                                      "Crit: {4}\n" +
                                      "Crit Damage: {5}\n" +
                                      "\n" +
                                      "Cooldown reduce: {6}\n" +
                                      "Resistance: {7}", SaveDataController.Instance.mPlayerInfoArr[ID].Hp.ToString("N1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].Atk.ToString("N1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].Def.ToString("N1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].Spd.ToString("N1"), SaveDataController.Instance.mPlayerInfoArr[ID].Crit.ToString("P1"),
                                      "1"+SaveDataController.Instance.mPlayerInfoArr[ID].CritDamage.ToString("P1"),
                                      SaveDataController.Instance.mPlayerInfoArr[ID].CooltimeReduce.ToString("P0"), SaveDataController.Instance.mPlayerInfoArr[ID].CCReduce.ToString("P0"));
            mItemText.text = Stat;
            mItemTitle.text = SaveDataController.Instance.mPlayerInfoArr[ID].EngName;
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
