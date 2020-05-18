﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{

    [SerializeField]
    private Button mSkillButton;
    [SerializeField]
    private TextMeshProUGUI mSkillTitleText;
    [SerializeField]
    private Image mCooldownImage;
    [SerializeField]
    private TextMeshProUGUI mCooldownText;

    public void SetButtinText(string title)//다국어 지원을 위해 텍스트를 메서드에 빼줌
    {
        mSkillTitleText.text = title;
    }

    public void SetButtonActive(bool isActive)
    {
        mSkillButton.interactable = isActive;
    }

    public void ShowCooltime(float currentTime, float maxTime)
    {
        if (currentTime > 0) 
        {
            mCooldownImage.gameObject.SetActive(true);
            mCooldownImage.fillAmount = currentTime / maxTime;
            int min = (int)(currentTime / 60f); //처음부터 int로 하면 오차가 생기니 float으로 계산 후 int로 변환
            int sec = (int)(currentTime % 60f);

            mCooldownText.text = string.Format("{0} : {1}", min.ToString("D2"), sec.ToString("D2"));
        }
        else
        {
            mCooldownImage.gameObject.SetActive(false);
        }
    }

}
