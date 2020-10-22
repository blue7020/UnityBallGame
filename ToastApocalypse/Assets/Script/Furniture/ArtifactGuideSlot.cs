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
        if (mArtifact != null)
        {
            mArtifactID = id;
            mArtifact = ArtifactGuide.Instance.mInfoArr[mArtifactID];
            mIcon.sprite = GameSetting.Instance.mArtifacts[mArtifactID].mRenderer.sprite;
        }
    }

    public void SetDataUnknown(Sprite spt)
    {
        mIcon.color = Color.white;
        mArtifactID = -1;
        mIcon.sprite = spt;
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
