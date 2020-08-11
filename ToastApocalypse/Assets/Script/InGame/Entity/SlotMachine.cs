using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public Room room;
    public Sprite[] mSpt;
    public Artifacts[] PassiveArtifactArr;
    public SpriteRenderer mRenderer;
    public Weapon[] WeaponeArr;
    public int Spend,index;
    private string text;

    public Text mPriceText;

    public Animator mAnim;
    public bool enable;

    private void Awake()
    {
        enable = false;
        mPriceText = Instantiate(mPriceText, CanvasFinder.Instance.transform);
        mPriceText.text = Spend.ToString() + "G";
        mPriceText.gameObject.SetActive(false);
        CanvasFinder.Instance.mSlotPriceText = mPriceText;
        mPriceText.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        mPriceText.transform.localScale = new Vector3(10, 10, 0);
    }

    private void Rolling()
    {
        mAnim.SetBool(AnimHash.Slot, false);
        float rand = Random.Range(0, 1f);
        if (rand<=0.25f)//꽝
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
        }
        else if (rand >0.25f&&rand<=0.6f)//무기
        {
            mRenderer.sprite = mSpt[2];
            StartCoroutine(WeaponSerch());
            if (GameSetting.Instance.Language == 0)
            {
                text = "무기 획득!";
            }
            else
            {
                text = "Get Weapone!";
            }
        }
        else if (rand>0.6f)//유물
        {
            index = 0;
            mRenderer.sprite = mSpt[3];
            StartCoroutine(ArtifactSearch());
            if (GameSetting.Instance.Language == 0)
            {
                text = "유물 획득!";
            }
            else
            {
                text = "Get Artifact!";
            }
        }
        TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
        effect.SetText(text);
        enable = false;
        mRenderer.sprite = mSpt[0];

    }

    private IEnumerator ArtifactSearch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int rand = Random.Range(0, PassiveArtifactArr.Length);
            if (InventoryController.Instance.mSlotArr[index]!= PassiveArtifactArr[rand])
            {
                Artifacts art = Instantiate(PassiveArtifactArr[rand], room.transform);
                art.transform.position = Player.Instance.transform.position - new Vector3(0, -1, 0);
                break;
            }
            index++;
            yield return delay;
        }
    }
    private IEnumerator WeaponSerch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int rand = Random.Range(0, WeaponeArr.Length);
            if (Player.Instance.NowPlayerWeapon != WeaponeArr[rand])
            {
                Weapon weapon = Instantiate(WeaponeArr[rand], room.transform);
                weapon.transform.position = Player.Instance.transform.position - new Vector3(0, -1, 0);
                break;
            }
            yield return delay;
        }
    }


    private IEnumerator Roll()
    {
        WaitForSeconds rolling = new WaitForSeconds(2f);
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
                enable = true;
                if (Player.Instance.mStats.Gold >= Spend)
                {
                    Player.Instance.mStats.Gold -= Spend;
                    UIController.Instance.ShowGold();
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

        }
    }

}
