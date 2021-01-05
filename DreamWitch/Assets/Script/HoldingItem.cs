using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    public int mID;
    public float mCooltime;
    public bool isDrop, isConsumable,isCooltime;
    public GameObject mItemKeyObj;
    public SpriteRenderer mRenderer;

    private void Awake()
    {
        isDrop = true;
        isCooltime = false;
        ItemDropCheck();
    }

    public void ItemDropCheck()
    {
        if (isDrop)
        {
            mItemKeyObj.SetActive(true);
        }
        else
        {
            mItemKeyObj.SetActive(false);
        }
    }

    public void Hold()
    {
        isDrop = false;
        ItemDropCheck();
        transform.SetParent(Player.Instance.mItemTransform);
        gameObject.SetActive(false);
    }

    public void Drop()
    {
        isDrop = true;
        ItemDropCheck();
        transform.SetParent(null);
        gameObject.SetActive(true);
    }

    public void ItemUse()
    {
        ItemController.Instance.UseItem(mID);
        if (mCooltime>0&& !isCooltime)
        {
            Player.Instance.StartCoroutine(ItemCooltime());
        }
    }

    public IEnumerator ItemCooltime()
    {
        WaitForSeconds delay = new WaitForSeconds(mCooltime);
        isCooltime = true;
        yield return delay;
        isCooltime = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.mDropItem = this;
            Player.Instance.mFuntion = (()=> { Player.Instance.ItemFuntion(); });
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.mDropItem = null;
            Player.Instance.mFuntion = null;
        }

    }
}
