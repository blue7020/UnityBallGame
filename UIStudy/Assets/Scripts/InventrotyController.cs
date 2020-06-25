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
    private Image mDragTarget;
#pragma warning restore 8649

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

    public bool StartDragging(int id)
    {
        Debug.Log("Start Dragging " +id);
        return id < mItemInfoList.Count;
    }

    private void Start()
    {
        for (int i=0; i<31;i++)
        {
            mItemInfoList.Add(mItemController.GetItem(i));
            mSlotArr[i].SetSprite(mItemController.GetItemSprite(i));
        }
    }

    public void AddItem(ItemData data)
    {
        mItemInfoList.Add(data);
    }

}
