using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialDialog : InformationLoader,IPointerClickHandler
{
    public static TutorialDialog Instance;

    public int NowDialogID;
    public bool MessageDelay;
    public Image mWindow, mNPCImage;
    public Text mText, mGuideText, GoalText;
    public Sprite[] mNPCFace;

    public DialogText[] mInfoArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.DIALOG_TEXT);
            NowDialogID = 0;
            MessageDelay = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowDialog()
    {
        mWindow.gameObject.SetActive(true);
        mNPCImage.sprite = mNPCFace[0];
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mText.text = mInfoArr[NowDialogID].ContensFormat;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mText.text = mInfoArr[NowDialogID].EngContensFormat;
        }
    }

    public void GuideText()
    {
        if (GameSetting.Instance.Language==0)
        {
            mGuideText.text = "터치로 계속";
        }
        else if (GameSetting.Instance.Language==1)
        {
            mGuideText.text = "Touch to continue";
        }
        mGuideText.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MessageDelay == false)
        {
            Time.timeScale = 0;
            GoalText.gameObject.SetActive(false);
            StartCoroutine(Delay());
            ShowDialog();
            if (mInfoArr[NowDialogID].IsClose == true)
            {
                switch (NowDialogID)
                {
                    case 2://id는 수정해야함
                        if (GameSetting.Instance.Language == 0)//한국어
                        {
                            GoalText.text = "녹색 타일로 이동하세요";
                        }
                        else if (GameSetting.Instance.Language == 1)//영어
                        {
                            GoalText.text = "Move to green tile";
                        }
                        break;
                    case 4://id는 수정해야함
                        if (GameSetting.Instance.Language == 0)//한국어
                        {
                            GoalText.text = "ㅇㅇㅇ1 타일로 이동하세요";
                        }
                        else if (GameSetting.Instance.Language == 1)//영어
                        {
                            GoalText.text = "ㅁㄴㅇㅁㄴㅇ to green tile";
                        }
                        break;
                }
                GoalText.gameObject.SetActive(true);
                mGuideText.gameObject.SetActive(false);
                mWindow.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            NowDialogID++;
        }
    }
    public IEnumerator Delay()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.1f);
        MessageDelay = true;
        yield return delay;
        GuideText();
        MessageDelay = false;
    }
}
