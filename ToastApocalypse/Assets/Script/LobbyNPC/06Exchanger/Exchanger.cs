using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exchanger : MonoBehaviour
{

    public static Exchanger Instance;

    public Image mPointer;
    public ExchangeController mWindow;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (SaveDataController.Instance.mUser.NPCOpen[6] == false)
            {
                mPointer.gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.RefreshInventory();
            mWindow.gameObject.SetActive(true);
        }
    }
}
