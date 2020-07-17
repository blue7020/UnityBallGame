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
#pragma warning disable 0649
    [SerializeField]
    private bool Sell;

    public eShopType ShopType;

    public UsingItem item;
    public Artifacts artifact;
    [SerializeField]
    private GameObject ItemBowl;
#pragma warning restore 0649

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
                    if (Player.Instance.mStats.Gold >= item.Stats.Price)
                    {
                        Sell = true;
                        item.IsShopItem = false;
                        Player.Instance.mStats.Gold -= item.Stats.Price;
                        item.ItemChange();
                        ItemBowl.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                    {
                        if (artifact.mType == eArtifactType.Passive)
                        {
                            if (InventoryController.Instance.nowIndex < InventoryController.Instance.mSlotArr.Length)
                            {
                                if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                                {
                                    Sell = true;
                                    artifact.IsShopItem = false;
                                    Player.Instance.mStats.Gold -= artifact.mStats.Price;
                                    artifact.ItemChange();
                                    ItemBowl.gameObject.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                            {
                                Sell = true;
                                artifact.IsShopItem = false;
                                Player.Instance.mStats.Gold -= artifact.mStats.Price;
                                artifact.ItemChange();
                                ItemBowl.gameObject.SetActive(false);
                            }
                        }
                        
                    } 
                }
                UIController.Instance.ShowGold();
            }
        }
    }
}
