using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    //UI쪽은 드래그&드랍(시리얼라이즈 필드)으로 연결해주는 게 더 안정적이며
    //Start나 Awake 전에 연결해주는 것이 오류를 예방할 수 있다.
#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Image mIcon;
#pragma warning restore 0649

    public void SetText(string text)//내용이 들어갈 수도 있으니 텍스트 자체를 넣는 것이다.
    {
        mText.text = text;
    }

    public void SetIcon(Sprite sprite)
    {
        mIcon.sprite = sprite;
    }
}
