using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArtSlot : MonoBehaviour, IPointerClickHandler
{
    public int mID;
    public ArtText mArt;
    public Image Icon;

    public void SetData(int id,int code)
    {
        mID = id;
        mArt = GameSetting.Instance.mArtInfoArr[mID];
        Icon.sprite = ArtistController.Instance.mIcon[code];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ArtistWindowController.Instance.SelectArt(mArt);
    }
}
