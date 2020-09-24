using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialDialog : InformationLoader,IPointerClickHandler
{
    public static TutorialDialog Instance;

    public int NowDialogID;
    public bool MessageDelay, NextMessage;
    public Image mWindow, mNPCImage;
    public Text mText, mGuideText, GoalText;
    public Sprite[] mNPCFace;

    public DialogText[] mInfoArr;
    public GameObject ClearItem;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.DIALOG_TEXT);
            Time.timeScale = 1;
            NowDialogID = 0;
            NextMessage = false;
            MessageDelay = false;
            if (GameSetting.Instance.Language == 0)
            {
                mGuideText.text = "터치로 계속";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mGuideText.text = "Touch to continue";
            }
            mGuideText.gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowDialog()
    {
        Time.timeScale = 0;
        mWindow.gameObject.SetActive(true);
        if (NowDialogID==43|| NowDialogID == 45|| NowDialogID == 46|| NowDialogID == 47|| NowDialogID == 48||NowDialogID==50|| NowDialogID == 51 || NowDialogID == 52)
        {
            mNPCImage.sprite = mNPCFace[1];
        }
        else
        {
            mNPCImage.sprite = mNPCFace[0];
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mText.text = mInfoArr[NowDialogID].ContensFormat;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mText.text = mInfoArr[NowDialogID].EngContensFormat;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MessageDelay == false)
        {
            if (NextMessage == false)
            {
                StartCoroutine(Delay());
                ShowDialog();
                if (mInfoArr[NowDialogID + 1].IsClose == true)
                {
                    switch (NowDialogID)
                    {
                        case 1:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "녹색 타일로 이동하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "Move to green tile";
                            }
                            break;
                        case 12:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "유물을 획득하고 변화한 능력치를 확인하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 18:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "줄어든 체력을 사용 아이템으로 회복하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 23:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "석상의 효과를 받고 변화한 능력치를 확인하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 29:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "무기를 장착한 후 체험해보세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 33:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "죽지 않고 허수아비를 처치하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 37:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "스킬을 사용해 가시를 안전하게 지나가세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 40:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "보스를 쓰러트리세요!";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 42:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "마법에 갇힌 시민을 구출하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            break;
                        case 49:
                            if (GameSetting.Instance.Language == 0)//한국어
                            {
                                GoalText.text = "조각을 획득하세요";
                            }
                            else if (GameSetting.Instance.Language == 1)//영어
                            {
                                GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                            }
                            ClearItem.gameObject.SetActive(true);
                            break;
                    }
                    NextMessage = true;
                }
                NowDialogID++;

            }
            else
            {
                Time.timeScale = 1;
                if (NowDialogID > 52)
                {
                    mGuideText.gameObject.SetActive(false);
                    mWindow.gameObject.SetActive(false);
                    TutorialEnd.Instance.GameClear();
                }
                else
                {
                    mGuideText.gameObject.SetActive(true);
                    mWindow.gameObject.SetActive(false);
                    NextMessage = false;
                }
            }
          
        }
          
    }
    public IEnumerator Delay()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.1f);
        MessageDelay = true;
        yield return delay;
        MessageDelay = false;
    }
}
