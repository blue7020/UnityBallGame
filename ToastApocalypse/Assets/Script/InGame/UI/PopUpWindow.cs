using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpWindow : MonoBehaviour,IPointerClickHandler
{
    public static PopUpWindow Instance;
    public Image mWindow;
    public Text mText;

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

    public void ShowWindow(string text)
    {
        mText.text = text;
        mWindow.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mWindow.gameObject.SetActive(false);
    }
}
