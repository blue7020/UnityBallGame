using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterAnimation : MonoBehaviour
{
    public GameObject mWindow;

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
    }
}
