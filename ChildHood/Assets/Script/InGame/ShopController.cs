using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private ItemPool[] mItempool;

    private void Awake()
    {
        for (int i=0; i<mItempool.Length;i++)
        {
            int rand = Random.Range(1,mItempool.Length);//아이템 수만큼
            mItempool[i].GetFromPool(rand);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          //불러와진 아이템 풀의 물품 구입
        }
    }
}
