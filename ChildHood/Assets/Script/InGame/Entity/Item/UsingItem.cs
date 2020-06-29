using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItem : InformationLoader
{
    [SerializeField]
    public int mID;

    private Rigidbody2D mRB2D;
    [SerializeField]
    public SpriteRenderer mRenderer;

    [SerializeField]
    public ItemStat[] mInfoArr;

    public ItemStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    public bool IsShopItem;
    private bool PosSet;

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.ITEM_STAT);
        mRB2D = GetComponent<Rigidbody2D>();
        IsShopItem = false;
        PosSet = false;
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
            ItemChange();
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Walls"))
        {
            Room Currentroom = Player.Instance.CurrentRoom;
            int RoomXMax = Currentroom.Width, RoomXMin = -Currentroom.Width;
            int RoomYMax = Currentroom.Height, RoomYMin = -Currentroom.Height;
            mRB2D.position = new Vector3(Mathf.Clamp(mRB2D.position.x, RoomXMax, RoomXMin), Mathf.Clamp(mRB2D.position.y, RoomYMax, RoomYMin), 0);
            GameObject Target = other.gameObject;
            switch (Target.GetComponent<WallDir>().Type)
            {
                case eWallType.Top:
                    int rand = UnityEngine.Random.Range(0, 5);
                    switch (rand)
                    {
                        case 0:
                            gameObject.transform.position += new Vector3(0, -2, 0);
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
                            gameObject.transform.position += new Vector3(0, 2, 0);
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
                            gameObject.transform.position += new Vector3(-2, 0, 0);
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
                            gameObject.transform.position += new Vector3(2, 0, 0);
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
