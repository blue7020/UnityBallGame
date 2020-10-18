using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour,IPointerClickHandler
{
    public int SkillID;
    public SkillStat mSkill;
    public SkillText mSkillText;
    public Image Icon;
    public Text Title, Price;

    public void SetData(int id)
    {
        SkillID = id;
        mSkill = SkillController.Instance.mStatInfoArr[SkillID];
        mSkillText = SkillController.Instance.mTextInfoArr[SkillID];
        Icon.sprite = SkillController.Instance.SkillIcon[SkillID];
        if (GameSetting.Instance.Language == 0)//한국어
        {
            Price.text = "가격: " + mSkillText.Price+ "시럽";
            Title.text = mSkillText.Title;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            Price.text = "Price: " + mSkillText.Price+"Syrup";
            Title.text = mSkillText.EngTitle;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillShop.Instance.ShowSkillInfo(mSkill, mSkillText);
    }
}
