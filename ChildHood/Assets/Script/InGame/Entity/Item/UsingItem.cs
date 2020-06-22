using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItem : InformationLoader
{
    [SerializeField]
    public int mID;

    [SerializeField]
    public SpriteRenderer mRenderer;
    private Sprite[] mItemSpriteArr;

    [SerializeField]
    public ItemStat[] mInfoArr;

    public ItemStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.ITEM_STAT);
        int rand = Random.Range(1, mItemSpriteArr.Length);
        switch (rand)
        {
            case 1:
                mID = 1;
                mRenderer.sprite = mItemSpriteArr[1];
                break;
            case 2:
                mID = 2;
                mRenderer.sprite = mItemSpriteArr[2];
                break;
            case 3:
                mID = 3;
                mRenderer.sprite = mItemSpriteArr[3];
                break;
            case 4:
                mID = 4;
                mRenderer.sprite = mItemSpriteArr[4];
                break;
            case 5:
                mID = 5;
                mRenderer.sprite = mItemSpriteArr[5];
                break;
            default:
                Debug.LogError("Wrong StatueType");
                break;
        }
    }

    public void UseItem()
    {
        if (mInfoArr[mID].Heal > 0)
        {
            if (mID==1|| mID == 2)
            {
                Player.Instance.mMaxHP += mInfoArr[mID].Heal;
            }
            Player.Instance.Heal(mInfoArr[mID].Heal);
        }
        if (mInfoArr[mID].Atk > 0)
        {
            StartCoroutine(Player.Instance.Atk(mInfoArr[mID].Atk, mInfoArr[mID].Duration));
        }
        if (mInfoArr[mID].AtkSpd > 0)
        {
            StartCoroutine(Player.Instance.Atk(mInfoArr[mID].AtkSpd, mInfoArr[mID].Duration));
        }
        if (mInfoArr[mID].Spd > 0)
        {
            StartCoroutine(Player.Instance.Atk(mInfoArr[mID].Spd, mInfoArr[mID].Duration));
        }
        if (mInfoArr[mID].Def > 0)
        {
            StartCoroutine(Player.Instance.Atk(mInfoArr[mID].Def, mInfoArr[mID].Duration));
        }

        Player.Instance.NowItem = null;
        UIController.Instance.NowItemImage = UIController.Instance.DefaultItemImage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.NowItem == null)
            {
                //플레이어가 현재 가진 아이템의 게임 오브젝트와 이 오브젝트의 능력치를 교체
                //땅에 있는 오브젝트는 이 오브젝트가 되고 현재 가진 아이템이 원래 땅에 있던 오브젝트가 됨
            }
            else
            {
                Player.Instance.NowItem = this;
                gameObject.SetActive(false);
            }
            
        }
    }
}
