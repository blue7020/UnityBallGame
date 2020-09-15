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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < SkillController.Instance.mStatInfoArr.Length; i++)
        {
            WeaponCount++;
        }
        mSelectSlot.mIcon.sprite = GameSetting.Instance.mWeapons[GameSetting.Instance.PlayerWeaponID].mWeaponImage;
        Instance.mSelectSlot.mIcon.color = Color.white;
        Instance.mSelectSlot.mWeapon = GameSetting.Instance.mInfoArr[GameSetting.Instance.PlayerWeaponID];
        Instance.mSelectSlot.mWeaponID = GameSetting.Instance.PlayerSkillID;
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

        SlotArr = new WeaponChangeSlot[GameSetting.Instance.PlayerHasWeapon.Length];
        for (int i = 0; i < SlotArr.Length; i++)
        {
            if (GameSetting.Instance.PlayerHasWeapon[i] == true)
            {
                SlotArr[i] = Instantiate(ChangeSlot, mChangeParents);
                SlotArr[i].SetData(i);
            }

        }
    }


    public bool StartDragging(int id)
    {
        return id < WeaponCount;
    }


}
