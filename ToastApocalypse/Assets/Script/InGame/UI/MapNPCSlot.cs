using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNPCSlot : MonoBehaviour
{
    public int mID;
    public Image mIcon;

    public void SetData(int id)
    {
        mID = id;
        mIcon.sprite = MapNPCController.Instance.mNPCArr[mID].mRenderer.sprite;
    }
}
