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

            if (Stage==7)
            {
                if (GameSetting.Instance.Language == 0)
                {
                    mText = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: 호박밭 스테이지";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mText = "This function is not open yet.\nRequirements: Pumpkin Field stage";
                }
            }
            else
            {
                if (GameSetting.Instance.Language == 0)
                {
                    mText = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: " + Stage + "스테이지 5층에서 구출";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mText = "This function is not open yet.\nRequirements: Rescue 5F in " + Stage + "stage";
                }
            }
        }
        else
        {
            if (GameSetting.Instance.Language == 0)
            {
                mText = "이 기능은 아직 개방되지 않았습니다.\n\n개방 조건: 모든 스테이지의 3층에서 확률적으로 등장";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mText = "This function is not open yet.\nRequirements: Occasionally discover in 3F in any stage";
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
