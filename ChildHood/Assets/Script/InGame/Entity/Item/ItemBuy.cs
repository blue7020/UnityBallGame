using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private bool Sell;

    public eShopType ShopType;

    public UsingItem item;
    public Artifacts artifact;
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
                        item.IsShopItem = false;
                        Player.Instance.mStats.Gold -= item.Stats.Price;
                        Sell = true;
                        item.ItemChange();
                    }
                }
                else
                {
                    if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                    {
                        if (artifact.mType == eArtifactType.Passive)
                        {
                            if (InventoryController.Instance.nowIndex <= InventoryController.Instance.mSlotArr.Length)
                            {
                                if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                                {
                                    artifact.IsShopItem = false;
                                    Player.Instance.mStats.Gold -= artifact.mStats.Price;
                                    Sell = true;
                                    artifact.Currentroom = Player.Instance.CurrentRoom;
                                    artifact.ItemChange();
                                }
                            }
                        }
                        else
                        {
                            if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                            {
                                artifact.IsShopItem = false;
                                Player.Instance.mStats.Gold -= artifact.mStats.Price;
                                Sell = true;
                                artifact.Currentroom = Player.Instance.CurrentRoom;
                                artifact.ItemChange();
                            }
                        }
                        
                    } 
                }
                UIController.Instance.ShowGold();
            }
        }
    }
}
