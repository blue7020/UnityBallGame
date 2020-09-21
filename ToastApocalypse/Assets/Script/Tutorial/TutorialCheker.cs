using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheker : MonoBehaviour
{
    public eTutorialStage mStage;
    public int mEventCount;
    public bool mEventSwitch, EndPoint;
    public Image mDialogUI; //dialogUI는 스크립트로 터치 시 다음 대사로 넘어가기, 마지막 대사라면 다음엔 ui 종료, ui켜졌을땐 pause
    public Text mDialog;
    public TutorialEnd mParts;
    public GameObject Wall;

    private void Awake()
    {
        mEventCount = 0;
        mEventSwitch = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (mStage)
            {
                case eTutorialStage.Start:
                    if (mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        Wall.SetActive(false);
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        TutorialDialog.Instance.ShowDialog();
                    }
                    break;
                case eTutorialStage.Artifact:
                    if (mEventCount==0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if(mEventCount == 1&&mEventSwitch==false)
                    {
                        if (InventoryController.Instance.mSlotArr[0].artifact.mID == 5)
                        {
                            if (Player.Instance.NowActiveArtifact.mID == 15)
                            {
                                mEventSwitch = true;
                                Wall.SetActive(false);
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                break;
                            }
                        }
                    }
                    break;
                case eTutorialStage.Item:
                    if (mEventCount == 0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if (mEventCount == 1 && Player.Instance.mCurrentHP==Player.Instance.mMaxHP && mEventSwitch == false)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                    }
                    break;
                case eTutorialStage.Statue:
                    if (mEventCount == 0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if (mEventCount == 1 && mEventSwitch == false&&TutorialUIController.Instance.TutorialStatueCheck>=3)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                    }
                    break;
                case eTutorialStage.Combat1:
                    if (mEventCount == 0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if (mEventCount == 1 && mEventSwitch == false)
                    {
                        if (Player.Instance.NowPlayerWeapon.mID == 0|| Player.Instance.NowPlayerWeapon.mID == 1)
                        {
                            mEventSwitch = true;
                            Wall.SetActive(false);
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        }
                    }
                    break;
                case eTutorialStage.Combat2:
                    if (mEventCount == 0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if (mEventCount == 1 && Player.Instance.mTotalKillCount>=2 && mEventSwitch == false)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                    }
                    break;
                case eTutorialStage.Skill:
                    if (mEventCount == 0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        Player.Instance.Heal(Player.Instance.mMaxHP);
                        Player.Instance.NowPlayerSkill.mID = 0;
                        Player.Instance.NowPlayerSkill.mSkillIcon = SkillController.Instance.SkillIcon[0];
                        TutorialUIController.Instance.ShowSkillImage();
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if (mEventCount == 1 && EndPoint==true && mEventSwitch == false)
                    {
                        mEventSwitch = true;
                        Wall.SetActive(false);
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                    }
                    break;
                case eTutorialStage.End:
                    if (mEventCount == 0 && mEventSwitch == true)
                    {
                        mEventSwitch = false;
                        mEventCount++;
                        TutorialDialog.Instance.ShowDialog();
                    }
                    else if (mEventCount == 0 && mEventSwitch == false && EndPoint == true)//npc 구출 시 대사나오고 파츠 등장하게
                    {
                        mEventSwitch = true;
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        mParts.gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }
}
