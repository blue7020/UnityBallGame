using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eShopType
{
    Item,
    Artifact
}

public class ItemBuy : MonoBehaviour
{
    [SerializeField]
    private bool Sell;

    [SerializeField]
    public eShopType ShopType;

    public UsingItem item;
    public Artifacts artifact;
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
                if (ShopType==eShopType.Item)
                {
                    if (Player.Instance.mInfoArr[Player.Instance.mID].Gold >= item.mInfoArr[item.mID].Price)
                    {
                        Sell = true;
                        item.IsShopItem = false;
                        Player.Instance.mInfoArr[Player.Instance.mID].Gold -= item.mInfoArr[item.mID].Price;
                        item.ItemChange();
                        ItemBowl.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("돈이 부족합니다!");
                    }
                }
                else
                {
                    if (Player.Instance.mInfoArr[Player.Instance.mID].Gold >= artifact.mInfoArr[artifact.mID].Price)
                    {
                        if (artifact.mType == eArtifactType.Passive)
                        {
                            if (Player.Instance.InventoryIndex < Player.Instance.Inventory.Length)
                            {
                                if (Player.Instance.mInfoArr[Player.Instance.mID].Gold >= artifact.mInfoArr[artifact.mID].Price)
                                {
                                    Sell = true;
                                    artifact.IsShopItem = false;
                                    Player.Instance.mInfoArr[Player.Instance.mID].Gold -= artifact.mInfoArr[artifact.mID].Price;
                                    artifact.ItemChange();
                                    ItemBowl.gameObject.SetActive(false);
                                }
                                else
                                {
                                    Debug.Log("돈이 부족합니다!");
                                }
                            }
                        }
                        else
                        {
                            if (Player.Instance.mInfoArr[Player.Instance.mID].Gold >= artifact.mInfoArr[artifact.mID].Price)
                            {
                                Sell = true;
                                artifact.IsShopItem = false;
                                Player.Instance.mInfoArr[Player.Instance.mID].Gold -= artifact.mInfoArr[artifact.mID].Price;
                                artifact.ItemChange();
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
        }
    }
}
