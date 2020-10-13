using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponShopSlot : MonoBehaviour, IPointerClickHandler
{
    public int mWeaponID;
    public Weapon mWeapon;
    public WeaponStat mWeaponStat;
    public Image Icon;
    public Text Title, Type;

    public void SetData(int id)
    {
        mWeaponID = id;
        mWeapon = GameSetting.Instance.mWeaponArr[mWeaponID];
        mWeaponStat = SaveDataController.Instance.mWeaponInfoArr[mWeaponID];
        Icon.sprite = GameSetting.Instance.mWeaponArr[mWeaponID].mWeaponImage;
        if (GameSetting.Instance.Language == 0)//한국어
        {
            Title.text = mWeaponStat.Name;
            switch (mWeaponStat.Type)
            {
                case 0:
                    Type.text ="무기 타입: <color=#FE2E2E>근접</color><color=#ffffff>\n</color>";
                    break;
                case 1:
                    Type.text = "무기 타입: <color=#FFBF00>원거리</color><color=#ffffff>\n</color>";
                    break;
                case 2:
                    Type.text = "무기 타입: <color=#FE642E>중거리</color><color=#ffffff>\n</color>";
                    break;

            }
            Type.text += "가격: "+mWeaponStat.Price;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            Title.text = mWeaponStat.EngName;
            switch (mWeaponStat.Type)
            {
                case 0:
                    Type.text = "Weapon Type: <color=#FE2E2E>Short Range</color><color=#ffffff>\n</color>";
                    break;
                case 1:
                    Type.text = "Weapon Type: <color=#FFBF00>Long Range</color><color=#ffffff>\n</color>";
                    break;
                case 2:
                    Type.text = "Weapon Type: <color=#FE642E>Middle Range</color><color=#ffffff>\n</color>";
                    break;

            }
            Type.text += "Price: " + mWeaponStat.Price;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        BlackSmithShopController.Instance.ShowWeaponInfo(mWeapon);
    }
}
