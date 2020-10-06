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
        Statue_Slot = GameSetting.Instance.StatueOpen.Length;
        for (int i = 0; i < Statue_Slot; i++)
        {
            StatueSlot mSlot = Instantiate(ShopSlot, mShopParents);
            mSlot.SetData(i);

        }
    }

    public void BuyStatue()
    {
        if (GameSetting.Instance.Syrup >= mStatueText.Price)
        {
            SoundController.Instance.SESoundUI(3);
            GameSetting.Instance.Syrup -= mStatueText.Price;
            GameSetting.Instance.StatueOpen[mStatue.ID] = true;
            MainLobbyUIController.Instance.ShowSyrupText();
            mBuyButton.interactable = false;
        }
    }

    public void ShowStatueInfo(StatueStat stat, StatueText lore)
    {
        mStatue = stat;
        mStatueText = lore;
        mBuyButton.interactable = true;
        if (GameSetting.Instance.StatueOpen[mStatue.ID] == true)
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                mBuyText.text = "연구 완료";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mBuyText.text = "Complete";
            }
            mBuyButton.interactable = false;
        }
        else
        {
            mBuyButton.interactable = true;
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mBuyText.text = "연구하기";
            mStatueName.text = mStatueText.Name;
            mStatueLore.text = mStatueText.ContensFormat + "\n가격:" + mStatueText.Price.ToString()+" 시럽";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mBuyText.text = "Research";
            mStatueName.text = mStatueText.EngName;
            mStatueLore.text = mStatueText.EngContensFormat + "\nPrice: " + mStatueText.Price.ToString() + " Syrup";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MainLobbyUIController.Instance.GamePause();
            mScreen.gameObject.SetActive(true);
        }
    }
}
