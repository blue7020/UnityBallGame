using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialGenerator : MonoBehaviour
{
    public static MaterialGenerator Instance;

    public Image mWindow, mGeneratingWindow, mMaterialIcon;
    public Text mTitleText, mTooltip, mButtontext, mMaterialText, mMaterialNameText;
    public Button mButton;

    public int mPrice;
    public List<int> mMaterialList;

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
        mMaterialList = new List<int>();
        RefreshCount();
    }

    public void RefreshCount()
    {
        mPrice = 50 + ((5 - SaveDataController.Instance.mUser.GeneratorUseAmount) * 10);
        if (GameSetting.Instance.Language == 0)
        {
            mTitleText.text = "재료 생성기";
            mTooltip.text = "시럽을 소모해 무작위 재료를 하나 생성합니다.\n일일 사용 횟수는 매일 초기화됩니다.\n일일 사용 횟수: " + SaveDataController.Instance.mUser.GeneratorUseAmount + "번\n\n생성 가격: " + mPrice + "시럽";
            mMaterialText.text = "생성 완료!";
            mButtontext.text = "생성";
        }
        else
        {
            mTitleText.text = "Material Generator";
            mTooltip.text = "Consume syrup to create one random material.\nThe 'Daily use count' is initialized daily.\nDaily use count: " + SaveDataController.Instance.mUser.GeneratorUseAmount + "\n\nGenerated price: " + mPrice + " Syrup";
            mMaterialText.text = "Succeeded!";
            mButtontext.text = "Generate";
        }
        MaterialCheck();
        if (SaveDataController.Instance.mUser.GeneratorUseAmount < 1)
        {
            mButton.interactable = false;
        }
        else
        {
            mButton.interactable = true;
        }
    }

    public void MaterialCheck()
    {
        for (int i=0; i<GameSetting.Instance.mMaterialSpt.Length;i++)
        {
            if (SaveDataController.Instance.mUser.HasMaterial[i]+1<=99)
            {
                mMaterialList.Add(i);
            }
        }
    }

    public void Generating()
    {
        if (SaveDataController.Instance.mUser.GeneratorUseAmount > 0)
        {
            if (SaveDataController.Instance.mUser.Syrup >= mPrice)
            {
                if (mMaterialList.Count > 0)
                {
                    int rand = Random.Range(0, mMaterialList.Count);
                    int MaterialId = mMaterialList[rand];
                    SaveDataController.Instance.mUser.HasMaterial[MaterialId] += 1;
                    SaveDataController.Instance.mUser.Syrup -= mPrice;
                    SaveDataController.Instance.mUser.GeneratorUseAmount -= 1;
                    MainLobbyUIController.Instance.ShowSyrupText();
                    RefreshCount();
                    SaveDataController.Instance.Save();
                    mMaterialIcon.sprite = GameSetting.Instance.mMaterialSpt[MaterialId];
                    if (GameSetting.Instance.Language == 0)
                    {
                        mMaterialNameText.text = MaterialController.Instance.mInfoArr[MaterialId].Title+"\n현재 보유량: " + SaveDataController.Instance.mUser.HasMaterial[MaterialId]+"개";
                    }
                    else
                    {
                        mMaterialNameText.text = MaterialController.Instance.mInfoArr[MaterialId].EngTitle + "\nNow amount: " + SaveDataController.Instance.mUser.HasMaterial[MaterialId];
                    }
                    mGeneratingWindow.gameObject.SetActive(true);
                }
                else
                {
                    mButton.interactable = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RefreshCount();
        mWindow.gameObject.SetActive(true);
    }
}
