using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    private const int SLOT_COUNT = 16;
    public static InventoryController Instance;


    private List<Artifact> mArtifactInfoList;
    [SerializeField]
    private InventorySlot mSlotPrefab;
    [SerializeField]
    private Transform mSlotParents;
    [SerializeField]
    private Sprite mVoidImage;


    public InventorySlot[] mSlotArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mArtifactInfoList = new List<Artifact>();
            mSlotArr = new InventorySlot[SLOT_COUNT];
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                mSlotArr[i] = Instantiate(mSlotPrefab, mSlotParents);
                mSlotArr[i].Init(i, mVoidImage);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Artifact data)
    {
        mArtifactInfoList.Add(data);
    }
}
