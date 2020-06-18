using System.Collections;
using UnityEngine;

public class Statue : InformationLoader
{
    //private bool IsCoolTime;
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
                switch (Type)
                {
                    case eStatueType.Heal:
                        Heal();
                        mRenderer.sprite = mSprites[1];
                        break;
                    case eStatueType.Strength:
                        StartCoroutine(Atk());
                        mRenderer.sprite = mSprites[3];
                        break;
                    case eStatueType.Speed:
                        StartCoroutine(Speed());
                        mRenderer.sprite = mSprites[5];
                        break;
                    case eStatueType.Def:
                        StartCoroutine(Def());
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
    //TODO 각 버프 중 플레이어한테 이펙트 표시
    private void Heal()
    {
        if ((Player.Instance.mCurrentHP + mHealAmount) >= Player.Instance.mMaxHP)
        {
            Player.Instance.mCurrentHP = Player.Instance.mMaxHP;
        }
        else
        {
            Player.Instance.mCurrentHP += mHealAmount;//+회복량 증가 옵션
        }
        UIController.Instance.ShowHP();
    }

    private IEnumerator Atk()
    {
        Player.Instance.IsBuff = true;
        if (Player.Instance.IsBuff == true)
        {
            //TODO 애니메이션 이펙트 추가
            Player.Instance.mInfoArr[Player.Instance.mID].Atk += mInfoArr[mID].Atk;
            WaitForSeconds Dura = new WaitForSeconds(mInfoArr[mID].Duration);
            yield return Dura;
            Player.Instance.mInfoArr[Player.Instance.mID].Atk -= mInfoArr[mID].Atk;
        }
        
    }

    private IEnumerator Speed()
    {
        Player.Instance.IsBuff = true;
        if (Player.Instance.IsBuff == true)
        {
            Player.Instance.mInfoArr[Player.Instance.mID].Spd += mInfoArr[mID].Spd;
            Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd -= mInfoArr[mID].AtkSpd;
            WaitForSeconds Dura = new WaitForSeconds(mInfoArr[mID].Duration);
            yield return Dura;
            Player.Instance.mInfoArr[Player.Instance.mID].Spd -= mInfoArr[mID].Spd;
            Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd += mInfoArr[mID].AtkSpd;
            Player.Instance.IsBuff = false;
        }

    }

    private IEnumerator Def()
    {
        Player.Instance.IsBuff = true;
        if (Player.Instance.IsBuff == true)
        {
            Player.Instance.mInfoArr[Player.Instance.mID].Def += mInfoArr[mID].Def;
            WaitForSeconds Dura = new WaitForSeconds(mInfoArr[mID].Duration);
            yield return Dura;
            Player.Instance.mInfoArr[Player.Instance.mID].Def -= mInfoArr[mID].Def;
            Player.Instance.IsBuff = false;
        }
    }

    //private IEnumerator Cooltime()
    //{
    //    //밸런스 조정으로 쿨타임 기능은 사라질 수도 있음
    //    WaitForSeconds Cool = new WaitForSeconds(mInfoArr[mID].Cooltime);
    //    IsCoolTime = true;

    //    switch (Type)
    //    {
    //        case eStatueType.Heal:
    //            Heal();
    //            break;
    //        case eStatueType.Speed:
    //        case eStatueType.Strength:
    //        case eStatueType.Def:
    //            break;
    //        default:
    //            Debug.LogError("Wrong StatueType");
    //            break;
    //    }

    //    yield return Cool;
    //    IsCoolTime = false;
    //    mRenderer.sprite = mSprites[0];
    //}
}
