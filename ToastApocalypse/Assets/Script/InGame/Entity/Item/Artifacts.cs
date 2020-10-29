using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : InformationLoader
{
    public int mID;
    public eArtifactType eType;

    public SpriteRenderer mRenderer;
    public string text;

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
        if (eType == eArtifactType.Active)
        {
            if (IsArtifactCool == false)
            {
                SoundController.Instance.SESoundUI(5);
                SkillCool();
            }

        }

    }

    private void SkillCool()
    {
        IsArtifactCool = true;
        ActiveArtifacts.Instance.ArtifactsFuntion(mID);
        StartCoroutine(CooltimeRoutine());
    }
    public void ShowCooltime(float maxTime, float currentTime)
    {
        if (GameController.Instance.IsTutorial==false)
        {
            if (currentTime > 0)
            {
                UIController.Instance.ArtifactCoolWheel.gameObject.SetActive(true);
                UIController.Instance.ArtifactCoolWheel.fillAmount = currentTime / maxTime;
            }
            else
            {
                UIController.Instance.ArtifactCoolWheel.gameObject.SetActive(false);
            }
        }
        else
        {
            if (currentTime > 0)
            {
                TutorialUIController.Instance.ArtifactCoolWheel.gameObject.SetActive(true);
                TutorialUIController.Instance.ArtifactCoolWheel.fillAmount = currentTime / maxTime;
            }
            else
            {
                TutorialUIController.Instance.ArtifactCoolWheel.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator CooltimeRoutine()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float maxTime = mStats.Skill_Cooltime - (mStats.Skill_Cooltime * Player.Instance.mStats.CooltimeReduce);
        float CoolTime = maxTime;
        float AttackCurrentTime = CoolTime;
        if (GameController.Instance.IsTutorial==true)
        {
            TutorialUIController.Instance.mArtifactButton.interactable = false;
        }
        else
        {
            UIController.Instance.mArtifactButton.interactable = false;
        }
        while (AttackCurrentTime >= 0)
        {
            yield return frame;
            AttackCurrentTime -= Time.fixedDeltaTime;
            ShowCooltime(CoolTime, AttackCurrentTime);
        }
        if (GameController.Instance.IsTutorial == true)
        {
            TutorialUIController.Instance.mArtifactButton.interactable = true;
        }
        else
        {
            UIController.Instance.mArtifactButton.interactable = true;
        }
        IsArtifactCool = false;
    }

    public void EquipArtifact()
    {
        if (Equip == false)
        {
            Player.Instance.EquipArtifact(this);
            SoundController.Instance.SESoundUI(7);
            if (SaveDataController.Instance.mUser.ArtifactFound[mID]==false)
            {
                SaveDataController.Instance.mUser.ArtifactFound[mID] = true;
            }
            if (GameController.Instance.IsTutorial==true)
            {
                TutorialUIController.Instance.ShowGetArtifact(TextStats);
            }
            else
            {
                UIController.Instance.ShowGetArtifact(TextStats);
            }
        }
    }

    public void UnequipArtifact()
    {
        if (Equip == true)
        {
            Player.Instance.UnequipArtifact(this);
        }
    }



    public void ArtifactChange()
    {
        if (eType == eArtifactType.Passive)
        {
            if (InventoryController.Instance.Full == false)
            {
                gameObject.transform.SetParent(Player.Instance.gameObject.transform);
                transform.position = Vector3.zero;
                gameObject.SetActive(false);
                InventoryController.Instance.Additem(InventoryController.Instance.nowIndex, this);
                InventoryController.Instance.nowIndex++;
                EquipArtifact();
            }
            else
            {
                if (GameSetting.Instance.Language == 0)
                {
                    text = "인벤토리에 빈 공간이 없습니다!";
                }
                else
                {
                    text = "Inventory is full!";
                }
                TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(text);
            }
        }
        if (Player.Instance.NowActiveArtifact == null && eType == eArtifactType.Active)
        {
            transform.SetParent(Player.Instance.gameObject.transform);
            transform.position = Vector3.zero;
            Player.Instance.NowActiveArtifact = this;
            Player.Instance.UseItemInventory = this;
            if (GameController.Instance.IsTutorial == false)
            {
                UIController.Instance.ShowArtifactImage();
            }
            else
            {
                TutorialUIController.Instance.ShowArtifactImage();
            }

            mRenderer.color = Color.clear;

        }

        Artifacts drop = Player.Instance.NowActiveArtifact;
        if (drop != null)
        {
            if (eType == eArtifactType.Active && drop.eType == eArtifactType.Active)
            {
                drop.UnequipArtifact();
                drop.Clamp();
                Player.Instance.NowActiveArtifact = null;
                drop.gameObject.transform.SetParent(Player.Instance.CurrentRoom.transform);
                drop.mRenderer.color = Color.white;
                drop.gameObject.transform.position = Player.Instance.gameObject.transform.position;
                if (IsShopItem == false)
                {
                    StartCoroutine(drop.DropCooltime());
                }
                Player.Instance.NowActiveArtifact = this;
                Player.Instance.UseItemInventory = this;
                if (GameController.Instance.IsTutorial == false)
                {
                    UIController.Instance.mUsingArtifactImage.sprite = Player.Instance.NowActiveArtifact.mRenderer.sprite;
                }
                else
                {
                    TutorialUIController.Instance.mUsingArtifactImage.sprite = Player.Instance.NowActiveArtifact.mRenderer.sprite;
                }
              
                gameObject.transform.SetParent(Player.Instance.gameObject.transform);
                mRenderer.color = Color.clear;
                EquipArtifact();
            }
        }
        if (GameController.Instance.IsTutorial==false)
        {
            UIController.Instance.ShowArtifactImage();
        }
        else
        {
            TutorialUIController.Instance.ShowArtifactImage();
        }

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
                    ArtifactChange();
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

}
