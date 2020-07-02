using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    public Image mItemImage;
    private int mID;//Slot ID

    public void Init(int id, Sprite image)
    {
        mID = id;
        mItemImage.sprite = image;
        mItemImage.color = Color.white;
    }

    public void SetSprite(Sprite image)
    {
        Debug.Log(mItemImage);
        mItemImage.sprite = image;
    }
}
