using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlackSmithShopController : MonoBehaviour
{
    public static BlackSmithShopController Instance;

    public Button mBuyButton;
    public Text mBuyText, mBuyImageText;
    public WeaponSelectSlot mSelectSlot;
    public MaterialSlot[] mMaterialSlot;
    public Image mBuyImage;

    public Weapon mWeapon;
    public WeaponStat mWeaponStat;

    public WeaponShopSlot ShopSlot;
    public Transform mShopParents;

    public bool[] RecipeCheker;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            RecipeCheker = new bool[4]; if (GameSetting.Instance.Language == 0)//한국어
            {
                mBuyText.text = "제작";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mBuyText.text = "Craft";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < SaveDataController.Instance.mWeaponInfoArr.Length; i++)
        {
            if (SaveDataController.Instance.mWeaponInfoArr[i].ShopSell == true && SaveDataController.Instance.mUser.WeaponOpen[i] == true)
            {
                WeaponShopSlot mSlot = Instantiate(ShopSlot, mShopParents);
                mSlot.SetData(i);
            }

        }
    }

    public void BuyWeapon()
    {
        if (SaveDataController.Instance.mUser.Syrup >= mWeapon.mStats.Price)
        {
            for (int i = 0; i < mMaterialSlot.Length; i++)
            {
                if (mMaterialSlot[i] == null)
                {
                    RecipeCheker[i] = true;
                }
                else
                {
                    if (mMaterialSlot[i].mAmount <= SaveDataController.Instance.mUser.HasMaterial[mMaterialSlot[i].mMaterialID])
                    {
                        SaveDataController.Instance.mUser.HasMaterial[mMaterialSlot[i].mMaterialID] -= mMaterialSlot[i].mAmount;
                        RecipeCheker[i] = true;
                    }
                }
            }
            if (RecipeCheker[0] == true && RecipeCheker[1] == true && RecipeCheker[2] == true && RecipeCheker[3] == true)
            {
                mBuyImage.gameObject.SetActive(true);
                SaveDataController.Instance.mUser.Syrup -= mWeapon.mStats.Price;
                SaveDataController.Instance.mUser.WeaponHas[mWeapon.mID] = true;
                MainLobbyUIController.Instance.ShowSyrupText();
                GameSetting.Instance.PlayerWeaponID = mWeapon.mID;
                mBuyButton.interactable = false;
                SoundController.Instance.SESoundUI(8);
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mBuyImageText.text = "무기 제작에 성공했습니다!";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mBuyImageText.text = "Succeeded weapon crafting!";
                }
            }
            else
            {
                mBuyImage.gameObject.SetActive(true);
                mBuyButton.interactable = true;
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mBuyImageText.text = "재료가 부족합니다";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mBuyImageText.text = "Not enough material";
                }
            }
        }
        else
        {
            mBuyImage.gameObject.SetActive(true);
            mBuyButton.interactable = true;
            if (GameSetting.Instance.Language == 0)//한국어
            {
                mBuyImageText.text = "시럽이 부족합니다";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                mBuyImageText.text = "Not enough syrup";
            }
        }
    }

    public void ShowWeaponInfo(Weapon weapon)
    {
        mWeapon = weapon;
        mWeaponStat = SaveDataController.Instance.mWeaponInfoArr[weapon.mID];
        mSelectSlot.SetData(mWeapon.mID,mWeapon.mWeaponImage, mWeaponStat);
        for (int i = 0; i < mMaterialSlot.Length; i++)
        {
            if (mWeapon.Recipe[i] == null)
            {
                mMaterialSlot[i].RemoveData();
            }
            else
            {
                mMaterialSlot[i].SetData(mWeapon.Recipe[i].mID);
                mMaterialSlot[i].mCount.text = mWeapon.RecipeAmount[i].ToString();
                mMaterialSlot[i].mAmount = mWeapon.RecipeAmount[i];
            }
        }
        if (SaveDataController.Instance.mUser.WeaponHas[mWeapon.mID] == true)
        {
            mBuyButton.interactable = false;
            if (GameSetting.Instance.Language == 0)//한국어
            {
                BlackSmith.Instance.mMaterialText.text = "비용: "+ mWeaponStat.Price + " 시럽 / 제작 재료";
                mBuyText.text = "보유중";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                BlackSmith.Instance.mMaterialText.text = "Price: " + mWeaponStat.Price + " Syrup / Recipe";
                mBuyText.text = "Get";
            }
        }
        else
        {
            mBuyButton.interactable = true;
            if (GameSetting.Instance.Language == 0)//한국어
            {
                BlackSmith.Instance.mMaterialText.text = "비용: " + mWeaponStat.Price + " 시럽 / 제작 재료";
                mBuyText.text = "제작";
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                BlackSmith.Instance.mMaterialText.text = "Price: " + mWeaponStat.Price + " Syrup / Recipe";
                mBuyText.text = "Craft";
            }
        }
    }
}
