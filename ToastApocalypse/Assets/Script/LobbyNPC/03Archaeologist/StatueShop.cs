using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatueShop : MonoBehaviour
{
    public static StatueShop Instance;

    public int Statue_Slot;

    public StatueSlot ShopSlot;
    public Transform mShopParents;

    public StatueStat mStatue;
    public StatueText mStatueText;
    public NotOpenFurniture mFurniture;

    public Image mScreen;
    public Button mBuyButton;
    public Text mTitle, mStatueName, mStatueLore, mBuyText;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SaveDataController.Instance.mUser.NPCOpen[3]==true)
        {
            mFurniture.gameObject.SetActive(false);
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mTitle.text = "석상 연구소";
            mStatueName.text = "석상 연구";
            mStatueLore.text = "석상을 연구하여 스테이지에서 출현하는 석상의 종류를 늘립니다";
            mBuyText.text = "연구하기";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mTitle.text = "Statue Lab";
            mStatueName.text = "Statue research";
            mStatueLore.text = "Researching statue increases the type of statues that are appearing in the stage";
            mBuyText.text = "Research";
        }
        Statue_Slot = SaveDataController.Instance.mStatueInfoArr.Length;
        for (int i = 0; i < Statue_Slot; i++)
        {
            StatueSlot mSlot = Instantiate(ShopSlot, mShopParents);
            mSlot.SetData(i);

        }
    }

    public void BuyStatue()
    {
        if (SaveDataController.Instance.mUser.Syrup >= mStatueText.Price)
        {
            SoundController.Instance.SESoundUI(3);
            SaveDataController.Instance.mUser.Syrup -= mStatueText.Price;
            SaveDataController.Instance.mUser.StatueHas[mStatue.ID] = true;
            MainLobbyUIController.Instance.ShowSyrupText();
            ShowStatueInfo(mStatue, mStatueText);
            SaveDataController.Instance.Save();
        }
    }

    public void ShowStatueInfo(StatueStat stat, StatueText lore)
    {
        mStatue = stat;
        mStatueText = lore;
        mBuyButton.interactable = true;
        if (SaveDataController.Instance.mUser.StatueHas[mStatue.ID] == true)
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                mBuyText.text = "연구 완료";
                mStatueName.text = mStatueText.Name;
                mStatueLore.text = mStatueText.ContensFormat + "\n가격:" + mStatueText.Price.ToString() + " 시럽";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mBuyText.text = "Complete";
                mStatueName.text = mStatueText.EngName;
                mStatueLore.text = mStatueText.EngContensFormat + "\nPrice: " + mStatueText.Price.ToString() + " Syrup";
            }
            mBuyButton.interactable = false;
        }
        else
        {
            mBuyButton.interactable = true;
            if (GameSetting.Instance.Language == 0)//한국어
            {
                mBuyText.text = "연구하기";
                mStatueName.text = mStatueText.Name;
                mStatueLore.text = mStatueText.ContensFormat + "\n가격:" + mStatueText.Price.ToString() + " 시럽";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mBuyText.text = "Research";
                mStatueName.text = mStatueText.EngName;
                mStatueLore.text = mStatueText.EngContensFormat + "\nPrice: " + mStatueText.Price.ToString() + " Syrup";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mScreen.gameObject.SetActive(true);
        }
    }
}
