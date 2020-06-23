using System.Collections;
using UnityEngine;

public class Statue : InformationLoader
{
    [SerializeField]
    private float mHealAmount;

    [SerializeField]
    public SpriteRenderer mRenderer;
    [SerializeField]
    public Sprite[] mSprites;

    [SerializeField]
    private int mID;
    [SerializeField]
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
        //IsCoolTime = false;
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
                Player.Instance.IsBuff = true;
                switch (Type)
                {
                    case eStatueType.Heal:
                        Player.Instance.Heal(mHealAmount);
                        mRenderer.sprite = mSprites[1];
                        break;
                    case eStatueType.Strength:
                        StartCoroutine(Player.Instance.Atk(mInfoArr[mID].Atk,mInfoArr[mID].Cooltime));
                        mRenderer.sprite = mSprites[3];
                        break;
                    case eStatueType.Speed:
                        StartCoroutine(Player.Instance.Atk(mInfoArr[mID].Spd, mInfoArr[mID].Cooltime));
                        StartCoroutine(Player.Instance.Atk(mInfoArr[mID].AtkSpd, mInfoArr[mID].Cooltime));
                        mRenderer.sprite = mSprites[5];
                        break;
                    case eStatueType.Def:
                        StartCoroutine(Player.Instance.Atk(mInfoArr[mID].Def, mInfoArr[mID].Cooltime));
                        mRenderer.sprite = mSprites[7];
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
