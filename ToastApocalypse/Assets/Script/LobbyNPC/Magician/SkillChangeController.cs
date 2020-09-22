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
    public SkillStat mSkill;
    public SkillText mSkillText;
    public Transform mChangeParents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DragTarget.color = Color.clear;
            for (int i = 0; i < SkillController.Instance.mStatInfoArr.Length; i++)
            {
                SkillCount++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RefreshInventory()
    {
        Instance.mSelectSlot.mIcon.color = Color.white;
        mSelectSlot.SetData(GameSetting.Instance.PlayerSkillID, SkillController.Instance.SkillIcon[GameSetting.Instance.PlayerSkillID]);

        SlotArr = new SkillChangeSlot[GameSetting.Instance.PlayerHasSkill.Length];
        for (int i = 0; i < SlotArr.Length; i++)
        {
            if (GameSetting.Instance.PlayerHasSkill[i] == true)
            {
                SlotArr[i] = Instantiate(ChangeSlot, mChangeParents);
                SlotArr[i].SetData(i);
            }

        }
    }


    public void DestroyInventory()
    {
        for (int i = 0; i < SlotArr.Length; i++)
        {
            if (GameSetting.Instance.PlayerHasSkill[i] == true)
            {
                Destroy(SlotArr[i].gameObject);
            }
        }
    }

    public bool StartDragging(int id)
    {
        return id < SkillCount;
    }


}
