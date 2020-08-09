using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public Text mText;
    public Image mIcon;

    public void SetText(string text)
    {
        mText.text = text;
    }

    public void SetIcon(Sprite sprite)
    {
        mIcon.sprite = sprite;
    }
}
