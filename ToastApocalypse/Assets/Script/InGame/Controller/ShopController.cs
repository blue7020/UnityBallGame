using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Transform[] mPos;
    public ItemBuy[] itembuy;

    public UsingItem[] mItemArr;
    private List<UsingItem> mItemList;


    public Text mPriceText;
    public Room Shop;

    private UsingItem item;
    private Artifacts artifact;
    private int index;

    private void Awake()
    {
        mItemList = new List<UsingItem>();
        for (int i=0; i< mItemArr.Length; i++)
        {
            mItemList.Add(mItemArr[i]);
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
        item = Instantiate(mItemList[rand], mPos[0].position, Quaternion.identity);
        item.transform.SetParent(mPos[0]);
        item.IsShopItem = true;
        itembuy[0].item = item;
        itembuy[0].mPriceText.text = item.mStats.Price.ToString() + "G";
        mItemList.RemoveAt(rand);
        itembuy[0].mPriceText.gameObject.SetActive(false);
        if (GameController.Instance.Level % 2 == 1)
        {
            StartCoroutine(PassiveArtifactSearch());
            //사용 아이템 1개 패시브 유물 2개
        }
        else
        {
            StartCoroutine(ActiveArtifactSearch());
            //사용 아이템 1개 액티브 유물 2개
        }
        
    }

    private IEnumerator PassiveArtifactSearch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        index = 0;
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
            if (InventoryController.Instance.mSlotArr[index] != ArtifactController.Instance.mPassiveArtifact[rand])
            {
                rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
                artifact = Instantiate(ArtifactController.Instance.mPassiveArtifact[rand], mPos[1].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[1]);
                artifact.Currentroom = Shop;
                artifact.IsShopItem = true;
                itembuy[1].artifact = artifact;
                itembuy[1].mPriceText.text = artifact.mStats.Price.ToString() + "G";
                ArtifactController.Instance.mPassiveArtifact.Remove(artifact);
                itembuy[1].mPriceText.gameObject.SetActive(false);
                StartCoroutine(PassiveArtifactSearch2());
                break;
            }
            index++;
            yield return delay;
        }
    }
    private IEnumerator PassiveArtifactSearch2()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        index = 0;
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
            if (InventoryController.Instance.mSlotArr[index] != ArtifactController.Instance.mPassiveArtifact[rand]&& itembuy[1].artifact!= ArtifactController.Instance.mPassiveArtifact[rand])
            {
                rand = Random.Range(0, ArtifactController.Instance.mPassiveArtifact.Count);
                artifact = Instantiate(ArtifactController.Instance.mPassiveArtifact[rand], mPos[2].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[2]);
                artifact.Currentroom = Shop;
                artifact.IsShopItem = true;
                itembuy[2].artifact = artifact;
                itembuy[2].mPriceText.text = artifact.mStats.Price.ToString() + "G";
                ArtifactController.Instance.mPassiveArtifact.Remove(artifact);
                itembuy[2].mPriceText.gameObject.SetActive(false);
                break;
            }
            index++;
            yield return delay;
        }
    }
    private IEnumerator ActiveArtifactSearch()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
            if (Player.Instance.NowActiveArtifact != ArtifactController.Instance.mActiveArtifact[rand])
            {
                rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
                artifact = Instantiate(ArtifactController.Instance.mActiveArtifact[rand], mPos[1].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[1]);
                artifact.Currentroom = Shop;
                artifact.IsShopItem = true;
                itembuy[1].artifact = artifact;
                itembuy[1].mPriceText.text = artifact.mStats.Price.ToString() + "G";
                ArtifactController.Instance.mActiveArtifact.Remove(artifact);
                itembuy[1].mPriceText.gameObject.SetActive(false);
                StartCoroutine(ArtifactSearch2());
                break;
            }
            yield return delay;
        }
    }
    private IEnumerator ArtifactSearch2()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
            if (Player.Instance.NowActiveArtifact != ArtifactController.Instance.mActiveArtifact[rand] && itembuy[1].artifact != ArtifactController.Instance.mActiveArtifact[rand])
            {
                rand = Random.Range(0, ArtifactController.Instance.mActiveArtifact.Count);
                artifact = Instantiate(ArtifactController.Instance.mActiveArtifact[rand], mPos[2].position, Quaternion.identity);
                artifact.transform.SetParent(mPos[2]);
                artifact.Currentroom = Shop;
                artifact.IsShopItem = true;
                itembuy[2].artifact = artifact;
                itembuy[2].mPriceText.text = artifact.mStats.Price.ToString() + "G";
                ArtifactController.Instance.mActiveArtifact.Remove(artifact);
                itembuy[2].mPriceText.gameObject.SetActive(false);
                break;
            }
            yield return delay;
        }
    }
}
