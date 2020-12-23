using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuffSelect : MonoBehaviour,IPointerDownHandler
{
    public int mID;
    public Text mText;
    public Image mIcon;

    public void SetBuff()
    {
        mIcon.sprite = BuffSelectController.Instance.mBuffIconArr[mID];
        if (GameSetting.Instance.Language == 0)
        {
            switch (mID)
            {
                case 0:
                    mText.text = "체력 모두 회복";
                    break;
                case 1:
                    mText.text = "최대 체력 +1";
                    break;
                case 2:
                    mText.text = "부활 토큰 +1";
                    break;
                case 3:
                    mText.text = "골드 +100";
                    break;
                case 4:
                    mText.text = "공격력 +30%";
                    break;
                case 5:
                    mText.text = "방어력 +30%";
                    break;
                case 6:
                    mText.text = "공격 속도 +30%";
                    break;
                case 7:
                    mText.text = "치명타 확률 +30%";
                    break;
                case 8:
                    mText.text = "치명타 데미지 +30%";
                    break;
                case 9:
                    mText.text = "상태이상 저항 +30%";
                    break;
                case 10:
                    mText.text = "재사용 대기시간 감소 +30%";
                    break;
                case 11:
                    mText.text = "추가 탄환 +1";
                    break;
                case 12:
                    mText.text = "투사체 크기 +30%";
                    break;
                case 13:
                    mText.text = "근접 공격 범위 +30%";
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (mID)
            {
                case 0:
                    mText.text = "Restore full HP";
                    break;
                case 1:
                    mText.text = "MaxHP +1";
                    break;
                case 2:
                    mText.text = "Revive token +1";
                    break;
                case 3:
                    mText.text = "Gold +100";
                    break;
                case 4:
                    mText.text = "Atk +30%";
                    break;
                case 5:
                    mText.text = "Def +30%";
                    break;
                case 6:
                    mText.text = "Attack speed +30%";
                    break;
                case 7:
                    mText.text = "Critical chance +30%";
                    break;
                case 8:
                    mText.text = "Critical damage +30%";
                    break;
                case 9:
                    mText.text = "CC reduce +30%";
                    break;
                case 10:
                    mText.text = "Cooltime reduce +30%";
                    break;
                case 11:
                    mText.text = "Additional bullet +1";
                    break;
                case 12:
                    mText.text = "Bullet size +30%";
                    break;
                case 13:
                    mText.text = "Melee attack range +30%";
                    break;
                default:
                    break;
            }
        }
    }

    public void Buff()
    {
        SoundController.Instance.SESoundUI(1);
        BuffSelectController.Instance.mBuffList.Remove(mID);
        switch (mID)
        {
            case 0:
                Player.Instance.Heal(Player.Instance.mMaxHP);
                break;
            case 1:
                Player.Instance.mMaxHP += 1;
                Player.Instance.Heal(1);
                break;
            case 2:
                PassiveArtifacts.Instance.ReviveCount += 1;
                break;
            case 3:
                Player.Instance.mStats.Gold += 100;
                UIController.Instance.ShowGold();
                break;
            case 4:
                Player.Instance.mStats.Atk *= 1.3f;
                break;
            case 5:
                Player.Instance.mStats.Def *= 1.3f;
                break;
            case 6:
                Player.Instance.AttackSpeedStat *= 1.3f;
                break;
            case 7:
                if (Player.Instance.mStats.Crit * 1.3f > 1)
                {
                    Player.Instance.mStats.Crit = 1f;
                }
                else
                {
                    Player.Instance.mStats.Crit *= 1.3f;
                }
                break;
            case 8:
                if (Player.Instance.mStats.CritDamage * 1.3f > 2)
                {
                    Player.Instance.mStats.CritDamage = 2f;
                }
                else
                {
                    Player.Instance.mStats.CritDamage *= 1.3f;
                }
                break;
            case 9:
                if (Player.Instance.mStats.CCReduce * 1.3f > 0.5f)
                {
                    Player.Instance.mStats.CCReduce = 0.5f;
                }
                else
                {
                    Player.Instance.mStats.CCReduce *= 1.3f;
                }
                break;
            case 10:
                if (Player.Instance.mStats.CooltimeReduce * 1.3f > 0.5f)
                {
                    Player.Instance.mStats.CooltimeReduce = 0.5f;
                }
                else
                {
                    Player.Instance.mStats.CooltimeReduce *= 1.3f;
                }
                break;
            case 11:
                PassiveArtifacts.Instance.AdditionalBullet += 1;
                break;
            case 12:
                PassiveArtifacts.Instance.AdditionalBulletSize += 0.3f;
                break;
            case 13:
                PassiveArtifacts.Instance.AdditionalMeleeRangeSize += 0.3f; Player.Instance.NowPlayerWeapon.WeaponRangeSize();
                break;
            default:
                break;
        }
    }

    public void NextMapInMode()
    {
        Buff();
        GameSetting.Instance.NowStage += 1;
        GameController.Instance.StageLevel = 1;
        BuffSelectController.Instance.mWindow.gameObject.SetActive(false);
        UIController.Instance.StartCoroutine(UIController.Instance.SceneMoveShadow());
        SceneManager.LoadScene(2);
        Player.Instance.transform.position = new Vector2(0, 0);
        Player.Instance.StageCheck();
        UIController.Instance.SetMapText();
        UIController.Instance.StartCoroutine(UIController.Instance.ShowLevel());
        GameController.Instance.SetActiveArtifacts();
        GameController.Instance.SetWeapon();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        NextMapInMode();
    }
}
