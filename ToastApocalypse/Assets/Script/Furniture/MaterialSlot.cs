using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MaterialSlot : MonoBehaviour, IPointerClickHandler
{
    public int mMaterialID;

    public Image mIcon;
    public MaterialStat mMaterial;
    public string title, lore;
    public Text mCount;
    public int mAmount;//상점용

    public void SetData(int id)
    {
        if (mMaterial != null && GameSetting.Instance.Ingame == false)
        {
            mMaterial = MaterialController.Instance.mInfoArr[mMaterialID];
        }
        mMaterialID = id;
        mIcon.sprite = GameSetting.Instance.mMaterialSpt[mMaterialID];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mMaterial != null&&GameSetting.Instance.Ingame==false&& BlackSmith.Instance.IsShop==false)
        {
            MaterialController.Instance.ShowDescription(mMaterialID);
        }

    }
}
