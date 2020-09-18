using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialDialog : MonoBehaviour,IPointerClickHandler
{
    public static TutorialDialog Instance;

    public int NowDialogID;
    public bool MessageDelay,IsClose;
    public Image mWindow, mNPCImage;
    public Text mText;
    public Sprite[] mNPCFace;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            NowDialogID = 0;
            MessageDelay = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowDialog(int id)
    {
        mWindow.gameObject.SetActive(true);
        switch (id)
        {
            case 0:
                mNPCImage.sprite = mNPCFace[0];
                if (GameSetting.Instance.Language==0)//한국어
                {
                    mText.text = "힘세고 강한 아침";
                }
                else if(GameSetting.Instance.Language == 1)//영어
                {
                    mText.text = "힘세고 강한 아침";
                }
                break;
            case 1:
                mNPCImage.sprite = mNPCFace[1];
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mText.text = "만일 내 이름을 묻는다면 나는 왈도";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mText.text = "만일 내 이름을 묻는다면 나는 왈도2";
                }
                IsClose = true;
                break;
            default:
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mText.text = "대사를 다 썼어";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mText.text = "대사를 다 썼어";
                }
                break;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (MessageDelay==false)
        {
            if (!IsClose)
            {
                StartCoroutine(Delay());
                ShowDialog(NowDialogID);
                NowDialogID++;
            }
            else
            {
                mWindow.gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator Delay()
    {
        WaitForSeconds delay = new WaitForSeconds(0.7f);
        MessageDelay = true;
        yield return delay;
        MessageDelay = false;
    }
}
