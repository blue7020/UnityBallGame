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
                StartCoroutine(SkillCool());
            }

        }

    }

    private IEnumerator SkillCool()
    {
        WaitForSeconds Cool = new WaitForSeconds(mStats.Skill_Cooltime);
        IsArtifactCool = true;
        ActiveArtifacts.Instance.ArtifactsFuntion(mID);
        StartCoroutine(CooltimeRoutine());
        yield return Cool;
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
                IsArtifactCool = false;
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
                IsArtifactCool = false;
                TutorialUIController.Instance.ArtifactCoolWheel.gameObject.SetActive(false);
            }
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
            SoundController.Instance.SESoundUI(7);
            if (eType == eArtifactType.Active)
            {
                for (int i = 0; i < ArtifactController.Instance.mActiveArtifact.Count; i++)
                {
                    if (ArtifactController.Instance.mActiveArtifact[i] == this)
                    {
                        ArtifactController.Instance.mActiveArtifact.RemoveAt(i);
                    }
                }
            }
            else
            {
                PassiveArtifacts.Instance.ArtifactsFuntion(mID);
            }
        }
    }

    public void UnequipArtifact()
    {
        if (Equip == true)
        {
            Player.Instance.UnequipArtifact(this);
            if (eType == eArtifactType.Passive)
            {
                ArtifactController.Instance.mPassiveArtifact.Add(this);
            }
            else
            {
                ArtifactController.Instance.mActiveArtifact.Add(this);
            }
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
            ArtifactController.Instance.mPassiveArtifact.Add(this);
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
                ArtifactController.Instance.mPassiveArtifact.Remove(drop);
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
