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

    public Animator mAnim;
    public bool enable;

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

    private void Rolling()
    {
        mAnim.SetBool(AnimHash.Slot, false);
        float rand = Random.Range(0, 1f);
        if (rand<0.15f)//꽝
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
        else if (WeaponController.Instance.mWeapons.Count > 0&&rand >=0.15f&&rand<0.6f)//무기
        {
            mRenderer.sprite = mSpt[2];
            StartCoroutine(WeaponSearch());
            SoundController.Instance.SESoundUI(3);
            Spend += 5;
            mPriceText.text = Spend.ToString() + "G";
        }
        else if (ArtifactController.Instance.mActiveArtifact.Count > 0 && rand >=0.6f)//유물
        {
            index = 0;
            mRenderer.sprite = mSpt[3];
            StartCoroutine(ArtifactSearch());
            SoundController.Instance.SESoundUI(3);
            Spend += 5;
            mPriceText.text = Spend.ToString() + "G";
        }
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

    private IEnumerator ArtifactSearch()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.1f);
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
            if (InventoryController.Instance.mSlotArr[index].mID!= ArtifactController.Instance.mPassiveArtifact[rand].mID)
            {
                Artifacts art = Instantiate(ArtifactController.Instance.mPassiveArtifact[rand], room.transform);
                if (UIController.Instance.mPlayerLookPoint.transform.rotation.z<=60&& UIController.Instance.mPlayerLookPoint.transform.rotation.z >=-60)
                {
                    art.transform.position = Player.Instance.transform.position + new Vector3(0, -2, 0);
                }
                else
                {
                    art.transform.position = Player.Instance.transform.position + new Vector3(0, 2, 0);
                }
                ArtifactController.Instance.mPassiveArtifact.RemoveAt(rand);
                if (GameSetting.Instance.Language == 0)
                {
                    text = "유물 획득!";
                }
                else
                {
                    text = "Get Artifact!";
                }
                break;
            }
            index++;
            yield return delay;
        }
    }
    private IEnumerator WeaponSearch()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.1f);
        while (true)
        {
            int WeaponIndex = Random.Range(0, WeaponController.Instance.mWeapons.Count);
            if (Player.Instance.NowPlayerWeapon.mID != WeaponController.Instance.mWeapons[WeaponIndex].mID)
            {
                Weapon weapon = Instantiate(WeaponController.Instance.mWeapons[WeaponIndex], room.transform);
                if (UIController.Instance.mPlayerLookPoint.transform.rotation.z <= 60 && UIController.Instance.mPlayerLookPoint.transform.rotation.z >= -60)
                {
                    weapon.transform.position = Player.Instance.transform.position + new Vector3(0, -2, 0);
                }
                else
                {
                    weapon.transform.position = Player.Instance.transform.position + new Vector3(0, 2, 0);
                }
                WeaponController.Instance.mWeapons.RemoveAt(WeaponIndex);
                if (GameSetting.Instance.Language == 0)
                {
                    text = "무기 획득!";
                }
                else
                {
                    text = "Get Weapone!";
                }
                break;
            }
            yield return delay;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (enable==false)
            {
                if (ArtifactController.Instance.mPassiveArtifact.Count<1)
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
    }

}
