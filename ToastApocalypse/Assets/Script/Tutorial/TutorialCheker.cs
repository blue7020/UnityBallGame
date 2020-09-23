using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheker : MonoBehaviour
{
    public eTutorialStage mStage;
    public bool EndPoint, NextText;
    public Image mDialogUI; //dialogUI는 스크립트로 터치 시 다음 대사로 넘어가기, 마지막 대사라면 다음엔 ui 종료, ui켜졌을땐 pause
    public Text mDialog;
    public TutorialEnd mParts;
    public GameObject Wall;

    private void Awake()
    {
        NextText = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!EndPoint && NextText == false)
            {
                switch (mStage)
                {
                    case eTutorialStage.Start:
                        break;
                    case eTutorialStage.Artifact:
                        TutorialDialog.Instance.ShowDialog();
                        break;
                    case eTutorialStage.Item:
                        TutorialDialog.Instance.ShowDialog();
                        break;
                    case eTutorialStage.Statue:
                        TutorialDialog.Instance.ShowDialog();
                        break;
                    case eTutorialStage.Combat1:
                        TutorialDialog.Instance.ShowDialog();

                        break;
                    case eTutorialStage.Combat2:
                        TutorialDialog.Instance.ShowDialog();
                        break;
                    case eTutorialStage.Skill:
                        TutorialDialog.Instance.ShowDialog();
                        Player.Instance.Heal(Player.Instance.mMaxHP);
                        Player.Instance.NowPlayerSkill.mID = 0;
                        Player.Instance.NowPlayerSkill.mSkillIcon = SkillController.Instance.SkillIcon[0];
                        TutorialUIController.Instance.ShowSkillImage();
                        break;
                    case eTutorialStage.Boss:
                        TutorialDialog.Instance.ShowDialog();
                        break;
                    case eTutorialStage.End:
                        TutorialDialog.Instance.ShowDialog();
                        break;
                }
                TutorialDialog.Instance.GoalText.gameObject.SetActive(true);
                NextText = true;
            }
            else if (EndPoint && NextText == false)
            {
                switch (mStage)
                {
                    case eTutorialStage.Start:
                        Wall.gameObject.SetActive(false);
                        TutorialDialog.Instance.ShowDialog();
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        NextText = true;
                        break;
                    case eTutorialStage.Artifact:
                        if (Player.Instance.mCurrentHP<Player.Instance.mMaxHP)
                        {
                            if (Player.Instance.NowActiveArtifact !=null)
                            {
                                TutorialDialog.Instance.ShowDialog();
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                Wall.gameObject.SetActive(false);
                                NextText = true;
                            }
                            else
                            {
                                NextText = false;
                            }
                        }
                        break;
                    case eTutorialStage.Item:
                        if (Player.Instance.mCurrentHP == Player.Instance.mMaxHP)
                        {
                            TutorialDialog.Instance.ShowDialog();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                        }
                        break;
                    case eTutorialStage.Statue:
                        if (TutorialUIController.Instance.TutorialStatueCheck >= 3)
                        {
                            TutorialDialog.Instance.ShowDialog();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                        }
                        break;
                    case eTutorialStage.Combat1:
                        if (Player.Instance.NowPlayerWeapon.mID == 0 || Player.Instance.NowPlayerWeapon.mID == 1)
                        {
                            TutorialDialog.Instance.ShowDialog();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                        }
                        else
                        {
                            NextText = false;
                        }
                        break;
                    case eTutorialStage.Combat2:
                        if (Player.Instance.mTotalKillCount == 2)
                        {
                            TutorialDialog.Instance.ShowDialog();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                        }
                        break;
                    case eTutorialStage.Skill:
                        TutorialDialog.Instance.ShowDialog();
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        Wall.gameObject.SetActive(false);
                        NextText = true;
                        break;
                    case eTutorialStage.Boss:
                        if (Player.Instance.mTotalKillCount >= 3)
                        {
                            TutorialDialog.Instance.ShowDialog();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                        }
                        break;
                    case eTutorialStage.End:
                        TutorialDialog.Instance.ShowDialog();
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        mParts.gameObject.SetActive(true);//npc 구출 시 대사나오고 파츠 등장하게
                        NextText = true;
                        break;
                }
            }

        }
    }
}
