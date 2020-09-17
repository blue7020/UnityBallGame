﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheker : MonoBehaviour
{
    public eTutorialStage mStage;
    public int mEventCount;
    public bool mEventSwitch;
    public Image mDialogUI; //dialogUI는 스크립트로 터치 시 다음 대사로 넘어가기, 마지막 대사라면 다음엔 ui 종료, ui켜졌을땐 pause
    public Text mDialog;
    public TutorialEnd mParts;
    public GameObject Wall;

    private void Awake()
    {
        mEventCount = 0;
        mEventSwitch = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (mStage)
            {
                case eTutorialStage.Artifact:
                    if (mEventCount==0)
                    {
                        mEventCount++;
                        //메시지 출력
                    }
                    else if(mEventCount == 1&&mEventSwitch==false)
                    {
                        for (int i=0; i<GameSetting.Instance.mArtifacts.Length;i++)
                        {
                            if (InventoryController.Instance.mSlotArr[i].artifact.mID==5&&Player.Instance.NowActiveArtifact.mID==15)
                            {
                                mEventSwitch = true;
                                Wall.SetActive(false);
                                break;
                            }
                        }
                    }
                    break;
                case eTutorialStage.Item:
                    if (mEventCount == 0)
                    {
                        mEventCount++;
                        //메시지 출력
                    }
                    else if (mEventCount == 1 && Player.Instance.mCurrentHP==Player.Instance.mMaxHP && mEventSwitch == false)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                    }
                    break;
                case eTutorialStage.Statue:
                    break;
                case eTutorialStage.Combat1:
                    break;
                case eTutorialStage.Combat2:
                    if (mEventCount == 0)
                    {
                        mEventCount++;
                        //메시지 출력
                    }
                    else if (mEventCount == 1 && Player.Instance.mTotalKillCount>=2 && mEventSwitch == false)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                    }
                    break;
                case eTutorialStage.Skill:
                    if (mEventCount == 0)
                    {
                        mEventCount++;
                        Player.Instance.Heal(Player.Instance.mMaxHP);
                        Player.Instance.NowPlayerSkill.mID = 0;
                        Player.Instance.NowPlayerSkill.mSkillIcon = SkillController.Instance.SkillIcon[0];
                        TutorialUIController.Instance.ShowSkillImage();
                        //메시지 출력
                    }
                    else if (mEventCount == 1 && Player.Instance.mCurrentHP == Player.Instance.mMaxHP && mEventSwitch == false)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                    }
                    break;
                case eTutorialStage.End:
                    if (mEventCount == 0)
                    {
                        mEventCount++;
                        //메시지 출력
                    }
                    if (mEventCount == 0 && mEventSwitch == true)//구출한 npc가 true로 변경함
                    {
                        //메시지 출력
                        mParts.gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }
}
