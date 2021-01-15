using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public Room room;
    public Sprite[] mSpt;
    public SpriteRenderer mRenderer;
    public int Spend,index;
    private string text;

    public Text mPriceText;
    private Button mUIShopButton;

    public Animator mAnim;
    public bool enable;

    private void Awake()
    {
        mUIShopButton = UIController.Instance.mShopButton;
    }

    private void Start()
    {
        enable = false;
        mPriceText = Instantiate(mPriceText, CanvasFinder.Instance.transform);
        mPriceText.text = Spend.ToString() + "G";
        mPriceText.gameObject.SetActive(false);
        CanvasFinder.Instance.mSlotPriceText = mPriceText;
        mPriceText.transform.position = transform.position + new Vector3(0, -1f, 0);
        mPriceText.transform.localScale = new Vector3(0.1f, 0.1f, 0);
    }

    private void FailRolling()
    {
        mRenderer.sprite = mSpt[1];
        if (GameSetting.Instance.Language == 0)
        {
            text = "실패...";
        }
        else
        {
            text = "Fail...";
        }
        SoundController.Instance.SESoundUI(9);
    }

    private void Rolling()
    {
        mAnim.SetBool(AnimHash.Slot, false);
        float rand = Random.Range(0, 1f);
        if (rand<0.15f)//꽝
        {
            FailRolling();
        }
        else if (rand >=0.15f&&rand<0.6f)//무기
        {
            WeaponSearch();
        }
        else if (rand >=0.6f)//유물
        {
            ArtifactSearch();
        }
        UIController.Instance.mShopSpendText.text = "-" + Spend + "G";
        Time.timeScale = 1;
        UIController.Instance.NoTouchArea.gameObject.SetActive(false);
        StartCoroutine(ResetSlot());
    }

    private IEnumerator ResetSlot()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(text);
        yield return delay;
        enable = false;
        mRenderer.sprite = mSpt[0];
    }

    private void ArtifactSearch()
    {
        float randArtifactType = Random.Range(0, 1f);
        if (randArtifactType>0.5f)//패시브
        {
            if (GameController.Instance.mPassiveArtifactList.Count > 0)
            {
                int rand = Random.Range(0, GameController.Instance.mPassiveArtifactList.Count);
                Artifacts art = Instantiate(GameController.Instance.mPassiveArtifactList[rand], room.transform);
                if (UIController.Instance.mPlayerLookPoint.transform.rotation.z <= 60 && UIController.Instance.mPlayerLookPoint.transform.rotation.z >= -60)
                {
                    art.transform.position = Player.Instance.transform.position + new Vector3(0, -2, 0);
                }
                else
                {
                    art.transform.position = Player.Instance.transform.position + new Vector3(0, 2, 0);
                }
                GameController.Instance.mPassiveArtifactList.RemoveAt(rand);
                if (GameSetting.Instance.Language == 0)
                {
                    text = "유물 획득!";
                }
                else
                {
                    text = "Get Artifact!";
                }
                index = 0;
                mRenderer.sprite = mSpt[3];
                SoundController.Instance.SESoundUI(3);
                Spend += 5;
                mPriceText.text = Spend.ToString() + "G";
            }
            else
            {
                FailRolling();
            }
        }
        else//액티브
        {
            if (GameController.Instance.mActiveArtifactList.Count>0)
            {
                int rand = Random.Range(0, GameController.Instance.mActiveArtifactList.Count);
                Artifacts art = Instantiate(GameController.Instance.mActiveArtifactList[rand], room.transform);
                if (UIController.Instance.mPlayerLookPoint.transform.rotation.z <= 60 && UIController.Instance.mPlayerLookPoint.transform.rotation.z >= -60)
                {
                    art.transform.position = Player.Instance.transform.position + new Vector3(0, -2, 0);
                }
                else
                {
                    art.transform.position = Player.Instance.transform.position + new Vector3(0, 2, 0);
                }
                GameController.Instance.mActiveArtifactList.RemoveAt(rand);
                if (GameSetting.Instance.Language == 0)
                {
                    text = "유물 획득!";
                }
                else
                {
                    text = "Get Artifact!";
                }
                index = 0;
                mRenderer.sprite = mSpt[3];
                SoundController.Instance.SESoundUI(3);
                Spend += 5;
                mPriceText.text = Spend.ToString() + "G";
            }
            else
            {
                FailRolling();
            }
        }
    }
    private void WeaponSearch()
    {
        int WeaponIndex = Random.Range(0, GameController.Instance.mWeaponList.Count);
        if (GameController.Instance.mWeaponList.Count>0)
        {
            Weapon weapon = Instantiate(GameController.Instance.mWeaponList[WeaponIndex], room.transform);
            if (UIController.Instance.mPlayerLookPoint.transform.rotation.z <= 60 && UIController.Instance.mPlayerLookPoint.transform.rotation.z >= -60)
            {
                weapon.transform.position = Player.Instance.transform.position + new Vector3(0, -2, 0);
            }
            else
            {
                weapon.transform.position = Player.Instance.transform.position + new Vector3(0, 2, 0);
            }
            GameController.Instance.mWeaponList.RemoveAt(WeaponIndex);
            if (GameSetting.Instance.Language == 0)
            {
                text = "무기 획득!";
            }
            else
            {
                text = "Get Weapone!";
            }
            mRenderer.sprite = mSpt[2];
            SoundController.Instance.SESoundUI(3);
            Spend += 5;
            mPriceText.text = Spend.ToString() + "G";
        }
        else
        {
            FailRolling();
        }
    }

    private IEnumerator Roll()
    {
        WaitForSecondsRealtime rolling = new WaitForSecondsRealtime(1f);
        UIController.Instance.NoTouchArea.gameObject.SetActive(true);
        Time.timeScale = 0;
        mAnim.SetBool(AnimHash.Slot,true);
        yield return rolling;
        Rolling();
    }

    public void UseSlotMachine()
    {
        if (enable == false)
        {
            if (GameController.Instance.mPassiveArtifactList.Count < 1)
            {
                if (GameSetting.Instance.Language == 0)
                {
                    text = "재고가 없습니다!";
                }
                else
                {
                    text = "Machine is Empty!";
                }
                TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                effect.SetText(text);
            }
            else
            {
                if (InventoryController.Instance.Full == false)
                {
                    if (Player.Instance.mStats.Gold >= Spend)
                    {
                        enable = true;
                        Player.Instance.mStats.Gold -= Spend;
                        UIController.Instance.ShowGold();
                        SoundController.Instance.SESound(19);
                        StartCoroutine(Roll());
                    }
                    else
                    {
                        if (GameSetting.Instance.Language == 0)
                        {
                            text = "골드가 부족합니다!";
                        }
                        else
                        {
                            text = "Not enough Gold!";
                        }
                        TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                        effect.SetText(text);
                    }
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
        }
        else
        {
            if (GameSetting.Instance.Language == 0)
            {
                text = "아직 사용중입니다!";
            }
            else
            {
                text = "Machine in use!";
            }
            TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
            effect.SetText(text);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mUIShopButton.onClick.RemoveAllListeners();
            mUIShopButton.onClick.AddListener(() => { UseSlotMachine(); });
            UIController.Instance.mShopSpendText.text = "-" +Spend+ "G";
            mUIShopButton.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mUIShopButton.gameObject.SetActive(false);
        }
    }

}
