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
    private SkillEffect effect;


    public bool IsShopItem;


    private void Awake()
    {
        mStats = ItemList.Instance.mInfoArr[mID];
        DropCool = false;
    }
    //아이템의 buff코드
    //30=무적 31=공격력 32=방어력 33=공격속도 34=이동속도
    public void UseItem()
    {
        if (mStats.Heal > 0)
        {
            if (mID==0|| mID == 1)
            {
                Player.Instance.mMaxHP += mStats.Heal;
            }
            effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            effect.SetEffect(BuffEffectController.Instance.mSprite[0], 0.5f,0,Color.green);
            BuffEffectController.Instance.EffectList.Add(effect);
            Player.Instance.Heal(mStats.Heal);
        }
        if (mStats.Atk > 0)
        {
            effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            effect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0,Color.red);
            BuffEffectController.Instance.EffectList.Add(effect);
            StartCoroutine(Player.Instance.Atk(mStats.Atk,31, mStats.Duration));
        }
        if (mStats.AtkSpd > 0)
        {
            effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            effect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0,Color.yellow);
            BuffEffectController.Instance.EffectList.Add(effect);
            StartCoroutine(Player.Instance.AtkSpeed(mStats.AtkSpd,33, mStats.Duration));
        }
        if (mStats.Spd > 0)
        {
            effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            effect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0,Color.cyan);
            BuffEffectController.Instance.EffectList.Add(effect);
            StartCoroutine(Player.Instance.Speed(mStats.Spd,34, mStats.Duration));
        }
        if (mStats.Def > 0)
        {
            effect = Instantiate(BuffEffectController.Instance.mEffect, Player.Instance.transform);
            effect.SetEffect(BuffEffectController.Instance.mSprite[0], mStats.Duration,0,Color.blue);
            BuffEffectController.Instance.EffectList.Add(effect);
            StartCoroutine(Player.Instance.Def(mStats.Def,32, mStats.Duration));
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
