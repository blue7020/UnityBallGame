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
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        mStat = SkillController.Instance.mStatInfoArr[mID];
        IsSkillCool = false;
        mSkillIcon = SkillController.Instance.SkillIcon[mID];

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
        //TODO 델리게이트로 해당하는 ID의 스킬 가져오기
        StartCoroutine(CooltimeRoutine());
        Debug.Log("스킬 사용");
        yield return Cool;
    }
    public void ShowCooltime(float maxTime, float currentTime)
    {
        if (currentTime > 0)
        {
            UIController.Instance.SkillCoolWheel.gameObject.SetActive(true);
            UIController.Instance.SkillCoolWheel.fillAmount = currentTime / maxTime;
        }
        else
        {
            IsSkillCool = false;
            UIController.Instance.SkillCoolWheel.gameObject.SetActive(false);
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
