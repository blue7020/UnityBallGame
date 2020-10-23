using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Transform[] mPos;
    public ItemBuy[] itembuy;

    private List<UsingItem> mItemList;

    public Text mPriceText;
    public Room Shop;

    private UsingItem item;
    private Artifacts artifact;

    private void Awake()
    {
        mItemList = new List<UsingItem>();
        for (int i=0; i< SaveDataController.Instance.mItemInfoArr.Length; i++)
        {
            if (SaveDataController.Instance.mUser.ItemHas[i]==true)
            {
                mItemList.Add(GameSetting.Instance.mItemArr[i]);
            }
        }
        for (int i = 0; i < itembuy.Length; i++)
        {
            itembuy[i].mPriceText = Instantiate(mPriceText, CanvasFinder.Instance.transform);
            CanvasFinder.Instance.mShopPriceText[i] = itembuy[i].mPriceText;
            itembuy[i].mID = i;
        }

    }

    private void Start()
    {
        int rand = Random.Range(0, mItemList.Count);
        item = Instantiate(mItemList[rand], mPos[0]);
        item.transform.SetParent(mPos[0]);
        item.IsShopItem = true;
        itembuy[0].item = item;
        itembuy[0].mPriceText.text = item.mStats.Price.ToString() + "G";
        mItemList.RemoveAt(rand);
        itembuy[0].mPriceText.gameObject.SetActive(false);
        if (GameController.Instance.StageLevel % 2 == 1)
        {
            int index = 1;
            int ShopCount = 0;
            for (int i = 0; i < InventoryController.Instance.mSlotArr.Length; i++)
            {
                if (ArtifactController.Instance.mPassiveArtifact.Count>0)
                {
                    rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
                    if (itembuy[(index - ShopCount)].artifact != ArtifactController.Instance.mPassiveArtifact[rand])
                    {
                        if (InventoryController.Instance.mSlotArr[i].artifact != ArtifactController.Instance.mPassiveArtifact[rand])
                        {
                            artifact = Instantiate(ArtifactController.Instance.mPassiveArtifact[rand], mPos[index]);
                            ArtifactController.Instance.mPassiveArtifact.RemoveAt(rand);
                            artifact.transform.SetParent(mPos[index]);
                            artifact.Currentroom = Shop;
                            artifact.IsShopItem = true;
                            itembuy[index].artifact = artifact;
                            itembuy[index].mPriceText.text = artifact.mStats.Price.ToString() + "G";
                            itembuy[index].mPriceText.gameObject.SetActive(false);
                            if (index < 2)
                            {
                                index++;
                                ShopCount++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            ActiveArtifactSearch();
        }
        
    }
        
    private void ActiveArtifactSearch()
    {
        int index = 1;
        int ShopCount = 0;
        if (ArtifactController.Instance.mActiveArtifact.Count>0)
        {
            for (int i = 0; i < ArtifactController.Instance.mActiveArtifact.Count; i++)
            {
                int rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
                if (itembuy[index - ShopCount].artifact != ArtifactController.Instance.mActiveArtifact[rand])
                {
                    artifact = Instantiate(ArtifactController.Instance.mActiveArtifact[rand], mPos[index]);
                    ArtifactController.Instance.mActiveArtifact.RemoveAt(rand);
                    artifact.transform.SetParent(mPos[index]);
                    artifact.Currentroom = Shop;
                    artifact.IsShopItem = true;
                    itembuy[index].artifact = artifact;
                    itembuy[index].mPriceText.text = artifact.mStats.Price.ToString() + "G";
                    itembuy[index].mPriceText.gameObject.SetActive(false);
                    if (index < 2)
                    {
                        index++;
                        ShopCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }

}
