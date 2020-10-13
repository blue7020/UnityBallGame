using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialInventoryController : MonoBehaviour

{
    public static MaterialInventoryController Instance;

    public int MaterialCount;
    public MaterialSlot ChangeSlot;
    public MaterialSlot[] SlotArr;
    public Transform mChangeParents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < MaterialController.Instance.mInfoArr.Length; i++)
        {
            MaterialCount++;
        }
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        if (SlotArr != null)
        {
            for (int i = 0; i < SlotArr.Length; i++)
            {
                Destroy(SlotArr[i].gameObject);
            }
        }

        SlotArr = new MaterialSlot[MaterialController.Instance.mInfoArr.Length];
        for (int i = 0; i < MaterialCount; i++)
        {
            SlotArr[i] = Instantiate(ChangeSlot, mChangeParents);
            SlotArr[i].SetData(i);
            SlotArr[i].mCount.text = SaveDataController.Instance.mUser.HasMaterial[SlotArr[i].mMaterialID].ToString();

        }
    }


}
