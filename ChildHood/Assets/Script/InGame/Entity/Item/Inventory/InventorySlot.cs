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
    }

    public void SetSprite(Sprite image)
    {
        mItemImage.sprite = image;
    }
}
