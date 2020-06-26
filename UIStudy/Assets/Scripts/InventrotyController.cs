using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventrotyController : MonoBehaviour
{
    private const int SLOT_COUNT = 35;
    public static InventrotyController Instance;
    private List<ItemData> mItemInfoList;

#pragma warning disable 0849
    [SerializeField]
    private ItemController mItemController;
    [SerializeField]
    private InventorySlot mSlotPrefab;
    [SerializeField]
    private Transform mSlotParents;

    [SerializeField]
    private UnityEngine.UI.Image mDragTarget;

    [SerializeField]
    private EquipmentSlot[] mEquipSlotArr;
#pragma warning restore 8649
    private int mDraggingID, mEquipSlotID;
    private InventorySlot[] mSlotArr;

    public void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
            mItemInfoList = new List<ItemData>();
            mSlotArr = new InventorySlot[SLOT_COUNT];
            for (int i =0; i<SLOT_COUNT;i++)
            {
                mSlotArr[i] = Instantiate(mSlotPrefab, mSlotParents);
                mSlotArr[i].Init(i, null,mDragTarget);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddItem(ItemData data)
    {
        mItemInfoList.Add(data);
    }

    public void SetEquipSlotID(int id)
    {
        mEquipSlotID = id;
    }

    public bool StartDragging(int id)
    {
        Debug.Log("Start Dragging " +id);
        mDraggingID = id;
        return id < mItemInfoList.Count;
    }

    public void EndDragging()
    {
        if (mDraggingID >= 0 && mEquipSlotID >= 0)
        {
            ItemData item = mItemInfoList[mDraggingID];
            EquipmentSlot equipSlot = mEquipSlotArr[mEquipSlotID];
            if (item.ItemType == equipSlot.GetEquipType())
            {
                //레벨제한같은 설정도 여기서 if문으로 해주면 됨
                equipSlot.SetSprite(mItemController.GetItemSprite(item.ID));
            }
        }
    }

    private void Start()
    {
        mDraggingID = -1;
        mEquipSlotID = -1;
        for (int i = 0; i < 31; i++)
        {
            mItemInfoList.Add(mItemController.GetItem(i));
            mSlotArr[i].SetSprite(mItemController.GetItemSprite(i));
        }
        for (int i=0; i<mEquipSlotArr.Length; i++)
        {
            mEquipSlotArr[i].Init(i);
        }
    }

}
