using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectController : MonoBehaviour
{
    public static WeaponSelectController Instance;

    public WeaponSelectSlot mSelectSlot;
    public Transform Canvas;
    public Image DragTarget;

    public int WeaponCount;
    public WeaponChangeSlot ChangeSlot;
    public List<WeaponChangeSlot> SlotList;
    public WeaponStat mWeapon;
    public Transform mChangeParents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DragTarget.color = Color.clear;
            for (int i = 0; i < SaveDataController.Instance.mWeaponInfoArr.Length; i++)
            {
                WeaponCount++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshInventory()
    {
        if (SlotList!=null||SlotList.Count>0)
        {
            DestroyInventory();
        }
        SlotList = new List<WeaponChangeSlot>();
        mSelectSlot.mIcon.color = Color.white;
        mSelectSlot.SetData(GameSetting.Instance.PlayerWeaponID, GameSetting.Instance.mWeaponArr[GameSetting.Instance.PlayerWeaponID].mWeaponImage, SaveDataController.Instance.mWeaponInfoArr[GameSetting.Instance.PlayerWeaponID]);

        for (int i = 0; i < GameSetting.Instance.mWeaponArr.Length; i++)
        {
            if (SaveDataController.Instance.mUser.WeaponHas[i] ==true)
            {
                SlotList.Add(Instantiate(ChangeSlot, mChangeParents));
                SlotList[SlotList.Count-1].SetData(i);
            }


        }
    }

    public void DestroyInventory()
    {
        for (int i = 0; i < SlotList.Count; i++)
        {
            Destroy(SlotList[i].gameObject);
        }
    }


    public bool StartDragging(int id)
    {
        return id < WeaponCount;
    }


}
