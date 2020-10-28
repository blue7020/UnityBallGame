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
    public Text mPriceText, mShopSpendText;
    private Button mUIShopButton, mTutorialUIShopButton;
    private string text;

    private void Awake()
    {
        mUIShopButton = UIController.Instance.mShopButton;
        mShopSpendText = UIController.Instance.mShopSpendText;
    }

    private void Start()
    {
        Sell = false;
        mPriceText.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        mPriceText.transform.localScale = new Vector3(0.1f, 0.1f, 0);
    }

    public void Buy()
    {
        if (Sell == false)
        {
            if (ShopType == eShopType.Item)
            {
                if (Player.Instance.mStats.Gold >= item.mStats.Price)
                {
                    mUIShopButton.gameObject.SetActive(false);
                    Player.Instance.mStats.Gold -= item.mStats.Price;
                    Sell = true;
                    item.ItemChange();
                    item.IsShopItem = false;
                    SoundController.Instance.SESoundUI(3);
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
                if (artifact != null)
                {
                    if (Player.Instance.mStats.Gold >= artifact.mStats.Price)
                    {
                        if (artifact.eType == eArtifactType.Passive)
                        {
                            if (InventoryController.Instance.Full == false)
                            {
                                mUIShopButton.gameObject.SetActive(false);
                                artifact.IsShopItem = false;
                                Player.Instance.mStats.Gold -= artifact.mStats.Price;
                                Sell = true;
                                artifact.Currentroom = Player.Instance.CurrentRoom;
                                artifact.ArtifactChange();
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
                            mUIShopButton.gameObject.SetActive(false);
                            artifact.IsShopItem = false;
                            Player.Instance.mStats.Gold -= artifact.mStats.Price;
                            Sell = true;
                            artifact.Currentroom = Player.Instance.CurrentRoom;
                            artifact.ArtifactChange();
                        }
                        SoundController.Instance.SESoundUI(3);
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
                    if (GameSetting.Instance.Language == 0)
                    {
                        text = "품절";
                    }
                    else
                    {
                        text = "Sold out";
                    }
                    TextEffect effect = TextEffectPool.Instance.GetFromPool(0);
                    effect.SetText(text);
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Sell==false)
            {
                mUIShopButton.onClick.RemoveAllListeners();
                mUIShopButton.onClick.AddListener(() => { Buy(); });
                if (ShopType == eShopType.Item)
                {
                    mShopSpendText.text = "-" + item.mStats.Price + "G";
                }
                else
                {
                    mShopSpendText.text = "-" + artifact.mStats.Price + "G";
                }
                mUIShopButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mUIShopButton.gameObject.SetActive(false);
        }
    }
}
