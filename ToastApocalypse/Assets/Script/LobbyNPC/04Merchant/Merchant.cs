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
            mItemTitle.text = "상품명";
            mItemText.text = "준비중";
            mBuyText.text = "구매";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitle.text = "Shop";
            mItemTitle.text = "ItemName";
            mItemText.text = "Preparing";
            mBuyText.text = "Purchase";
        }
    }

    public void BuyItem()
    {
        switch (NowItemID)
        {
            case 0://광고 제거
                break;

        }
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
                mItemTitle.text = "광고 제거";
                mItemText.text = "모든 보상을 광고 없이 획득할 수 있습니다.";
                break;
            case 1://캐릭터
                mItemTitle.text = "캐릭터 1";
                mItemText.text = "능력치 표시하기";
                break;
            case 2://캐릭터
                mItemTitle.text = "캐릭터 1";
                mItemText.text = "능력치 표시하기";
                break;
            case 3://캐릭터
                mItemTitle.text = "캐릭터 1";
                mItemText.text = "능력치 표시하기";
                break;
            case 4://캐릭터
                mItemTitle.text = "캐릭터 1";
                mItemText.text = "능력치 표시하기";
                break;
            case 5://
                mItemTitle.text = "캐릭터 1";
                mItemText.text = "능력치 표시하기";
                break;


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
