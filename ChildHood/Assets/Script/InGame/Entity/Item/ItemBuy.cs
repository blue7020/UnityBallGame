using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    [SerializeField]
    private bool Sell;

    public UsingItem item;
    [SerializeField]
    private GameObject ItemBowl;

    private void Start()
    {
        Sell = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Sell == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Player.Instance.mInfoArr[Player.Instance.mID].Gold >= item.mInfoArr[item.mID].Price)
                {
                    Sell = true;
                    Player.Instance.mInfoArr[Player.Instance.mID].Gold -= item.mInfoArr[item.mID].Price;
                    item.ItemChange();
                    ItemBowl.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("돈이 부족합니다!");
                }

            }
        }
    }
}
