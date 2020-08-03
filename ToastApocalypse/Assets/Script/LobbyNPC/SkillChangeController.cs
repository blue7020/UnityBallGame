using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChangeController : MonoBehaviour

{
    public static SkillChangeController Instance;

    public SkillSelectSlot mSelectSlot;
    public Transform Canvas;
    public Image DragTarget;

    public int SkillCount;
    public SkillChangeSlot ChangeSlot;
    public SkillChangeSlot[] SlotArr;
    public Transform mChangeParents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SkillCount = 0;
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
            SkillCount++;
        }
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        if(SlotArr != null)
        {
            for(int i = 0; i < SlotArr.Length; i++)
            {
                Destroy(SlotArr[i].gameObject);
            }
        }

        SlotArr = new SkillChangeSlot[SkillCount];
        for (int i = 0; i < SkillCount; i++)
        {
            SlotArr[i] = Instantiate(ChangeSlot, mChangeParents);
            if (GameSetting.Instance.PlayerHasSkill[i] == true)
            {
                SlotArr[i].SetData(i);
            }

        }
    }


    public bool StartDragging(int id)
    {
        return id < SkillCount;
    }


}
