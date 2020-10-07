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
    public WeaponChangeSlot[] SlotArr;
    public WeaponStat mWeapon;
    public Transform mChangeParents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DragTarget.color = Color.clear;
            for (int i = 0; i < GameSetting.Instance.mWeaponInfoArr.Length; i++)
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
        mSelectSlot.SetData(GameSetting.Instance.PlayerWeaponID, GameSetting.Instance.mWeaponArr[GameSetting.Instance.PlayerWeaponID].mWeaponImage, GameSetting.Instance.mWeaponInfoArr[GameSetting.Instance.PlayerWeaponID]);

        SlotArr = new WeaponChangeSlot[GameSetting.Instance.mWeaponInfoArr.Length];
        for (int i = 0; i < SlotArr.Length; i++)
        {
            if (GameSetting.Instance.mWeaponInfoArr[i].PlayerHas == true)
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
            if (GameSetting.Instance.mWeaponInfoArr[i].PlayerHas == true)
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
