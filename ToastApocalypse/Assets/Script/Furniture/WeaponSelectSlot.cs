﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSelectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int mWeaponID;
    public int mDraggingID;

    public Image mIcon;
    public WeaponStat mWeapon;
    public SkillTooltip mTooltip;
    public string title, lore;

    private void Awake()
    {
        mDraggingID = -1;
    }

    public void SetData(int id,Sprite sprite, WeaponStat weapon)
    {
        SoundController.Instance.SESound(7);
        mWeaponID = id;
        mIcon.sprite = sprite;
        mWeapon = weapon;
        if (mWeapon != null)
        {
            if (GameSetting.Instance.Language == 0)//한국어
            {
                lore = mWeapon.ContensFormat + "\n";
                if (mWeapon.Atk > 0)
                {
                    lore += "공격력: +" + mWeapon.Atk + "\n";
                }
                if (mWeapon.AtkSpd > 0)
                {
                    lore += "공격 딜레이: " + mWeapon.AtkSpd + "초\n";
                }
                if (mWeapon.Crit > 0)
                {
                    lore += " 치명타 확률: +" + mWeapon.Crit + "%\n";
                }
                if (mWeapon.Bullet > 0&&weapon.Type==1)
                {
                    lore += "탄약 수: " + mWeapon.Bullet + "발\n";
                }
                if (mWeapon.ReloadCool > 0)
                {
                    lore += "장전 시간: " + mWeapon.ReloadCool + "초\n";
                }
                if (mWeapon != null)
                {
                    switch (mWeapon.Type)
                    {
                        case 0:
                            title = mWeapon.Name + " (무기 타입: <color=#FE2E2E>근접</color><color=#ffffff>)\n</color>";
                            break;
                        case 1:
                            title = mWeapon.Name + " (무기 타입: <color=#FFBF00>원거리</color><color=#ffffff>)\n</color>";
                            break;
                        case 2:
                            title = mWeapon.Name + " (무기 타입: <color=#FE642E>중거리</color><color=#ffffff>)\n</color>";
                            break;

                    }
                }
                else
                {
                    title = "무기를 선택해주십시오";
                }
            }
            else if (GameSetting.Instance.Language == 1)//영어
            {
                lore = mWeapon.EngContensFormat + "\n";
                if (mWeapon.Atk > 0)
                {
                    lore += "Atk: +" + mWeapon.Atk + "\n";
                }
                if (mWeapon.AtkSpd > 0)
                {
                    lore += "Atk Delay: +" + mWeapon.AtkSpd + "Sec\n";
                }
                if (mWeapon.Crit > 0)
                {
                    lore += "Critical chance: +" + mWeapon.Crit + "%\n";
                }
                if (mWeapon.Bullet > 0 && weapon.Type == 1)
                {
                    lore += "Bullets: +" + mWeapon.Bullet + "\n";
                }
                if (mWeapon.ReloadCool > 0)
                {
                    lore += "Reload Time: " + mWeapon.ReloadCool + "Sec\n";
                }
                if (mWeapon != null)
                {
                    switch (mWeapon.Type)
                    {
                        case 0:
                            title = mWeapon.EngName + " (Weapon Type: <color=#FE2E2E>Short Range</color><color=#ffffff>)\n</color>";
                            break;
                        case 1:
                            title = mWeapon.EngName + " (Weapon Type: <color=#FFBF00>Long Range</color><color=#ffffff>)\n</color>";
                            break;
                        case 2:
                            title = mWeapon.EngName + " (Weapon Type: <color=#FE642E>Middle Range</color><color=#ffffff>)\n</color>";
                            break;
                    }
                }
                else
                {
                    title = "Please select using weapon";
                }

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mDraggingID = mWeaponID;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mDraggingID = -1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mTooltip.SetData(title, lore, mIcon.sprite);
        mTooltip.gameObject.SetActive(true);
    }
}
