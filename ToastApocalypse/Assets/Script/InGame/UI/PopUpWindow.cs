using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour,IPointerClickHandler
{
    public Image mWindow;
    public Text mText;
    public bool noTouchFadeOut;

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
