using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour,IPointerClickHandler
{
    public Image mWindow;
    public Text mText, mTouchText;
    public bool noTouchFadeOut;

    private void Awake()
    {
        if (mTouchText!=null)
        {
            if (GameSetting.Instance.Language == 0)
            {
                mTouchText.text = "터치로 화면 닫기";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mTouchText.text = "Touch to close message";
            }
        }
    }

    public void ShowWindow(string text)
    {
        mText.text = text;
        mWindow.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (noTouchFadeOut!=true)
        {
            mWindow.gameObject.SetActive(false);
        }
    }
}
