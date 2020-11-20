using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalScholar : MonoBehaviour
{
    public static PortalScholar Instance;

    public Image mPointer,mWindow;
    public Text mTitleText, mGuideText, mHalloweenText, mChristmasText;
    public Button[] mPortalButtonArr;
    public StageController[] mPortalArr;
    public bool[] EventPortalOpenCheckArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            
            EventPortalOpenCheckArr = SaveDataController.Instance.mUser.EventPortalOpenCheckArr;
            if (SaveDataController.Instance.mUser.NPCOpen[7] == true)
            {
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mTitleText.text = "포탈 교체";
                    mGuideText.text = "시럽을 소모해 현재 이벤트 스테이지 포탈을 교체할 수 있습니다\n가격: 2000시럽";
                    mHalloweenText.text = GameSetting.Instance.mMapInfoArr[7].Title;
                    mChristmasText.text = GameSetting.Instance.mMapInfoArr[8].Title;
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mTitleText.text = "Portal Change";
                    mGuideText.text = "Use syrup to change the current event stage portal\nPrice: 2000 Syrup";
                    mHalloweenText.text = GameSetting.Instance.mMapInfoArr[7].EngTitle;
                    mChristmasText.text = GameSetting.Instance.mMapInfoArr[8].EngTitle;
                }
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

    public void PortalChange(int i)
    {
        if (SaveDataController.Instance.mUser.Syrup >= 2000)
        {
            SaveDataController.Instance.mUser.Syrup -= 2000;
            switch (i)
            {
                case 0:
                    mPortalArr[0].gameObject.SetActive(true);
                    EventPortalOpenCheckArr[0] = true;
                    mPortalArr[1].gameObject.SetActive(false);
                    EventPortalOpenCheckArr[1] = false;
                    SaveDataController.Instance.mUser.NowEventMapID = 1;
                    break;
                case 1:
                    mPortalArr[1].gameObject.SetActive(true);
                    EventPortalOpenCheckArr[1] = true;
                    mPortalArr[0].gameObject.SetActive(false);
                    EventPortalOpenCheckArr[0] = true;
                    SaveDataController.Instance.mUser.NowEventMapID = 2;
                    break;
            }
            ButtonRefresh();
            MainLobbyUIController.Instance.ShowSyrupText();
            MainLobbyUIController.Instance.PortalTextRefresh();
            SaveDataController.Instance.Save();
        }
    }

    public void ButtonRefresh()
    {
        if (EventPortalOpenCheckArr[0]==true)
        {
            mPortalButtonArr[0].interactable = false;
            mPortalButtonArr[1].interactable = true;
        }
        else
        {
            mPortalButtonArr[0].interactable = true;
            mPortalButtonArr[1].interactable = false;
        }
        if (EventPortalOpenCheckArr[1] == true)
        {
            mPortalButtonArr[1].interactable = false;
            mPortalButtonArr[0].interactable = true;
        }
        else
        {
            mPortalButtonArr[1].interactable = true;
            mPortalButtonArr[0].interactable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ButtonRefresh();
            mWindow.gameObject.SetActive(true);
        }
    }
}
