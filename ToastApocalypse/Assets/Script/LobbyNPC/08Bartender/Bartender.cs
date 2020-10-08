using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bartender : MonoBehaviour
{
    public static Bartender Instance;

    public Image mWindow;
    public Text mTitleText, mItemNameText, mItemTooltipText, mButtonText;
    public Button mButton;
    public NotOpenFurniture mFurniture;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetText()
    {
        if (GameSetting.Instance.NPCOpen[8] == true)
        {
            mFurniture.gameObject.SetActive(false);
        }
        if (GameSetting.Instance.Language==0)
        {
            mTitleText.text = "선술집";
            mItemNameText.text = "아이템 구매";
            mItemTooltipText.text = "아이템을 구매해 던전의 상점에서 파는\n아이템의 종류를 늘릴 수 있습니다";
            mButtonText.text = "구매";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitleText.text = "Pub";
            mItemNameText.text = "Item purchase";
            mItemTooltipText.text = "Can increase items\nsold in Dungeon shop.";
            mButtonText.text = "Purchase";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetText();
            mWindow.gameObject.SetActive(true);
        }
    }
}
