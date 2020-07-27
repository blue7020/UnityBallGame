using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : InformationLoader
{
    public int mID;
    public eArtifactType mType;

    public SpriteRenderer mRenderer;

    public Room Currentroom;
    public Vector3 backupPos;

    public ArtifactStat mStats;
    public ArtifactTextStat TextStats;

    public bool Equip;
    public bool IsArtifactCool;
    private bool DropCool;

    public bool IsShopItem;

    private void Awake()
    {
        mStats = ArtifactController.Instance.mStatInfoArr[mID];
        TextStats = ArtifactController.Instance.mTextInfoArr[mID];
        Equip = false;
        IsArtifactCool = false;
        DropCool = false;
    }

    public void UseArtifact()
    {
        if (mType == eArtifactType.Use)
        {
            if (IsArtifactCool == false)
            {
                StartCoroutine(SkillCool());
            }

        }

    }

    private IEnumerator SkillCool()
    {
        WaitForSeconds Cool = new WaitForSeconds(mStats.Skill_Cooltime);
        IsArtifactCool = true;
        //TODO 델리게이트로 해당하는 ID의 스킬 가져오기
        StartCoroutine(CooltimeRoutine());
        Debug.Log("유믈 사용");
        yield return Cool;
    }
    public void ShowCooltime(float maxTime, float currentTime)
    {
        if (currentTime > 0)
        {
            UIController.Instance.ArtifactCoolWheel.gameObject.SetActive(true);
            UIController.Instance.ArtifactCoolWheel.fillAmount = currentTime / maxTime;
        }
        else
        {
            IsArtifactCool = false;
            UIController.Instance.ArtifactCoolWheel.gameObject.SetActive(false);
        }
    }

    private IEnumerator CooltimeRoutine()
    {
        float maxTime = mStats.Skill_Cooltime + (mStats.Skill_Cooltime - (mStats.Skill_Cooltime * (1 + Player.Instance.mStats.CooltimeReduce)));
        float CoolTime = maxTime;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float AttackCurrentTime = CoolTime;
        while (AttackCurrentTime >= 0)
        {
            yield return frame;
            AttackCurrentTime -= Time.fixedDeltaTime;
            ShowCooltime(CoolTime, AttackCurrentTime);
        }
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

            if (InventoryController.Instance.nowIndex <= InventoryController.Instance.mSlotArr.Length)
            {
                gameObject.transform.SetParent(Player.Instance.gameObject.transform);
                transform.position = Vector3.zero;
                gameObject.SetActive(false);
                InventoryController.Instance.Additem(InventoryController.Instance.nowIndex, this);
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
        if (drop != null)
        {
            if (mType == eArtifactType.Use && drop.mType == eArtifactType.Use)
            {
                drop.UnequipArtifact();
                drop.Clamp();
                Player.Instance.NowUsingArtifact = null;
                drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
                drop.mRenderer.color = Color.white;
                drop.gameObject.transform.position = Player.Instance.gameObject.transform.position;
                if (IsShopItem == false)
                {
                    StartCoroutine(drop.DropCooltime());
                }
                Player.Instance.NowUsingArtifact = this;
                Player.Instance.UseItemInventory = this;
                UIController.Instance.mUsingArtifactImage.sprite = Player.Instance.NowUsingArtifact.mRenderer.sprite;
                gameObject.transform.SetParent(Player.Instance.gameObject.transform);
                mRenderer.color = Color.clear;
                EquipArtifact();
            }
        }
        UIController.Instance.ShowArtifactImage();

    }

    private IEnumerator DropCooltime()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        DropCool = true;
        yield return delay;
        DropCool = false;
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
