using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    public SpriteRenderer mRenderer;

    public int mID;
    public int mStatueID;
    public StatueStat mStat;

    public float SpendGold;
    public Text mPriceText;

    public eStatueType eType;
    public eStatuePay ePayType;
    private bool IsUse;

    private void Awake()
    {
        IsUse = false;
    }

    public void StatSetting(int id)
    {
        mStatueID = id;
        switch (mStatueID)
        {
            case 0:
                eType = eStatueType.Heal;
                mRenderer.sprite = StatueController.Instance.mStageSprites[0];
                break;
            case 1:
                eType = eStatueType.Strength;
                mRenderer.sprite = StatueController.Instance.mStageSprites[2];
                break;
            case 2:
                eType = eStatueType.Speed;
                mRenderer.sprite = StatueController.Instance.mStageSprites[4];
                break;
            case 3:
                eType = eStatueType.Def;
                mRenderer.sprite = StatueController.Instance.mStageSprites[6];
                break;
            case 4:
                eType = eStatueType.Gold;
                mRenderer.sprite = StatueController.Instance.mStageSprites[8];
                break;
            case 5:
                eType = eStatueType.War;
                mRenderer.sprite = StatueController.Instance.mStageSprites[10];
                break;
            case 6:
                eType = eStatueType.Heart;
                mRenderer.sprite = StatueController.Instance.mStageSprites[12];
                break;
            case 7:
                eType = eStatueType.Harvest;
                mRenderer.sprite = StatueController.Instance.mStageSprites[14];
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
        mStat = StatueController.Instance.mStatInfoArr[mStatueID];
        mPriceText.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        mPriceText.transform.localScale = new Vector3(10, 10, 0);
    }

    private void StatueUse()
    {
        switch (eType)
        {
            case eStatueType.Heal:
                Player.Instance.Heal(mStat.Hp);
                mRenderer.sprite = StatueController.Instance.mStageSprites[1];
                break;
            case eStatueType.Strength:
                StartCoroutine(Player.Instance.Atk(Player.Instance.mStats.Atk - (Player.Instance.mStats.Atk * (1 + -mStat.Atk)), mStat.Duration));
                mRenderer.sprite = StatueController.Instance.mStageSprites[3];
                break;
            case eStatueType.Speed:
                StartCoroutine(Player.Instance.Speed(Player.Instance.mStats.Spd - (Player.Instance.mStats.Spd * (1 + -mStat.Spd)), mStat.Duration));
                StartCoroutine(Player.Instance.AtkSpeed(Player.Instance.mStats.AtkSpd - (Player.Instance.mStats.AtkSpd * (1 + mStat.AtkSpd)), mStat.Duration));
                mRenderer.sprite = StatueController.Instance.mStageSprites[5];
                break;
            case eStatueType.Def:
                StartCoroutine(Player.Instance.Def(Player.Instance.mStats.Def - (Player.Instance.mStats.Def * (1 + -mStat.Def)), mStat.Duration));
                mRenderer.sprite = StatueController.Instance.mStageSprites[7];
                break;
            case eStatueType.Gold:
                Player.Instance.mGoldBonus += 0.5f;
                mRenderer.sprite = StatueController.Instance.mStageSprites[9];
                break;
            case eStatueType.War:
                Player.Instance.mStats.Crit += Player.Instance.mStats.Crit*(1 + 0.3f);
                mRenderer.sprite = StatueController.Instance.mStageSprites[11];
                break;
            case eStatueType.Heart:
                Player.Instance.Heal(Player.Instance.mMaxHP);
                mRenderer.sprite = StatueController.Instance.mStageSprites[13];
                break;
            case eStatueType.Harvest:
                Debug.Log("재료 드랍율 상승");
                mRenderer.sprite = StatueController.Instance.mStageSprites[15];
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
        IsUse = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsUse==false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (ePayType == eStatuePay.Pay)
                {
                    if (Player.Instance.mStats.Gold>= SpendGold)
                    {
                        Player.Instance.mStats.Gold-=SpendGold;
                        StatueUse();
                        UIController.Instance.ShowGold();
                        CanvasFinder.Instance.DeletdStatuePrice(mID);
                    }
                }
                else
                {
                    StatueUse();
                }
                

            }
        }
        
    }
}
