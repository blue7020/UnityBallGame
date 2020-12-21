using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Safe : MonoBehaviour
{
    public static Safe Instance;

    public Image mWindow;
    public Text mGoldText, mTitleText, mUpgradeText, mTooltip;
    public Button mUpgradeButton;
    public int[] mPlusGold,mUpgradePrice;
    public int ButtonChecker;

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
        RefreshText();
    }

    public void RefreshText()
    {
        ButtonChecker = SaveDataController.Instance.mUser.SafeGold;
        if (GameSetting.Instance.Language == 0)
        {
            mGoldText.text = "현재 추가 골드: " + mPlusGold[SaveDataController.Instance.mUser.SafeGold];
            mTitleText.text = "금고";
            mUpgradeText.text = "강화";
            mTooltip.text = "현재 강화 레벨에 따라 스테이지 시작 시 보유한 골드가 늘어납니다." +
                "\n\n강화 시 비용: " + mUpgradePrice[SaveDataController.Instance.mUser.SafeGold] + "시럽";
        }
        else
        {
            mGoldText.text = "Now gold: " + mPlusGold[SaveDataController.Instance.mUser.SafeGold];
            mTitleText.text = "Safe";
            mUpgradeText.text = "Updrade";
            mTooltip.text = "The gold you have in the start of the stage will increase, Depending on the current level of upgrade." +
               "\n\nUpgrade price: " + mUpgradePrice[SaveDataController.Instance.mUser.SafeGold] + " Syrup";
        }
        if (ButtonChecker == 5)
        {
            mUpgradeButton.interactable = false;
        }
    }

    public void Upgrade()
    {
        if (SaveDataController.Instance.mUser.SafeGold<=5)
        {
            if (SaveDataController.Instance.mUser.Syrup>= mUpgradePrice[SaveDataController.Instance.mUser.SafeGold])
            {
                SoundController.Instance.SESoundUI(3);
                SaveDataController.Instance.mUser.Syrup -= mUpgradePrice[SaveDataController.Instance.mUser.SafeGold];
                SaveDataController.Instance.mUser.SafeGold += 1;
                MainLobbyUIController.Instance.ShowSyrupText();
                RefreshText();
                SaveDataController.Instance.Save();
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
