using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    private const int SLOT_COUNT = 50;

    private List<ItemData> mItemInfoList;
    private List<SkillData> mSkillInfoList;

#pragma warning disable 0849
    [SerializeField]
    private ItemDataController mItemController;
    [SerializeField]
    private InventorySlot mSlotPrefab;
    [SerializeField]
    private Transform mSlotParents;

    [SerializeField]
    private SkillDataController mSkillController;
    [SerializeField]
    private SkillSlot mSkillSlotPrefab;
    [SerializeField]
    private Transform mSkillSlotParents;

    [SerializeField]
    private UnityEngine.UI.Image mDragTarget;

#pragma warning restore 8649
    private int mDraggingID, mEquipSlotID;
    [SerializeField]
    private EquipSlot[] mEquipSlotArr;
    private InventorySlot[] mSlotArr;
    [SerializeField]
    private SkillSlot[] mSkillArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mItemInfoList = new List<ItemData>();
            mSkillInfoList = new List<SkillData>();
            mSlotArr = new InventorySlot[SLOT_COUNT];
            mSkillArr = new SkillSlot[8];
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                mSlotArr[i] = Instantiate(mSlotPrefab, mSlotParents);
                mSlotArr[i].Init(i, UIController.Instance.nullImage, mDragTarget);
            }
            for(int i = 0; i < mSkillArr.Length; i++)
            {
                mSkillArr[i] = Instantiate(mSkillSlotPrefab, mSkillSlotParents);
                mSkillArr[i].Init(i, UIController.Instance.nullImage);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mDraggingID = -1;
        mEquipSlotID = -1;
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (i>=mItemController.mInfoArr.Length)
            {
                mSlotArr[i].SetSprite(UIController.Instance.nullImage);
            }
            else
            {
                mItemInfoList.Add(mItemController.GetItemData(i));
                mSlotArr[i].SetSprite(mItemController.GetItemSprite(i));
            }
        }
        for (int i = 0; i < mEquipSlotArr.Length; i++)
        {
            mEquipSlotArr[i].Init(i);
        }
        for (int i = 0; i < 8; i++)
        {
            if (i >= mSkillController.mInfoArr.Length)
            {
                mSkillArr[i].SetSprite(UIController.Instance.nullImage);
            }
            else
            {
                mSkillInfoList.Add(mSkillController.GetSkillData(i));
                mSkillArr[i].SetSprite(mSkillController.GetSprite(i));
                mSkillArr[i].mSkill = SkillDataController.Instance.mInfoArr[i];
            }

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
        Debug.Log("Start Dragging " + id);
        mDraggingID = id;
        return id < mItemInfoList.Count;
    }

    public void EndDragging()
    {
        if (mDraggingID >= 0 && mEquipSlotID >= 0)
        {
            ItemData item = mItemInfoList[mDraggingID];
            EquipSlot equipSlot = mEquipSlotArr[mEquipSlotID];
            if (item.ItemType == equipSlot.GetEquipType())
            {
                //레벨제한같은 설정도 여기서 if문으로 해주면 됨
                equipSlot.SetSprite(mItemController.GetItemSprite(item.ID));
            }
        }
    }


}
