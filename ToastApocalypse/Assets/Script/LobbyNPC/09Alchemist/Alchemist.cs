using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alchemist : MonoBehaviour
{
    public static Alchemist Instance;

    public Image mWindow, mPointer;
    public Text mTitleText, mGuideText, mHalloweenButtonText;
    public Button[] mButtonArr;
    public NotOpenFurniture mFurniture;
    public PopUpWindow mPopupWindow;
    public string text,ArtifactText;
    public int Price;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (SaveDataController.Instance.mUser.NPCOpen[9] == true)
            {
                mFurniture.gameObject.SetActive(false);
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mTitleText.text = "연금술";
                    mGuideText.text = "이벤트 맵에서만 출현하는 유물이 모든 맵에서 출현하게 됩니다\n가격: 3000 시럽";
                    mHalloweenButtonText.text = "할로윈 유물";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mTitleText.text = "Alchemy";
                    mGuideText.text = "Can make artifacts that only appear on the event stage, appear on all stage\nPrice: 3000 Syrup";
                    mHalloweenButtonText.text = "Halloween Artifact";
                }
                IsGetArtifact();
            }
            else
            {
                mPointer.gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IsGetArtifact()
    {
        if (SaveDataController.Instance.mUser.ArtifactOpen[0] == true)
        {
            mButtonArr[0].interactable = false;
        }
        if (SaveDataController.Instance.mUser.ArtifactOpen[1] == true)
        {
            mButtonArr[1].interactable = false;
        }
    }

    public void OpenArtifact(int stage)
    {
        if (SaveDataController.Instance.mUser.Syrup>=Price)
        {
            switch (stage)
            {
                case 7:
                    if (GameSetting.Instance.Language == 0)
                    {
                        ArtifactText = "할로윈";
                    }
                    else if (GameSetting.Instance.Language == 1)
                    {
                        ArtifactText = "Halloween";
                    }
                    SaveDataController.Instance.mUser.ArtifactOpen[0]=true;
                    IsGetArtifact();
                    break;
                case 8:
                    if (GameSetting.Instance.Language == 0)
                    {
                        ArtifactText = "크리스마스";
                    }
                    else if (GameSetting.Instance.Language == 1)
                    {
                        ArtifactText = "Christmas";
                    }
                    SaveDataController.Instance.mUser.ArtifactOpen[1] = true;
                    IsGetArtifact();
                    break;
                default:
                    break;
            }
            SaveDataController.Instance.mUser.Syrup -= Price;
            MainLobbyUIController.Instance.ShowSyrupText();
            if (GameSetting.Instance.Language == 0)
            {
                text = ArtifactText+ " 유물이 개방되었습니다!";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                text = ArtifactText +" artifact is open!";
            }
            SaveDataController.Instance.Save();
        }
        else
        {
            if (GameSetting.Instance.Language == 0)
            {
                text = "시럽이 부족합니다!";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                text = "Not enough syrup!";
            }
        }
        mPopupWindow.ShowWindow(text);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
