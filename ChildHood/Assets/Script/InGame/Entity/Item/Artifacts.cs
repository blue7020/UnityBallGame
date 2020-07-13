using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : InformationLoader
{
    public int mID;
    public eArtifactType mType;

    [SerializeField]
    public SpriteRenderer mRenderer;

    [SerializeField]
    public Room Currentroom;
    public Vector3 backupPos;

    public ArtifactStat mStats;
    public ArtifactTextStat TextStats;

    public bool Equip;
    public bool Cool;

    public bool IsShopItem;

    private void Start()
    {
        mStats = ArtifactController.Instance.mInfoArr[mID];
        TextStats = ArtifactController.Instance.mStatInfoArr[mID];
        IsShopItem = false;
        Equip = false;
        Cool = false;
    }

    public void UseArtifact()
    {
        if (mType == eArtifactType.Use)
        {
            if (Cool == false)
            {
                float realCoolDown = mStats.Skill_Cooltime * (1 + Player.Instance.mStats.CooltimeReduce / 100);
                Debug.Log("유물 사용");
                StartCoroutine(Cooldown(realCoolDown));
                //TODO 델리게이트를 사용해 추가 효과 부여
                //메서드 클래스 생성
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
            Player.Instance.EquipArtifact(this);
        }
    }

    public void UnequipArtifact()
    {
        if (Equip == true)
        {
            Player.Instance.UnequipArtifact(this);
        }
    }



    public void ItemChange()
    { 
        if (mType == eArtifactType.Passive)
        {

            if (InventoryController.Instance.nowIndex < InventoryController.Instance.mSlotArr.Length)
            {
                gameObject.transform.SetParent(Player.Instance.gameObject.transform);
                InventoryController.Instance.Additem(this, InventoryController.Instance.nowIndex);
                transform.position = Vector3.zero;
                mRenderer.color = Color.clear;
                InventoryController.Instance.nowIndex++;
                EquipArtifact();
            }
        }
        if (Player.Instance.NowUsingArtifact == null && mType == eArtifactType.Use)
        {
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.position = Vector3.zero;
            Player.Instance.NowUsingArtifact = this;
            Player.Instance.UseItemInventory = this;
            UIController.Instance.ShowArtifactImage();
            mRenderer.color = Color.clear;

        }

        Artifacts drop = Player.Instance.NowUsingArtifact;
        if (drop!=null)
        {
            if (mType == eArtifactType.Use && drop.mType == eArtifactType.Use)
            {
                drop.UnequipArtifact();
                Clamp();
                Player.Instance.NowUsingArtifact = null;
                drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
                int randx = UnityEngine.Random.Range(-1, 1);
                int randy = UnityEngine.Random.Range(-1, 1);
                drop.mRenderer.color = Color.white;
                drop.gameObject.transform.position = Player.Instance.gameObject.transform.position + new Vector3(randx, randy, 0);
                Player.Instance.NowUsingArtifact = this;
                Player.Instance.UseItemInventory = this;
                UIController.Instance.mUsingArtifactImage.sprite = Player.Instance.NowUsingArtifact.mRenderer.sprite;
                UIController.Instance.ShowArtifactImage();
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
            if (IsShopItem == false)
            {
                ItemChange();
            }
        }
    }


    public void Clamp()
    {
        Currentroom = Player.Instance.CurrentRoom;
        int RoomXMax = Currentroom.Width - 1, RoomXMin = -Currentroom.Width + 1;
        int RoomYMax = Currentroom.Height - 1, RoomYMin = -Currentroom.Height + 1;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, RoomXMax, RoomXMin), Mathf.Clamp(transform.position.y, RoomYMax, RoomYMin), 0);

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (mType == eArtifactType.Use)
        {
            if (other.gameObject.CompareTag("Walls"))
            {
                if (Currentroom != null&&Equip==false)
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
