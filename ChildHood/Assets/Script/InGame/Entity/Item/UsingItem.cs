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
        Player.Instance.NowItem.gameObject.SetActive(false);
        Player.Instance.NowItem = null;

        
        UIController.Instance.ShowItemImage();
    }

    public void ItemChange()
    {

        if (Player.Instance.NowItem == null)
        {
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
            Player.Instance.NowItem = this;
        }
        else //TODO 게임을 플레이하면서 비활성화된 아이템이 쌓이니까 처리할 방법 모색(destroy?)
        {
            UsingItem drop = Player.Instance.NowItem;
            Player.Instance.NowItem = null;
            drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
            //TODO 현재 아이템을 드롭하면 고정좌표에 드롭하기 때문에 아이템이 사라질 수 있음, 더 나은 방법이 없는지 모색
            drop.gameObject.transform.position = Player.Instance.gameObject.transform.position + new Vector3(0, -1, 0);
            drop.gameObject.SetActive(true);
            Player.Instance.NowItem = this;
            gameObject.transform.SetParent(Player.Instance.gameObject.transform);
            gameObject.SetActive(false);
        }
        UIController.Instance.ShowItemImage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemChange();
    }
}
