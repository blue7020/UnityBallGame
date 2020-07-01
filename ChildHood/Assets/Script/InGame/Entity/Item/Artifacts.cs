using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : InformationLoader
{
    [SerializeField]
    public int mID;
    [SerializeField]
    public eArtifactType Type;

    public Rigidbody2D mRB2D;
    [SerializeField]
    public SpriteRenderer mRenderer;

    [SerializeField]
    public Room Currentroom;
    public Vector3 backupPos;
    [SerializeField]
    public Artifact[] mInfoArr;
    public bool Equip;
    public bool Cool;

    public Artifact[] GetInfoArr()
    {
        return mInfoArr;
    }

    public bool IsShopItem;
    private bool PosSet;

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.ARTIFACT_STAT);
        mRB2D = GetComponent<Rigidbody2D>();
        IsShopItem = false;
        PosSet = false;
        Equip = false;
        Cool = false;
    }

    public void UseArtifact()
    {
        if (Type == eArtifactType.Use)
        {
            if (Cool == false)
            {
                float realCoolDown = mInfoArr[mID].Skill_Cooltime * (1 + Player.Instance.mInfoArr[Player.Instance.mID].CooltimeReduce / 100);
                Debug.Log("유물 사용");
                StartCoroutine(Cooldown(realCoolDown));
                //TODO 델리게이트를 사용해 추가 효과 부여
                //메서드 클래스
            }

        }

    }

    public IEnumerator Cooldown(float realCooldown)
    {    
        WaitForSeconds cool = new WaitForSeconds(realCooldown);
        Cool = true;
        yield return cool;
        Cool = false;
    }

    public void EquipArtifact()
    {
        if (Equip == false)
        {
            Equip = true;
            Player.Instance.mMaxHP += mInfoArr[mID].Hp;
            Player.Instance.mInfoArr[Player.Instance.mID].Atk += mInfoArr[mID].Atk;
            Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd -= mInfoArr[mID].AtkSpd;
            Player.Instance.mInfoArr[Player.Instance.mID].Spd += mInfoArr[mID].Spd;
            Player.Instance.mInfoArr[Player.Instance.mID].Def += mInfoArr[mID].Def;
            Player.Instance.mInfoArr[Player.Instance.mID].Crit += mInfoArr[mID].Crit;
            Player.Instance.mInfoArr[Player.Instance.mID].CritDamage += mInfoArr[mID].CritDamage;
            Player.Instance.mInfoArr[Player.Instance.mID].CCReduce += mInfoArr[mID].CCReduce;
            Player.Instance.mInfoArr[Player.Instance.mID].CooltimeReduce += mInfoArr[mID].CooltimeReduce;
            if (Type == eArtifactType.Use)
            {
                Player.Instance.NowUsingArtifact = this;
                UIController.Instance.ShowItemImage();
            }

        }
    }

    public void ClearArtifact()
    {
        if (Equip == true)
        {
            Player.Instance.mMaxHP += mInfoArr[mID].Hp;
            Player.Instance.mInfoArr[Player.Instance.mID].Atk -= mInfoArr[mID].Atk;
            Player.Instance.mInfoArr[Player.Instance.mID].AtkSpd += mInfoArr[mID].AtkSpd;
            Player.Instance.mInfoArr[Player.Instance.mID].Spd -= mInfoArr[mID].Spd;
            Player.Instance.mInfoArr[Player.Instance.mID].Def -= mInfoArr[mID].Def;
            Player.Instance.mInfoArr[Player.Instance.mID].Crit -= mInfoArr[mID].Crit;
            Player.Instance.mInfoArr[Player.Instance.mID].CritDamage -= mInfoArr[mID].CritDamage;
            Player.Instance.mInfoArr[Player.Instance.mID].CCReduce -= mInfoArr[mID].CCReduce;
            Player.Instance.mInfoArr[Player.Instance.mID].CooltimeReduce -= mInfoArr[mID].CooltimeReduce;
            Equip = false;
        }
    }



    public void ItemChange()
    { 
        Artifacts drop = Player.Instance.NowUsingArtifact;
        if (Type == eArtifactType.Passive)
        {
            for (int i = 0; i < Player.Instance.Inventory.Length; i++)
            {
                if (Player.Instance.Inventory[i] == null || Player.Instance.Inventory[i].mID < 0)
                {
                    Equip = true;
                    transform.SetParent(Player.Instance.gameObject.transform);
                    transform.position = Vector3.zero;
                    Player.Instance.Inventory[i] = this;
                    Debug.Log(mRenderer.sprite);
                    InventoryController.Instance.mSlotArr[i].SetSprite(mRenderer.sprite);
                    mRenderer.color = Color.clear;
                    break;
                }
            }
        }
        if (Player.Instance.NowUsingArtifact == null && Type == eArtifactType.Use)
        {
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.position = Vector3.zero;
            Player.Instance.NowUsingArtifact = this;
            Player.Instance.UseItemInventory = this;
            UIController.Instance.mUsingArtifactImage.sprite = mRenderer.sprite;
            mRenderer.color = Color.clear;

        }


        if (drop!=null)
        {
            if (Type == eArtifactType.Use && drop.Type == eArtifactType.Use)
            {
                Clamp();
                Player.Instance.NowUsingArtifact = null;
                drop.ClearArtifact();
                drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
                int randx = UnityEngine.Random.Range(-1, 1);
                int randy = UnityEngine.Random.Range(-1, 1);
                drop.mRenderer.color = Color.white;
                drop.gameObject.transform.position = Player.Instance.gameObject.transform.position + new Vector3(randx, randy, 0);
                Player.Instance.NowUsingArtifact = this;
                Player.Instance.UseItemInventory = this;
                UIController.Instance.mUsingArtifactImage.sprite = mRenderer.sprite;
                gameObject.transform.SetParent(Player.Instance.gameObject.transform);
                mRenderer.color = Color.clear;
                EquipArtifact();
            }
        }

        UIController.Instance.ShowArtifactImage();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemChange();
        }
    }


    public void Clamp()
    {
        backupPos = transform.position;
        Currentroom = Player.Instance.CurrentRoom;
        int RoomXMax = Currentroom.Width - 1, RoomXMin = -Currentroom.Width + 1;
        int RoomYMax = Currentroom.Height - 1, RoomYMin = -Currentroom.Height + 1;
        mRB2D.position = new Vector3(Mathf.Clamp(mRB2D.position.x, RoomXMax, RoomXMin), Mathf.Clamp(mRB2D.position.y, RoomYMax, RoomYMin), 0);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Type == eArtifactType.Use)
        {
            transform.position = backupPos;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Type == eArtifactType.Use)
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
}
