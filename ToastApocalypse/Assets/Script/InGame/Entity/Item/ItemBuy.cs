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
    private string text;

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
                    else
                    {
                        if (GameSetting.Instance.Language == 0)
                        {
                            text = "골드가 부족합니다!";
                        }
                        else
                        {
                            text = "Not enough Gold!";
                        }
                        TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                        effect.SetText(text);
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
                                artifact.IsShopItem = false;
                                Player.Instance.mStats.Gold -= artifact.mStats.Price;
                                Sell = true;
                                artifact.Currentroom = Player.Instance.CurrentRoom;
                                artifact.ItemChange();
                            }
                            else
                            {
                                if (GameSetting.Instance.Language == 0)
                                {
                                    text = "인벤토리 공간이 부족합니다!";
                                }
                                else
                                {
                                    text = "Inventory if full!";
                                }
                                TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                                effect.SetText(text);
                            }
                        }
                        else
                        {
                            artifact.IsShopItem = false;
                            Player.Instance.mStats.Gold -= artifact.mStats.Price;
                            Sell = true;
                            artifact.Currentroom = Player.Instance.CurrentRoom;
                        }
                        CanvasFinder.Instance.DeleteShopPrice(mID);
                        UIController.Instance.ShowGold();
                    }
                    else
                    {
                        if (GameSetting.Instance.Language == 0)
                        {
                            text = "골드가 부족합니다!";
                        }
                        else
                        {
                            text = "Not enough Gold!";
                        }
                        TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                        effect.SetText(text);
                    }

                }
            }
        }
    }
}
