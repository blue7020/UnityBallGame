using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    [SerializeField]
    public Transform[] mPos;
    [SerializeField]
    private ItemBuy[] itembuy;
    [SerializeField]
    private UsingItem[] mItemList;
    [SerializeField]
    private Room Shop;

    private UsingItem item;

    private void Start()
    {
        for (int i=0; i<mPos.Length;i++)
        {
            int rand = Random.Range(0, mItemList.Length);
            item = Instantiate(mItemList[rand], mPos[i].position,Quaternion.identity);
            Debug.Log(item.transform.position);
            item.transform.SetParent(mPos[i]);
            item.IsShopItem = true;
            itembuy[i].item = item;
            item = null;
        }
    }
}
