using System.Collections;
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
        if (GameController.Instance.IsTutorial == false)
        {
            SkillSetting();
        }
        else
        {
            mID = -1;
            TutorialUIController.Instance.ShowSkillImage();
        }
        IsSkillCool = false;

    }

    public void SkillSetting()
    {
        mID = GameSetting.Instance.PlayerSkillID;
        mStat = SkillController.Instance.mStatInfoArr[mID];
        mSkillIcon = SkillController.Instance.SkillIcon[mID];
        Player.Instance.NowPlayerSkill.mID = mID;
        UIController.Instance.ShowSkillImage();
    }

    public void SkillSettingTutorial()
    {
        GameSetting.Instance.PlayerSkillID = 0;
        mID = GameSetting.Instance.PlayerSkillID;
        mStat = SkillController.Instance.mStatInfoArr[mID];
        mSkillIcon = SkillController.Instance.SkillIcon[mID];
        Player.Instance.NowPlayerSkill.mID = 0;
        TutorialUIController.Instance.ShowSkillImage();
    }


    public void SkillUse()
    {
        if (Player.Instance.NowPlayerSkill.mID>-1&&IsSkillCool==false)
        {
            StartCoroutine(SkillCool());
        }
    }

    private IEnumerator SkillCool()
    {
        WaitForSeconds Cool = new WaitForSeconds(mStat.Cooltime);
        SoundController.Instance.SESoundUI(5);
        IsSkillCool = true;
        SkillList.Instance.SkillSetting(mID);
        StartCoroutine(CooltimeRoutine());
        yield return Cool;
    }
    public void ShowCooltime(float maxTime, float currentTime)
    {
        if (GameController.Instance.IsTutorial == false)
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
        else
        {
            if (currentTime > 0)
            {
                TutorialUIController.Instance.mSkillCoolWheel.gameObject.SetActive(true);
                TutorialUIController.Instance.mSkillCoolWheel.fillAmount = currentTime / maxTime;
            }
            else
            {
                IsSkillCool = false;
                TutorialUIController.Instance.mSkillCoolWheel.gameObject.SetActive(false);
            }
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
