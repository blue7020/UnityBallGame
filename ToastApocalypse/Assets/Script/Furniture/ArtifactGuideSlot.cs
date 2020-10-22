using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArtifactGuideSlot : MonoBehaviour, IPointerClickHandler
{
    public int mArtifactID;

    public Image mIcon;
    public ArtifactStat mArtifact;
    public string title, lore;

    public void SetData(int id)
    {
        mIcon.color = Color.white;
        if (mArtifact != null && GameSetting.Instance.Ingame == false)
        {
            mArtifact = ArtifactGuide.Instance.mInfoArr[mArtifactID];
        }
        mArtifactID = id;
        mIcon.sprite = GameSetting.Instance.mArtifacts[mArtifactID].mRenderer.sprite;
    }

    public void RemoveData()
    {
        mArtifactID = -1;
        mIcon.color = Color.clear;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ArtifactGuide.Instance.ShowDescription(mArtifactID);
    }
}
