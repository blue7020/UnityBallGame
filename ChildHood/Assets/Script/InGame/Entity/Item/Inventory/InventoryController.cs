using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private const int SLOT_COUNT = 16;
    public static InventoryController Instance;
    private List<ArtifactData> mArtifactInfoList;

    [SerializeField]
    private ArtifactController mArtifactController;
    [SerializeField]
    private InventorySlot mSlotPrefab;
    [SerializeField]
    private Transform mSlotParents;


    private InventorySlot[] mSlotArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mArtifactInfoList = new List<ArtifactData>();
            mSlotArr = new InventorySlot[SLOT_COUNT];
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                mSlotArr[i] = Instantiate(mSlotPrefab, mSlotParents);
                mSlotArr[i].Init(i, null);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            mArtifactInfoList.Add(mArtifactController.GetItem(i));
            mSlotArr[i].SetSprite(mArtifactController.GetItemSprite(i));
        }
    }

    public void AddItem(ArtifactData data)
    {
        mArtifactInfoList.Add(data);
    }
}
