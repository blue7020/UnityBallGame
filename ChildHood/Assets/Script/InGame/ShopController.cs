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
    private UsingItem[] mItemArr;
    private List<UsingItem> mItemList;

    [SerializeField]
    private Artifacts[] mArtifactArr;
    [SerializeField]
    private List<Artifacts> mUsingArtifact;
    [SerializeField]
    private List<Artifacts> mPassiveArtifact;

    [SerializeField]
    private Room Shop;

    private UsingItem item;
    private Artifacts artifact;

    private void Awake()
    {
        mItemList = new List<UsingItem>();
        for (int i=0; i< mItemArr.Length; i++)
        {
            mItemList.Add(mItemArr[i]);
        }
        mPassiveArtifact = new List<Artifacts>();
        mUsingArtifact = new List<Artifacts>();
        for (int i = 0; i < mArtifactArr.Length; i++)
        {
            if (mArtifactArr[i].mType==eArtifactType.Use)
            {
                mUsingArtifact.Add(mArtifactArr[i]);
            }
            else
            {
                mPassiveArtifact.Add(mArtifactArr[i]);
            }
        }

    }

    private void Start()
    {
        if (Player.Instance.Level % 2 == 1)
        {
            int rand = Random.Range(0, mItemList.Count);
            item = Instantiate(mItemList[rand], mPos[0].position, Quaternion.identity);
            itembuy[0].item = item;
            mItemList.RemoveAt(rand);
            for (int i = 1; i < mPos.Length; i++)
            {
                rand = Random.Range(0, mPassiveArtifact.Count);
                artifact = Instantiate(mPassiveArtifact[rand], mPos[i].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[i]);
                artifact.Currentroom = Shop;
                artifact.IsShopItem = true;
                itembuy[i].artifact = artifact;
                mPassiveArtifact.Remove(artifact);
                //사용 아이템 1개 패시브 유물 2개
            }
        }
        else
        {
            int rand = Random.Range(0, mItemList.Count);
            item = Instantiate(mItemList[rand], mPos[0].position, Quaternion.identity);
            itembuy[0].item = item;
            mItemList.RemoveAt(rand);
            for (int i = 1; i < mPos.Length; i++)
            {
                rand = Random.Range(0, mUsingArtifact.Count);
                artifact = Instantiate(mUsingArtifact[rand], mPos[i].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[i]);
                artifact.Currentroom = Shop;
                artifact.IsShopItem = true;
                itembuy[i].artifact = artifact;
                //사용 아이템 1개 액티브 유물 2개
            }
        }
        
        
    }
}
