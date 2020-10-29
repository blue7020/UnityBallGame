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
        for (int i = 0; i < SaveDataController.Instance.mItemInfoArr.Length; i++)
        {
            if (SaveDataController.Instance.mUser.ItemHas[i] == true)
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
            if (GameController.Instance.mPassiveArtifactList.Count > 0)
            {
                for (int i = 0; i < GameController.Instance.mPassiveArtifactList.Count; i++)
                {
                    rand = Random.Range(0, GameController.Instance.mPassiveArtifactList.Count);
                    if (itembuy[(index - ShopCount)].artifact == null || itembuy[(index - ShopCount)].artifact != GameController.Instance.mPassiveArtifactList[rand])
                    {
                        artifact = Instantiate(GameController.Instance.mPassiveArtifactList[rand], mPos[index]);
                        GameController.Instance.mPassiveArtifactList.RemoveAt(rand);
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
            else
            {
                itembuy[1].artifact =null;
                itembuy[1].mPriceText.gameObject.SetActive(false);
                itembuy[2].artifact =null;
                itembuy[2].mPriceText.gameObject.SetActive(false);
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
        if (GameController.Instance.mActiveArtifactList.Count > 0)
        {
            for (int i = 0; i < GameController.Instance.mActiveArtifactList.Count; i++)
            {
                int rand = Random.Range(0, GameController.Instance.mActiveArtifactList.Count);
                if (itembuy[index - ShopCount].artifact != GameController.Instance.mActiveArtifactList[rand])
                {
                    artifact = Instantiate(GameController.Instance.mActiveArtifactList[rand], mPos[index]);
                    GameController.Instance.mActiveArtifactList.RemoveAt(rand);
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
