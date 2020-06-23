using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    [SerializeField]
    public Transform mPos;

    [SerializeField]
    private ItemPool mItempool;
    [SerializeField]
    private GameObject ItemBowl;

    private UsingItem item;

    [SerializeField]
    private bool Sell;

    private void Start()
    {
        Sell = false;
        int rand = Random.Range(0, 3);//TODO 이후 아이템 수만큼 수정
        item = Instantiate(mItempool.GetFromPool(rand), mPos);
        item.IsShopItem = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Sell==false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Player.Instance.mInfoArr[Player.Instance.mID].Gold >= item.mInfoArr[item.mID].Price)
                {
                    //불러와진 아이템 풀의 물품 구입
                    Sell = true;
                    Player.Instance.mInfoArr[Player.Instance.mID].Gold -= item.mInfoArr[item.mID].Price;
                    item = Instantiate(item,Player.Instance.gameObject.transform);
                    item.gameObject.SetActive(false);
                    Player.Instance.NowItem = item;
                    UIController.Instance.ShowItemImage();
                    ItemBowl.gameObject.SetActive(false);
                    Debug.Log("구입하였습니다!");
                }
                else
                {
                    Debug.Log("돈이 부족합니다!");
                }

            }
        }
    }
}
