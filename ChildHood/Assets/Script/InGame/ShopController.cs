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
    private ItemPool mItempool;

    private UsingItem item;

    private void Start()
    {
        for (int i=0; i<mPos.Length;i++)
        {
            int rand = Random.Range(0, 3);//TODO 이후 아이템 수만큼 수정
            item = mItempool.GetFromPool(rand);
            item.transform.SetParent(mPos[i]);
            item.transform.position = mPos[i].transform.position;
            item.IsShopItem = true;
            itembuy[i].item = item;
            item = null;
        }
    }

    
}
