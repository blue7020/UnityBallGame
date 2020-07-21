using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour,IEndDragHandler
{

    public static InventoryController Instance;

    public const int SLOT_COUNT = 50;



    public eItemType eType;
    public EquipmentSlot mEquipSlot;


    public InventorySlot[] mSlotArr;
    public int itemCount;
    public int mSlotID;
    public InventorySlot mSlot;
    public Transform SlotParent;
    public Transform Canvas;
    public Sprite mVoidspt;
    public Image mDragTarget;

    public bool mDragging;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        itemCount = 0;
        mSlotArr = new InventorySlot[SLOT_COUNT];
        for (int i=0; i< mSlotArr.Length; i++)
        {
            mSlotArr[i]= Instantiate(mSlot,SlotParent);
            mSlotArr[i].Init(i,mVoidspt);
        }
    }

    private void Start()
    {
        UIController.Instance.mSlotText.text = "Slot: 0 /" + mSlotArr.Length;
    }

    public void GetItem()
    {
        if (itemCount<=30)
        {
            mSlotArr[itemCount].SetItem(itemCount);
            itemCount++;
            UIController.Instance.mSlotText.text = "Slot: " + itemCount + "/" + mSlotArr.Length;
        }
        else
        {
            string text = "아이템이 없습니다!";
            UIController.Instance.ShowPopup(text);
        }
        
    }

    public bool StartDragging(int id)
    {
        return id < itemCount;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
