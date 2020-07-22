﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItem : InformationLoader
{
    public int mID;

    public SpriteRenderer mRenderer;

    public Room Currentroom;
    public Vector3 backupPos;

    public ItemStat Stats;


    public bool IsShopItem;


    private void Start()
    {
        Stats = ItemList.Instance.mInfoArr[mID];
    }

    public void UseItem()
    {
        if (Stats.Heal > 0)
        {
            if (mID==0|| mID == 1)
            {
                Player.Instance.mMaxHP += Stats.Heal;
            }
            Player.Instance.Heal(Stats.Heal);
        }
        if (Stats.Atk > 0)
        {
            Player.Instance.StartCoroutine(Player.Instance.Atk(Player.Instance.mStats.Atk - (Player.Instance.mStats.Atk * (1 + -Stats.Atk)), Stats.Duration));
        }
        if (Stats.AtkSpd > 0)
        {
            Player.Instance.StartCoroutine(Player.Instance.AtkSpeed(Player.Instance.mStats.AtkSpd - (Player.Instance.mStats.AtkSpd * (1 + Stats.AtkSpd)), Stats.Duration));
        }
        if (Stats.Spd > 0)
        {
            Player.Instance.StartCoroutine(Player.Instance.Speed(Player.Instance.mStats.Spd - (Player.Instance.mStats.Spd * (1 + -Stats.Spd)), Stats.Duration));
        }
        if (Stats.Def > 0)
        {
            Player.Instance.StartCoroutine(Player.Instance.Def(Player.Instance.mStats.Def - (Player.Instance.mStats.Def * (1 + -Stats.Def)), Stats.Duration));
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
            gameObject.SetActive(false);
            Player.Instance.NowItem = this;
        }
        else
        {
            Clamp();
            UsingItem drop = Player.Instance.NowItem;
            Player.Instance.NowItem = null;
            drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
            int randx = UnityEngine.Random.Range(-1, 1);
            int randy = UnityEngine.Random.Range(-1, 1);
            drop.gameObject.transform.position = Player.Instance.gameObject.transform.position + new Vector3(randx, randy, 0);
            drop.gameObject.SetActive(true);
            Player.Instance.NowItem = this;
            gameObject.transform.SetParent(Player.Instance.gameObject.transform);
            gameObject.SetActive(false);
        }
        UIController.Instance.ShowItemImage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsShopItem == false)
            {
                ItemChange();
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

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Walls"))
        {
            if (Currentroom != null)
            {
                switch (other.GetComponent<WallDir>().Type)
                {
                    case eWallType.Top:
                        int rand = UnityEngine.Random.Range(0, 5);
                        switch (rand)
                        {
                            case 0:
                                gameObject.transform.position += new Vector3(0, -1, 0);
                                break;
                            case 1:
                                gameObject.transform.position += new Vector3(1, 0, 0);
                                break;
                            case 2:
                                gameObject.transform.position += new Vector3(-1, 0, 0);
                                break;
                            case 3:
                                gameObject.transform.position += new Vector3(1, -1, 0);
                                break;
                            case 4:
                                gameObject.transform.position += new Vector3(-1, -1, 0);
                                break;
                        }
                        break;
                    case eWallType.Bot:
                        int rand2 = UnityEngine.Random.Range(0, 5);
                        switch (rand2)
                        {
                            case 0:
                                gameObject.transform.position += new Vector3(0, 1, 0);
                                break;
                            case 1:
                                gameObject.transform.position += new Vector3(1, 0, 0);
                                break;
                            case 2:
                                gameObject.transform.position += new Vector3(-1, 0, 0);
                                break;
                            case 3:
                                gameObject.transform.position += new Vector3(1, 1, 0);
                                break;
                            case 4:
                                gameObject.transform.position += new Vector3(-1, 1, 0);
                                break;
                        }
                        break;
                    case eWallType.Right:
                        int rand3 = UnityEngine.Random.Range(0, 5);
                        switch (rand3)
                        {
                            case 0:
                                gameObject.transform.position += new Vector3(-1, 0, 0);
                                break;
                            case 1:
                                gameObject.transform.position += new Vector3(0, -1, 0);
                                break;
                            case 2:
                                gameObject.transform.position += new Vector3(0, 1, 0);
                                break;
                            case 3:
                                gameObject.transform.position += new Vector3(-1, 1, 0);
                                break;
                            case 4:
                                gameObject.transform.position += new Vector3(-1, -1, 0);
                                break;
                        }
                        break;
                    case eWallType.Left:
                        int rand4 = UnityEngine.Random.Range(0, 5);
                        switch (rand4)
                        {
                            case 0:
                                gameObject.transform.position += new Vector3(1, 0, 0);
                                break;
                            case 1:
                                gameObject.transform.position += new Vector3(0, -1, 0);
                                break;
                            case 2:
                                gameObject.transform.position += new Vector3(0, 1, 0);
                                break;
                            case 3:
                                gameObject.transform.position += new Vector3(1, 1, 0);
                                break;
                            case 4:
                                gameObject.transform.position += new Vector3(1, -1, 0);
                                break;
                        }
                        break;
                    default:
                        Debug.LogError("Wrong Wall Type");
                        break;
                }
            }
        }
    }
}
