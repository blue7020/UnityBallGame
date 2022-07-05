using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterAnimation : MonoBehaviour
{
    public GameObject mWindow;
    public PopUpWindow mPopUpWindow;
    public string text;

    public void Awake()
    {
        if (GameSetting.Instance.Language == 0)//한국어
        {
            text = "[전설의 토스터]로 인해\n6스테이지("+GameSetting.Instance.mMapInfoArr[6].Title+")의\n'눅눅함의 저주'가 해제되었습니다!";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            text = "Stage 6("+ GameSetting.Instance.mMapInfoArr[6].EngTitle + ")'s\n'Curse of sogginess' has been\nlifted by the [Legendary toaster]!";
        }
    }

    public void SoundOn()
    {
        SoundController.Instance.SESoundUI(6);
    }

    public void AnimEnd()
    {
        SaveDataController.Instance.mUser.FirstGameClearEvent = true;
        ToasterPedestal.Instance.ShowToasterImage();
        SoundOn();
        mWindow.gameObject.SetActive(false);
        mPopUpWindow.ShowWindow(text);
    }
}
