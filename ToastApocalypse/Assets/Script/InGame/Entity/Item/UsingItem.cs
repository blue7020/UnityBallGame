using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItem : InformationLoader
{
    public int mID;

    public SpriteRenderer mRenderer;

    public Room Currentroom;
    public Vector3 backupPos;

    public ItemStat mStats;
    private bool DropCool;


    public bool IsShopItem;


    private void Awake()
    {
        mStats = ItemList.Instance.mInfoArr[mID];
        DropCool = false;
    }

    public void UseItem()
    {
        if (mStats.Heal > 0)
        {
            if (mID==0|| mID == 1)
            {
                Player.Instance.mMaxHP += mStats.Heal;
            }
            Player.Instance.Heal(mStats.Heal);
        }
        if (mStats.Atk > 0)
        {
            StartCoroutine(Player.Instance.Atk(Player.Instance.mStats.Atk - (Player.Instance.mStats.Atk * (1 + mStats.Atk)), mStats.Duration));
        }
        if (mStats.AtkSpd > 0)
        {
            StartCoroutine(Player.Instance.AtkSpeed(Player.Instance.mStats.AtkSpd-(Player.Instance.mStats.AtkSpd * (1 + mStats.AtkSpd)), mStats.Duration));
        }
        if (mStats.Spd > 0)
        {
            StartCoroutine(Player.Instance.Speed(Player.Instance.mStats.Spd - (Player.Instance.mStats.Spd * (1 + mStats.Spd)), mStats.Duration));
        }
        if (mStats.Def > 0)
        {
            StartCoroutine(Player.Instance.Def(Player.Instance.mStats.Def - (Player.Instance.mStats.Def * (1 + mStats.Def)), mStats.Duration));
        }
        Player.Instance.NowItem = null;
        UIController.Instance.ShowItemImage();
    }

    public void ItemChange()
    {
        if (Player.Instance.NowItem == null)
        {
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.position = Vector3.zero;
            Player.Instance.NowItem = this;
            mRenderer.color = Color.clear;

        }
        else
        {
            UsingItem drop = Player.Instance.NowItem;
            drop.Clamp();
            Player.Instance.NowItem = null;
            drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
            drop.mRenderer.color = Color.white;
            drop.gameObject.transform.position = Player.Instance.gameObject.transform.position;
            if (IsShopItem==false)
            {
                StartCoroutine(drop.DropCooltime());
            }
            Player.Instance.NowItem = this;
            gameObject.transform.SetParent(Player.Instance.gameObject.transform);
            mRenderer.color = Color.clear;
        }
        UIController.Instance.ShowItemImage();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsShopItem == false)
            {
                if (DropCool==false)
                {
                    ItemChange();
                }
            }
        }

    }


    public void Clamp()
    {
        Currentroom = Player.Instance.CurrentRoom;
        int RoomXMax = Currentroom.Width, RoomXMin = -Currentroom.Width;
        int RoomYMax = Currentroom.Height, RoomYMin = -Currentroom.Height;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, RoomXMax - 1, RoomXMin - 1), Mathf.Clamp(transform.position.y, RoomYMax - 1, RoomYMin - 1), 0);

    }

    private IEnumerator DropCooltime()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        DropCool = true;
        yield return delay;
        DropCool = false;
    }
}
