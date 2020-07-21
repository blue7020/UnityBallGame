using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour,IPointerClickHandler
{
    public int mID;
    public Image mIcon,CoolWheel;
    public Text mSlotText,mCooltimeText;
    public bool hasSkill;
    public SkillData mSkill;

    public void Init(int id, Sprite voidSpt)
    {
        mID = id;
        mSlotText.text = (mID+1).ToString();
        mIcon.sprite = voidSpt;
        hasSkill = false;
        mSkill = null;
    }

    public void SetSkill(int id)
    {
        mIcon.sprite = SkillDataController.Instance.GetSprite(id);
        mSkill = SkillDataController.Instance.GetSkillData(id);
    }

    public IEnumerator Cooltime()
    {
        WaitForSeconds Cool = new WaitForSeconds(mSkill.Cooltime);
        hasSkill = true;
        float max = mSkill.Cooltime;
        float current = max;
        CoolWheel.gameObject.SetActive(true);
        Player.Instance.CastSkill(mID);
        CoolWheel.fillAmount = current / max;

        StartCoroutine(Fillamount(current, max));

        yield return Cool;
        CoolWheel.gameObject.SetActive(false);
        hasSkill = false;
    }

    public IEnumerator Fillamount(float current, float max)
    {
        WaitForEndOfFrame cool = new WaitForEndOfFrame();
        int Cooltime = (int)max;
        while (current > 0)
        {
            yield return cool;
            current -= Time.deltaTime;
            CoolWheel.fillAmount = current / max;
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mSkill!=null)
        {
            if (hasSkill == false)
            {
                StartCoroutine(Cooltime());
            }
        }
        
    }
}
