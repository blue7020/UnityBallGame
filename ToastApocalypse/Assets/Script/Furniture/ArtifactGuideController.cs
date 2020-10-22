using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactGuideController : MonoBehaviour

{
    public static ArtifactGuideController Instance;

    public int ArtifactCount;
    public ArtifactGuideSlot ChangeSlot;
    public ArtifactGuideSlot[] SlotArr;
    public Transform mChangeParents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < ArtifactGuide.Instance.mInfoArr.Length; i++)
        {
            ArtifactCount++;
        }
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

        SlotArr = new ArtifactGuideSlot[ArtifactGuide.Instance.mInfoArr.Length];
        for (int i = 0; i < ArtifactCount; i++)
        {
            SlotArr[i] = Instantiate(ChangeSlot, mChangeParents);
            SlotArr[i].SetData(i);

        }
    }


}
