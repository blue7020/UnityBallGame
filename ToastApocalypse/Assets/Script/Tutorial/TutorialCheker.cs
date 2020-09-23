using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheker : MonoBehaviour
{
    public eTutorialStage mStage;
    public int mEventCount;
    public bool EndPoint,mDoorSwitch;
    public Image mDialogUI; //dialogUI는 스크립트로 터치 시 다음 대사로 넘어가기, 마지막 대사라면 다음엔 ui 종료, ui켜졌을땐 pause
    public Text mDialog;
    public TutorialEnd mParts;
    public GameObject Wall;

    private void Awake()
    {
        mEventCount = 0;
        mDoorSwitch = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (mStage)
            {
                case eTutorialStage.Start:
                    switch (mEventCount)
                    {
                        case 0:
                            Wall.gameObject.SetActive(false);
                            mEventCount++;
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        default:
                            break;
                    }
                    break;
                case eTutorialStage.Artifact:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        case 1:
                            if (InventoryController.Instance.mSlotArr[0].artifact.mID == 5&& Player.Instance.NowActiveArtifact.mID == 15 && EndPoint == true)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mDoorSwitch = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (mDoorSwitch==true)
                    {
                        Debug.Log("1");
                        Time.timeScale = 1;
                        Wall.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                    }
                    break;
                case eTutorialStage.Item:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        case 1:
                            if (Player.Instance.mCurrentHP == Player.Instance.mMaxHP&& EndPoint == true)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mDoorSwitch = true;
                            }
                            break;
                        default:
                            break;

                    }
                    if (mDoorSwitch == true)
                    {
                        Time.timeScale = 1;
                        Wall.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                    }
                    break;
                case eTutorialStage.Statue:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        case 1:
                            if (TutorialUIController.Instance.TutorialStatueCheck >= 3 && EndPoint == true)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mDoorSwitch = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (mDoorSwitch == true)
                    {
                        Time.timeScale = 1;
                        Wall.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                    }
                    break;
                case eTutorialStage.Combat1:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        case 1:
                            if (EndPoint == true && Player.Instance.NowPlayerWeapon.mID == 0 || Player.Instance.NowPlayerWeapon.mID == 1)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mDoorSwitch = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (mDoorSwitch == true)
                    {
                        Time.timeScale = 1;
                        Wall.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                    }
                    break;
                case eTutorialStage.Combat2:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        case 1:
                            if (EndPoint == true && Player.Instance.mTotalKillCount >= 2)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mDoorSwitch = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (mDoorSwitch == true)
                    {
                        Time.timeScale = 1;
                        Wall.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                    }
                    break;
                case eTutorialStage.Skill:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            Player.Instance.Heal(Player.Instance.mMaxHP);
                            Player.Instance.NowPlayerSkill.mID = 0;
                            Player.Instance.NowPlayerSkill.mSkillIcon = SkillController.Instance.SkillIcon[0];
                            TutorialUIController.Instance.ShowSkillImage();
                            break;
                        case 1:
                            if (EndPoint == true)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mDoorSwitch = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (mDoorSwitch == true)
                    {
                        Time.timeScale = 1;
                        Wall.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                    }
                    break;
                case eTutorialStage.End:
                    switch (mEventCount)
                    {
                        case 0:
                            mEventCount++;
                            TutorialDialog.Instance.ShowDialog();
                            break;
                        case 1:
                            if (EndPoint == true)
                            {
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                TutorialDialog.Instance.ShowDialog();
                                mEventCount++;
                                mParts.gameObject.SetActive(true);//npc 구출 시 대사나오고 파츠 등장하게
                            }
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
    }
}
