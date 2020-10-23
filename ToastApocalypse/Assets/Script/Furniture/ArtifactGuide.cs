using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactGuide : InformationLoader
{
    public static ArtifactGuide Instance;

    public Text mTitle, mArtifactTitle, mLore;
    public Image mArtifactWindow;

    public ArtifactStat[] mInfoArr;
    public ArtifactTextStat[] mTextInfoArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.ARTIFACT_STAT);
            LoadJson(out mTextInfoArr, Path.ARTIFACT_TEXT_STAT);
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mTitle.text = "유물 가이드";
            mArtifactTitle.text = "유물";
            mLore.text = "유물을 터치하면 설명이 표시됩니다";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mTitle.text = "Artifact Guide";
            mArtifactTitle.text = "Artifact";
            mLore.text = "Touch the Artifact to show description";
        }
    }

    public void ShowDescription(int id)
    {
        if (id<0)
        {
            if (GameSetting.Instance.Language == 0)
            {
                mArtifactTitle.text = "[미발견 유물]";
                mLore.text = "이 유물은 아직 발견되지 않았습니다";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mArtifactTitle.text = "[Undiscovered Artifact]";
                mLore.text = "This artifact has not yet been found";
            }
        }
        else
        {
            if (GameSetting.Instance.mArtifacts[id].eType == eArtifactType.Active)
            {
                if (GameSetting.Instance.Language == 0)
                {
                    mArtifactTitle.text = "[" + mTextInfoArr[id].Title + "]";
                    mLore.text = "재사용 대기시간: " + mInfoArr[id].Skill_Cooltime + "초\n" + mTextInfoArr[id].ContensFormat + "\n\n\"" + mTextInfoArr[id].PlayableText + "\"";
                }
                else if (GameSetting.Instance.Language == 1)
                {
                    mArtifactTitle.text = "[" + mTextInfoArr[id].EngTitle + "]";
                    mLore.text = "CoolTime: " + mInfoArr[id].Skill_Cooltime + "Sec\n" + mTextInfoArr[id].EngContensFormat + "\n\n\"" + mTextInfoArr[id].EngPlayableText + "\"";
                }
            }
            else
            {
                if (GameSetting.Instance.Language == 0)//한국어
                {
                    mArtifactTitle.text = "[" + mTextInfoArr[id].Title + "]";
                    mLore.text = mTextInfoArr[id].ContensFormat + "\n";
                    if (mInfoArr[id].Heal > 0)
                    {
                        mLore.text += "회복량: " + mInfoArr[id].Heal + "\n";
                    }
                    if (mInfoArr[id].Atk > 0)
                    {
                        mLore.text += "공격력: +" + mInfoArr[id].Atk * 100 + "%\n";
                    }
                    if (mInfoArr[id].Def > 0)
                    {
                        mLore.text += "방어력: +" + mInfoArr[id].Def * 100 + "%\n";
                    }
                    if (mInfoArr[id].AtkSpd > 0)
                    {
                        mLore.text += "공격 속도: +" + mInfoArr[id].AtkSpd * 100 + "%\n";
                    }
                    if (mInfoArr[id].Crit > 0)
                    {
                        mLore.text += "치명타 확률: +" + mInfoArr[id].Crit * 100 + "%\n";
                    }
                    if (mInfoArr[id].Spd > 0)
                    {
                        mLore.text += "이동 속도: +" + mInfoArr[id].Spd * 100 + "%\n";
                    }
                    if (mInfoArr[id].CooltimeReduce > 0)
                    {
                        mLore.text += "재사용 대기시간 감소: +" + mInfoArr[id].CooltimeReduce * 100 + "%\n";
                    }
                    if (mInfoArr[id].CCReduce > 0)
                    {
                        mLore.text += "상태이상 저항: +" + mInfoArr[id].CCReduce * 100 + "%\n";
                    }
                    mLore.text += "\n\n\"" + mTextInfoArr[id].PlayableText + "\"";
                }
                else if (GameSetting.Instance.Language == 1)//영어
                {
                    mArtifactTitle.text = "[" + mTextInfoArr[id].EngTitle + "]";
                    mLore.text = mTextInfoArr[id].EngContensFormat + "\n";
                    if (mInfoArr[id].Heal > 0)
                    {
                        mLore.text += "Heal amount: " + mInfoArr[id].Heal + "\n";
                    }
                    if (mInfoArr[id].Atk > 0)
                    {
                        mLore.text += "Atk: +" + mInfoArr[id].Atk * 100 + "%\n";
                    }
                    if (mInfoArr[id].Def > 0)
                    {
                        mLore.text += "Def: +" + mInfoArr[id].Def * 100 + "%\n";
                    }
                    if (mInfoArr[id].AtkSpd > 0)
                    {
                        mLore.text += "Atk Speed: +" + mInfoArr[id].AtkSpd * 100 + "%\n";
                    }
                    if (mInfoArr[id].Crit > 0)
                    {
                        mLore.text += "Critical chance: +" + mInfoArr[id].Crit * 100 + "%\n";
                    }
                    if (mInfoArr[id].Spd > 0)
                    {
                        mLore.text += "Speed: +" + mInfoArr[id].Spd * 100 + "%\n";
                    }
                    if (mInfoArr[id].CooltimeReduce > 0)
                    {
                        mLore.text += "Cooltime Reduce: +" + mInfoArr[id].CooltimeReduce * 100 + "%\n";
                    }
                    if (mInfoArr[id].CCReduce > 0)
                    {
                        mLore.text += "Resistance: +" + mInfoArr[id].CCReduce * 100 + "%\n";
                    }
                    mLore.text += "\n\n\"" + mTextInfoArr[id].EngPlayableText + "\"";
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        mArtifactWindow.gameObject.SetActive(true);
    }
}
