using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCodeController : MonoBehaviour
{
    public static SerialCodeController Instance;

    public Text mTitleText, mGuideText, mCodeText, mRewardTitle, mRewardText, ButtonText;
    public InputField mCodeBox;
    public Image mRewardWindow;
    public RewardWindow mRewardImageWindow;
    public List<RewardWindow> SlotList;
    public Transform Parents;
    public Sprite[] mSpt;
    public int index;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            index = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetText();
    }

    public void DestroyInventory()
    {
        if (SlotList!=null)
        {
            for (int i = 0; i < SlotList.Count; i++)
            {
                Destroy(SlotList[i].gameObject);
            }
        }
        index = 0;
    }

    public void ResetText()
    {
        DestroyInventory();
        mCodeBox.text = "";
        mCodeText.text = "";
        if (GameSetting.Instance.Language == 0)
        {
            mTitleText.text = "코드 사용";
            mGuideText.text = "아래의 슬롯을 눌러 코드를 입력하세요";
            ButtonText.text = "입력\n완료";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitleText.text = "Using Code";
            mGuideText.text = "Press the slot below to input the code";
            ButtonText.text = "Done";
        }
    }

    public void CodeInput(Text code)
    {
        mCodeBox.text = code.text;
    }

    public void CodeCheck()
    {
        DestroyInventory();
        bool Check = false;
        mCodeText.text = mCodeBox.text;
        for (int i = 0; i < SaveDataController.Instance.mCodeInfoArr.Length; i++)
        {
            if (SaveDataController.Instance.mCodeInfoArr[i].Code == mCodeText.text)
            {
                if (SaveDataController.Instance.mUser.CodeUse[i] == false && SaveDataController.Instance.mCodeInfoArr[i].IsExpiration == false)
                {
                    if (GameSetting.Instance.Language == 0)
                    {
                        mRewardTitle.text = "보상 획득!";
                    }
                    else if (GameSetting.Instance.Language == 1)
                    {
                        mRewardTitle.text = "Get Reward!";
                    }
                    SlotList = new List<RewardWindow>();
                    RewardInstantiate(i);
                    SaveDataController.Instance.mUser.CodeUse[i] = true;
                    SaveDataController.Instance.Save();
                    Check = true;
                    break;
                }
            }
        }
        if (Check == false)
        {
            if (GameSetting.Instance.Language == 0)
            {
                mRewardTitle.text = "실패";
                mGuideText.text = "사용 가능한 코드가 아닙니다!";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mRewardTitle.text = "Failed";
                mGuideText.text = "This code is not available!";
            }
        }
        mRewardWindow.gameObject.SetActive(true);
    }

    public void RewardInstantiate(int ID)
    {
        if (SaveDataController.Instance.mCodeInfoArr[ID].CharacterID >= 0)
        {
            SlotList.Add(Instantiate(mRewardImageWindow, Parents));
            SlotList[index].mIcon.sprite = GameSetting.Instance.mPlayerSpt[SaveDataController.Instance.mCodeInfoArr[ID].CharacterID];
            SlotList[index].mAmountText.text = "1";
            SaveDataController.Instance.mUser.CharacterHas[SaveDataController.Instance.mCodeInfoArr[ID].CharacterID] = true;
            SaveDataController.Instance.mUser.CharacterOpen[SaveDataController.Instance.mCodeInfoArr[ID].CharacterID] = true;
            index++;
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].SkillID >= 0)
        {
            SlotList.Add(Instantiate(mRewardImageWindow, Parents));
            SlotList[index].mIcon.sprite = SkillController.Instance.SkillIcon[SaveDataController.Instance.mCodeInfoArr[ID].SkillID];
            SaveDataController.Instance.mUser.SkillHas[SaveDataController.Instance.mCodeInfoArr[ID].SkillID] = true;
            SlotList[index].mAmountText.text = "1";
            index++;
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].WeaponID >= 0)
        {
            SlotList.Add(Instantiate(mRewardImageWindow, Parents));
            SlotList[index].mIcon.sprite = GameSetting.Instance.mWeaponArr[SaveDataController.Instance.mCodeInfoArr[ID].WeaponID].mRenderer.sprite;
            SaveDataController.Instance.mUser.WeaponHas[SaveDataController.Instance.mCodeInfoArr[ID].WeaponID] = true;
            SlotList[index].mAmountText.text = "1";
            index++;
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].ItemID >= 0)
        {
            SlotList.Add(Instantiate(mRewardImageWindow, Parents));
            SlotList[index].mIcon.sprite = GameSetting.Instance.mItemArr[SaveDataController.Instance.mCodeInfoArr[ID].ItemID].mRenderer.sprite;
            SaveDataController.Instance.mUser.ItemHas[SaveDataController.Instance.mCodeInfoArr[ID].ItemID] = true;
            SlotList[index].mAmountText.text = "1";
            index++;
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].SyrupAmount > 0)
        {
            SlotList.Add(Instantiate(mRewardImageWindow, Parents));
            SlotList[index].mIcon.sprite = mSpt[0];
            SlotList[index].mAmountText.text = SaveDataController.Instance.mCodeInfoArr[ID].SyrupAmount.ToString();
            GameSetting.Instance.GetSyrup(SaveDataController.Instance.mCodeInfoArr[ID].SyrupAmount);
            index++;
        }
        for (int i = 0; i < SaveDataController.Instance.mCodeInfoArr[ID].MaterialID.Length; i++)
        {
            if (SaveDataController.Instance.mCodeInfoArr[ID].MaterialID[i] >= 0 && SaveDataController.Instance.mCodeInfoArr[ID].MaterialAmount[i] > 0)
            {
                SlotList.Add(Instantiate(mRewardImageWindow, Parents));
                SlotList[index].mIcon.sprite = GameSetting.Instance.mMaterialSpt[SaveDataController.Instance.mCodeInfoArr[ID].MaterialID[i]];
                SlotList[index].mAmountText.text = SaveDataController.Instance.mCodeInfoArr[ID].MaterialAmount[i].ToString();
                SaveDataController.Instance.mUser.HasMaterial[SaveDataController.Instance.mCodeInfoArr[ID].MaterialID[i]] += SaveDataController.Instance.mCodeInfoArr[ID].MaterialAmount[i];
                index++;
            }
        }
    }
}
