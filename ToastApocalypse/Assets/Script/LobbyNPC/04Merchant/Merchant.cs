using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Merchant : MonoBehaviour
{
    public static Merchant Instance;

    public Image mWindow;
    public Text mTitle, mItemTitle, mItemText;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameSetting.Instance.Language==0)
        {
            mTitle.text = "상점";
            mItemTitle.text = "상품명";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTitle.text = "Shop";
            mItemTitle.text = "ItemName";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
            MainLobbyUIController.Instance.GamePause();
        }
    }
}
