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
    private Artifacts[] mArtifactList;

    private UsingItem item;
    private Artifacts artifact;

    private void Start()
    {
        if (Player.Instance.Level % 2 == 1)
        {
            for (int i = 0; i < mPos.Length; i++)
            {
                int rand = Random.Range(0, mItemList.Length);
                item = Instantiate(mItemList[rand], mPos[i].position, Quaternion.identity);
                item.transform.SetParent(mPos[i]);
                item.IsShopItem = true;
                itembuy[i].item = item;
                item = null;
            }
        }
        else
        {
            for (int i = 0; i < mPos.Length; i++)
            {
                int rand = Random.Range(0, mArtifactList.Length);
                artifact = Instantiate(mArtifactList[rand], mPos[i].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[i]);
                artifact.IsShopItem = true;
                itembuy[i].artifact = artifact;
                artifact = null;
            }
        }
        
        
    }
}
