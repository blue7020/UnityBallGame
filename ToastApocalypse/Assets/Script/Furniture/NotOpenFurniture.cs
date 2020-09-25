using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotOpenFurniture : MonoBehaviour
{
    public string mText;
    public int Stage;
    private void Awake()
    {
        if (GameSetting.Instance.Language==0)
        {
            mText = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: "+Stage+"클리어";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mText = "This function is not open yet.\nRequirements: " + Stage + "Clear";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PopUpWindow.Instance.ShowWindow(mText);
        }
    }
}
