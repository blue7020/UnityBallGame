using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotOpenFurniture : MonoBehaviour
{
    public string mText;
    public int Stage;
    public PopUpWindow mWindow;
    private void Awake()
    {
        if (Stage>0)
        {
            if (GameSetting.Instance.Language == 0)
            {
                mText = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: " + Stage + "스테이지 클리어";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mText = "This function is not open yet.\nRequirements: " + Stage + "stage clear";
            }
        }
        else
        {
            if (GameSetting.Instance.Language == 0)
            {
                mText = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: 모든 스테이지 내에서 확률적으로 등장";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mText = "This function is not open yet.\nRequirements: Occasionally discover in any stage";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.ShowWindow(mText);
        }
    }
}
