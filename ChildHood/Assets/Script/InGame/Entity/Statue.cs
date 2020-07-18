using System.Collections;
using UnityEngine;

public class Statue : InformationLoader
{
    public float mHealAmount;

    public SpriteRenderer mRenderer;
    public Sprite[] mSprites;

    public int mID;
    public StatuetStat[] mInfoArr;


    public StatuetStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    [SerializeField]
    private eStatueType Type;
    private bool IsUse;

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.STATUE_STAT);
        IsUse = false;
    }
    private void Start()
    {
        int rand = Random.Range(0, mInfoArr.Length);
        switch (rand)
        {
            case 0:
                Type = eStatueType.Heal;
                mID = 0;
                mRenderer.sprite = mSprites[0];
                break;
            case 1:
                Type = eStatueType.Strength;
                mID = 1;
                mRenderer.sprite = mSprites[2];
                break;
            case 2:
                mID = 2;
                Type = eStatueType.Speed;
                mRenderer.sprite = mSprites[4];
                break;
            case 3:
                mID = 3;
                Type = eStatueType.Def;
                mRenderer.sprite = mSprites[6];
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsUse==false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                switch (Type)
                {
                    case eStatueType.Heal:
                        Player.Instance.Heal(mHealAmount);
                        mRenderer.sprite = mSprites[1];
                        break;
                    case eStatueType.Strength:
                        Player.Instance.NowBuff.Add(StartCoroutine(Player.Instance.Atk(Player.Instance.mStats.Atk - (Player.Instance.mStats.Atk * (1 + mInfoArr[mID].Atk)), mInfoArr[mID].Duration)));
                        mRenderer.sprite = mSprites[3];
                        
                        break;
                    case eStatueType.Speed:
                        Player.Instance.NowBuff.Add(StartCoroutine(Player.Instance.Speed(Player.Instance.mStats.Spd - (Player.Instance.mStats.Spd * (1 + mInfoArr[mID].Spd)), mInfoArr[mID].Duration)));
                        Player.Instance.NowBuff.Add(StartCoroutine(Player.Instance.AtkSpeed(Player.Instance.mStats.AtkSpd - (Player.Instance.mStats.AtkSpd * (1 + mInfoArr[mID].AtkSpd)), mInfoArr[mID].Duration)));
                        mRenderer.sprite = mSprites[5];
                        break;
                    case eStatueType.Def:
                        Player.Instance.NowBuff.Add(StartCoroutine(Player.Instance.Def(Player.Instance.mStats.Def - (Player.Instance.mStats.Def * (1 + mInfoArr[mID].Def)), mInfoArr[mID].Duration)));
                        break;
                    default:
                        Debug.LogError("Wrong StatueType");
                        break;
                }
                IsUse = true;

            }
        }
        
    }
}
