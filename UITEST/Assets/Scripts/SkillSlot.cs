using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField]
    private Image mItemImage,mCooltimeImage;
    [SerializeField]
    private Text mSlotNumber;
    public SkillData mSkill;
    private int currentTime;
    private int mID;

    public void Init(int id, Sprite image)
    {
        mID = id;
        mSlotNumber.text = (mID+1).ToString();
        mItemImage.sprite = image;
    }


    public void SetSprite(Sprite image)
    {
        mItemImage.sprite = image;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mCooltimeImage.gameObject.SetActive(true);
        StartCoroutine(Cooltime());
    }

    private IEnumerator Cooltime()
    {
        WaitForSeconds onesec = new WaitForSeconds(1f);
        mCooltimeImage.fillAmount = currentTime / mSkill.Cooltime;
        while (mCooltimeImage.fillAmount!=0)
        {
            mCooltimeImage.fillAmount -= Time.deltaTime;
            yield return onesec;
        }

        mCooltimeImage.gameObject.SetActive(false);

    }
}
