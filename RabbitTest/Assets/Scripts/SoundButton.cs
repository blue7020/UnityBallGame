using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour,IPointerClickHandler
{
    public Sprite[] mSprite;
    public Image mSoundButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SaveDataController.Instance.mUser.Mute==true)//mute on
        {
            mSoundButton.sprite = mSprite[0];
            SaveDataController.Instance.mUser.Mute = false;
        }
        else
        {
            mSoundButton.sprite = mSprite[1];
            SaveDataController.Instance.mUser.Mute = true;
        }
        SoundController.Instance.SESound(3);
    }
}
