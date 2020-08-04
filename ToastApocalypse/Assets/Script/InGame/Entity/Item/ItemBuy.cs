using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuy : MonoBehaviour
{
    public int mID;
    public bool Sell;
    public eShopType ShopType;
    public ShopController mShopController;

    public UsingItem item;
    public Artifacts artifact;
    public Text mPriceText;

    private void OnEnable()
    {
        Sell = false;
    }

    private void Start()
    {
        mPriceText.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        mPriceText.transform.localScale = new Vector3(10, 10, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Sell == false)
            {
                if (ShopType == eShopType.Item)
                {
                    if (Player.Instance.mStats.Gold >= item.mStats.Price)
                    {
                        Player.Instance.mStats.Gold -= item.mStats.Price;
                        Sell = true;
                        item.ItemChange();
                        item.IsShopItem = false;
                        CanvasFinder.Instance.DeleteShopPrice(mID);
                        UIController.Instance.ShowGold();
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
                        CanvasFinder.Instance.DeleteShopPrice(mID);
                        UIController.Instance.ShowGold();
                    }
                }
            }
        }
    }
}
