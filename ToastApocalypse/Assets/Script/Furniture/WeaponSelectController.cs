﻿using System.Collections;
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
    public WeaponChangeSlot[] SlotArr;
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
        mSelectSlot.mIcon.color = Color.white;
        mSelectSlot.SetData(GameSetting.Instance.PlayerWeaponID, GameSetting.Instance.mWeaponArr[GameSetting.Instance.PlayerWeaponID].mWeaponImage, SaveDataController.Instance.mWeaponInfoArr[GameSetting.Instance.PlayerWeaponID]);

        SlotArr = new WeaponChangeSlot[SaveDataController.Instance.mWeaponInfoArr.Length];
        for (int i = 0; i < SlotArr.Length; i++)
        {
            if (SaveDataController.Instance.mUser.WeaponHas[i] == true)
            {
                SlotArr[i] = Instantiate(ChangeSlot, mChangeParents);
                SlotArr[i].SetData(i);
            }

        }
    }

    public void DestroyInventory()
    {
        for (int i = 0; i < SlotArr.Length; i++)
        {
            if (SaveDataController.Instance.mUser.WeaponHas[i] == true)
            {
                Destroy(SlotArr[i].gameObject);
            }
        }
    }


    public bool StartDragging(int id)
    {
        return id < WeaponCount;
    }


}
