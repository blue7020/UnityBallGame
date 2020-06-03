using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : InformationLoader
{
    private bool IsCoolTime;
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

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.STATUE_STAT);
        IsCoolTime = false;
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
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (Type)
            {
                case eStatueType.Heal:
                    if (IsCoolTime == false)
                    {
                        if (Type == eStatueType.Heal)
                        {
                            StartCoroutine(Cooltime());
                        }

                    }
                    else
                    {
                        Debug.Log("Cooltime");
                    }
                    break;
                case eStatueType.Strength:
                    StartCoroutine(Atk());
                    mRenderer.sprite = mSprites[3];
                    break;
                case eStatueType.Speed:
                    StartCoroutine(Speed());
                    mRenderer.sprite = mSprites[5];
                    break;
                default:
                    Debug.LogError("Wrong StatueType");
                    break;
            }
            
        }
    }

    private IEnumerator Cooltime()
    {
        //밸런스 조정으로 쿨타임 기능은 사라질 수도 있음
        WaitForSeconds Cool = new WaitForSeconds(mInfoArr[mID].Cooltime);
        IsCoolTime = true;
        
        switch (Type)
        {
            case eStatueType.Heal:
                Heal();
                mRenderer.sprite = mSprites[1];
                break;
            case eStatueType.Speed:
                break;
            case eStatueType.Strength:
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
        
        yield return Cool;
        IsCoolTime = false;
        mRenderer.sprite = mSprites[0];
    }

    private void Heal()
    {
        Debug.Log("healing");
        if ((Player.Instance.mCurrentHP + mHealAmount) >= Player.Instance.mMaxHP)
        {
            Player.Instance.mCurrentHP = Player.Instance.mMaxHP;
        }
        else
        {
            Player.Instance.mCurrentHP += mHealAmount;
        }
        UIController.Instance.ShowHP();
    }

    private IEnumerator Atk()
    {
        Player.Instance.mInfoArr[Player.Instance.mID].Atk += mInfoArr[mID].Atk;
        WaitForSeconds Dura = new WaitForSeconds(mInfoArr[mID].Duration);
        yield return Dura;
        Player.Instance.mInfoArr[Player.Instance.mID].Atk -= mInfoArr[mID].Atk;
    }

    private IEnumerator Speed()
    {
        Player.Instance.mInfoArr[Player.Instance.mID].Spd += mInfoArr[mID].Spd;
        Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd -= mInfoArr[mID].AtkSpd;
        WaitForSeconds Dura = new WaitForSeconds(mInfoArr[mID].Duration);
        yield return Dura;
        Player.Instance.mInfoArr[Player.Instance.mID].Spd -= mInfoArr[mID].Spd;
        Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd += mInfoArr[mID].AtkSpd;

    }
}
