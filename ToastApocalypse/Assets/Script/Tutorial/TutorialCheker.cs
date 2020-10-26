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
    public SpriteRenderer mRenderer;

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
                Time.timeScale = 0;
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
                        TutorialUIController.Instance.mPlayerSkill.SkillSettingTutorial();
                        TutorialDialog.Instance.ShowDialog();
                        break;
                    case eTutorialStage.Boss:
                        TutorialDialog.Instance.ShowDialog();
                        SoundController.Instance.SESound(17);
                        Wall.gameObject.SetActive(true);
                        Instantiate(TutorialUIController.Instance.mEnemy[3],TutorialUIController.Instance.mSpawnPos[3]);
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
                Time.timeScale = 1;
                switch (mStage)
                {
                    case eTutorialStage.Start:
                        Wall.gameObject.SetActive(false);
                        TutorialDialog.Instance.gameObject.SetActive(true);
                        TutorialDialog.Instance.DialogEvent();
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        NextText = true;
                        mRenderer.color = Color.clear;
                        break;
                    case eTutorialStage.Artifact:
                        if (Player.Instance.mCurrentHP<Player.Instance.mMaxHP)
                        {
                            if (Player.Instance.NowActiveArtifact !=null)
                            {
                                TutorialDialog.Instance.gameObject.SetActive(true);
                                TutorialDialog.Instance.DialogEvent();
                                TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                                Wall.gameObject.SetActive(false);
                                NextText = true;
                                mRenderer.color = Color.clear;
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
                            TutorialDialog.Instance.gameObject.SetActive(true);
                            TutorialDialog.Instance.DialogEvent();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                            mRenderer.color = Color.clear;
                        }
                        break;
                    case eTutorialStage.Statue:
                        if (TutorialUIController.Instance.TutorialStatueCheck >= 3)
                        {
                            TutorialDialog.Instance.gameObject.SetActive(true);
                            TutorialDialog.Instance.DialogEvent();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                            mRenderer.color = Color.clear;
                        }
                        break;
                    case eTutorialStage.Combat1:
                        if (Player.Instance.NowPlayerWeapon.mID == 0 || Player.Instance.NowPlayerWeapon.mID == 1)
                        {
                            TutorialDialog.Instance.gameObject.SetActive(true);
                            TutorialDialog.Instance.DialogEvent();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                            mRenderer.color = Color.clear;
                        }
                        else
                        {
                            NextText = false;
                        }
                        break;
                    case eTutorialStage.Combat2:
                        if (Player.Instance.mTotalKillCount == 2)
                        {
                            TutorialDialog.Instance.gameObject.SetActive(true);
                            TutorialDialog.Instance.DialogEvent();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                            mRenderer.color = Color.clear;
                            Player.Instance.Heal(Player.Instance.mMaxHP);
                        }
                        break;
                    case eTutorialStage.Skill:
                        TutorialDialog.Instance.gameObject.SetActive(true);
                        TutorialDialog.Instance.DialogEvent();
                        TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                        Wall.gameObject.SetActive(false);
                        NextText = true;
                        mRenderer.color = Color.clear;
                        break;
                    case eTutorialStage.Boss:
                        if (Player.Instance.mTotalKillCount >= 3)
                        {
                            TutorialDialog.Instance.gameObject.SetActive(true);
                            TutorialDialog.Instance.DialogEvent();
                            TutorialDialog.Instance.GoalText.gameObject.SetActive(false);
                            Wall.gameObject.SetActive(false);
                            NextText = true;
                            mRenderer.color = Color.clear;
                            Player.Instance.Heal(Player.Instance.mMaxHP);
                        }
                        break;
                    case eTutorialStage.End:
                        break;
                }
            }

        }
    }
}
