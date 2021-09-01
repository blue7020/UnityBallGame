using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour,IPointerClickHandler
{
    public Sprite[] mSprite;
    public Image mSoundButton;

    public void SoundButtonRefresh()
    {
        if (SaveDataController.Instance.mMute == true)//mute on
        {
            mSoundButton.sprite = mSprite[1];
        }
        else
        {
            mSoundButton.sprite = mSprite[0];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundController.Instance.Mute();
        SoundButtonRefresh();
        SoundController.Instance.SESound(3);
    }
}
