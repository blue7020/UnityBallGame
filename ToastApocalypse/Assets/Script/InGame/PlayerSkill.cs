﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill Insatnce;

    public int mID;
    public Sprite mSkillIcon;

    public SkillStat mStat;

    private bool IsSkillCool;

    private void Awake()
    {
        if (Insatnce==null)
        {
            Insatnce = this;
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        mID = GameSetting.Instance.PlayerSkillID;
        mStat = SkillController.Instance.mStatInfoArr[mID];
        mSkillIcon = SkillController.Instance.SkillIcon[mID];
        IsSkillCool = false;
        UIController.Instance.ShowSkillImage();

    }
    public void SkillUse()
    {
        if (IsSkillCool==false)
        {
            StartCoroutine(SkillCool());
        }
    }

    private IEnumerator SkillCool()
    {
        WaitForSeconds Cool = new WaitForSeconds(mStat.Cooltime);
        IsSkillCool = true;
        SkillList.Instance.SkillSetting(mID);
        StartCoroutine(CooltimeRoutine());
        yield return Cool;
    }
    public void ShowCooltime(float maxTime, float currentTime)
    {
        if (currentTime > 0)
        {
            UIController.Instance.mSkillCoolWheel.gameObject.SetActive(true);
            UIController.Instance.mSkillCoolWheel.fillAmount = currentTime / maxTime;
        }
        else
        {
            IsSkillCool = false;
            UIController.Instance.mSkillCoolWheel.gameObject.SetActive(false);
        }
    }

    private IEnumerator CooltimeRoutine()
    {
        float maxTime = mStat.Cooltime + (mStat.Cooltime - (mStat.Cooltime * (1 + Player.Instance.mStats.CooltimeReduce)));
        float CoolTime = maxTime;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float AttackCurrentTime = CoolTime;
        while (AttackCurrentTime >= 0)
        {
            yield return frame;
            AttackCurrentTime -= Time.fixedDeltaTime;
            ShowCooltime(CoolTime, AttackCurrentTime);
        }
    }
}
