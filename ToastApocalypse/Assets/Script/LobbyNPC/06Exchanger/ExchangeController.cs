using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeController : InformationLoader

{
    public static ExchangeController Instance;

    public int MaterialCount,mNowId;
    public MaterialSlot ChangeSlot;
    public List<MaterialSlot> SlotList;
    public Transform mChangeParents;

    public Text mTitleText, mTooltipText, mMaterialText, mSellText;

    public int mSellAmount, mTotalSyrup;

    public MaterialStat[] mInfoArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.MATERIAL_STAT);
            SlotList = new List<MaterialSlot>();
            if (GameSetting.Instance.Language == 0)
            {
                mTitleText.text = "거래소";
                mTooltipText.text = "선택한 재료를 판매합니다.";
                mMaterialText.text = "[재료명]\n판매 개수: 0 / 99\n판매 가격: 0 시럽";
                mSellText.text = "판매";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mTitleText.text = "Exchange";
                mTooltipText.text = "Sell the selected materials.";
                mMaterialText.text = "[Material name]\nAmount: 0 / 99\nPrice: 0 Syrup";
                mSellText.text = "Sell";
            }
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            MaterialCount++;
        }
        RefreshInventory();
    }

    public void ShowDescription(int id)
    {
        mNowId = id;
        if (mNowId == -1)
        {
            if (GameSetting.Instance.Language == 0)
            {
                mMaterialText.text = "[재료명]\n판매 개수: 0 / 99\n판매 가격: 0 시럽";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mMaterialText.text = "[Material name]\nAmount: 0 / 99\nPrice: 0 Syrup";
            }
        }
        else
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                mMaterialText.text = mInfoArr[mNowId].Title+"\n판매 개수: "+ mSellAmount + "/ "+
                    SaveDataController.Instance.mUser.HasMaterial[mNowId] + "\n판매 가격: "+ mTotalSyrup + " 시럽";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mMaterialText.text = mInfoArr[mNowId].EngTitle + "\nAmount: " + mSellAmount + "/ " +
                    SaveDataController.Instance.mUser.HasMaterial[mNowId] + "\nPrice: " + mTotalSyrup + " Syrup";
            }
        }
    }

    public void AmountControl(int plusAmount)//-1 or 1
    {
        if (plusAmount==-1)
        {
            if (mSellAmount>0&& SaveDataController.Instance.mUser.HasMaterial[mNowId]-1>=0)
            {
                mSellAmount--;
            }
        }
        else if (plusAmount == 1)
        {
            if (mSellAmount < SaveDataController.Instance.mUser.HasMaterial[mNowId] && SaveDataController.Instance.mUser.HasMaterial[mNowId] + 1 <= Constants.MATERIAL_COUNT)
            {
                mSellAmount++;
            }
        }
        mTotalSyrup = mSellAmount * mInfoArr[mNowId].SellAmount;
        ShowDescription(mNowId);
    }

    public void Sell()
    {
        if (mTotalSyrup > 0&& SaveDataController.Instance.mUser.Syrup>= SaveDataController.Instance.mUser.Syrup-mTotalSyrup)
        {
            SoundController.Instance.SESoundUI(3);
            SaveDataController.Instance.mUser.Syrup -= mTotalSyrup;
            SaveDataController.Instance.mUser.HasMaterial[mNowId] -= mSellAmount;
            SlotList[mNowId].mCount.text = SaveDataController.Instance.mUser.HasMaterial[SlotList[mNowId].mMaterialID].ToString();
            MainLobbyUIController.Instance.ShowSyrupText();
            mSellAmount = 0;
            mTotalSyrup = 0;
            SaveDataController.Instance.Save();
            mNowId = -1;
            ShowDescription(mNowId);
        }
    }

    public void RefreshInventory()
    {
        if (SlotList != null)
        {
            for (int i = 0; i < SlotList.Count; i++)
            {
                Destroy(SlotList[i].gameObject);
            }
        }
        SlotList = new List<MaterialSlot>();
        for (int i = 0; i < MaterialCount; i++)
        {
            SlotList.Add(Instantiate(ChangeSlot, mChangeParents));
            SlotList[i].SetData(i,true);
            SlotList[i].mCount.text = SaveDataController.Instance.mUser.HasMaterial[SlotList[i].mMaterialID].ToString();

        }
    }


}
