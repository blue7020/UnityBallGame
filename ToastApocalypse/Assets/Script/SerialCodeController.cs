using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCodeController : InformationLoader
{
    public static SerialCodeController Instance;

    public Text mTitleText, mGuideText,mCodeText, mRewardTitle,mRewardText;
    public InputField mCodeBox;
    public Image mRewardWindow;
    public RewardWindow mRewardImageWindow;
    public Transform Parents;
    //code json불러오기

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
        ResetText();
        if (GameSetting.Instance.Language == 0)
        {
            mTitleText.text = "코드 사용";
            mGuideText.text = "아래의 슬롯을 눌러 코드를 입력하세요";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitleText.text = "Using Code";
            mGuideText.text = "Press the slot below to input the code";
        }
    }

    public void ResetText()
    {
        mCodeBox.text = "";
        mCodeText.text = "";
    }

    public void CodeInput(InputField code)
    {
        if (true)
        {
            if (GameSetting.Instance.Language == 0)
            {
                mTitleText.text = "보상 획득!";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mTitleText.text = "Get Reward!";
            }
            mGuideText.text = "";
            //보상 개수에 따라 이미지 인스턴시에이트 하기
            for (int i=0; i<SaveDataController.Instance.mUser.CodeUse.Length;i++)
            {
                if (SaveDataController.Instance.mCodeInfoArr[i].Code==code.text&& SaveDataController.Instance.mCodeInfoArr[i].IsExpiration==false)
                {
                    if (SaveDataController.Instance.mCodeInfoArr[i].IsUse==false)
                    {
                        RewardInstantiate(i);
                        SaveDataController.Instance.mUser.CodeUse[i] = true;
                        break;
                    }
                }
            }
            SaveDataController.Instance.Save();
            mRewardImageWindow.gameObject.SetActive(true);
        }
        else
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
            mRewardImageWindow.gameObject.SetActive(false);
        }
        mRewardWindow.gameObject.SetActive(true);
        ResetText();
    }

    public void RewardInstantiate(int ID)
    {
        if (SaveDataController.Instance.mCodeInfoArr[ID].CharacterID>=0)
        {
            RewardWindow image = Instantiate(mRewardImageWindow, Parents);
            //image.sprite=
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].SkillID >= 0)
        {
            RewardWindow image = Instantiate(mRewardImageWindow, Parents);
            //image.sprite=
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].WeaponID >= 0)
        {
            RewardWindow image = Instantiate(mRewardImageWindow, Parents);
            //image.sprite=
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].ItemID >= 0)
        {
            RewardWindow image = Instantiate(mRewardImageWindow, Parents);
            //image.sprite=
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].MaterialID >= 0&& SaveDataController.Instance.mCodeInfoArr[ID].MaterialAmount >= 0)
        {
            RewardWindow image = Instantiate(mRewardImageWindow, Parents);
            //image.sprite=
                image.mAmountText.text = SaveDataController.Instance.mCodeInfoArr[ID].MaterialAmount.ToString();
                SaveDataController.Instance.mUser.HasMaterial[SaveDataController.Instance.mCodeInfoArr[ID].MaterialID] += SaveDataController.Instance.mCodeInfoArr[ID].MaterialAmount;
        }
        if (SaveDataController.Instance.mCodeInfoArr[ID].SyrupAmount > 0)
        {
            RewardWindow image = Instantiate(mRewardImageWindow, Parents);
            //image.sprite=
            image.mAmountText.text = SaveDataController.Instance.mCodeInfoArr[ID].SyrupAmount.ToString();
            SaveDataController.Instance.mUser.Syrup += SaveDataController.Instance.mCodeInfoArr[ID].SyrupAmount;
        }
    }
}
