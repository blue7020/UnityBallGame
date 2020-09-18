using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNPC : MonoBehaviour
{
    public Sprite mFace;
    public GameObject mJail;
    public bool mRescue;
    public string mMessage;

    private void Awake()
    {
        mRescue = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mRescue == false)
            {
                if (GameSetting.Instance.Language==0)//한국어
                {
                    mMessage = "대충 구출했다는 내용";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mMessage = "대충 구출했다는 내용";
                }
                mJail.SetActive(false);
                //메시지 띄우기
            }
            else
            {
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mMessage = "대충 고맙다는 내용";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mMessage = "대충 고맙다는 내용";
                }
                //메시지 띄우기
            }
        }
    }
}
