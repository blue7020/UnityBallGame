using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItem : InformationLoader
{
    [SerializeField]
    public int mID;

    [SerializeField]
    public SpriteRenderer mRenderer;

    [SerializeField]
    public ItemStat[] mInfoArr;

    public ItemStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    public bool IsShopItem;

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.ITEM_STAT);
        IsShopItem = false;
    }

    public void UseItem()
    {
        Debug.Log("아이템사용");
        if (mInfoArr[mID].Heal > 0)
        {
            if (mID==0|| mID == 1)
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
        UIController.Instance.ShowItemImage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsShopItem == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Player.Instance.NowItem == null)
                {
                    //TODO 플레이어가 현재 가진 아이템의 게임 오브젝트와 이 오브젝트의 능력치를 교체
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
}
