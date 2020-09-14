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

    public void SetData(int id)
    {
        if (mMaterial != null && GameSetting.Instance.Ingame == false)
        {
            mMaterial = MaterialController.Instance.mInfoArr[mMaterialID];
        }
        mMaterialID = id;
        mIcon.sprite = GameSetting.Instance.mMaterialSpt[mMaterialID];
        mCount.text = GameSetting.Instance.HasMaterial[mMaterialID].ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mMaterial != null&&GameSetting.Instance.Ingame==false)
        {
            MaterialController.Instance.ShowDescription(mMaterialID);
        }

    }
}
