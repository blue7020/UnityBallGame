using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public static Merchant Instance;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (GameSetting.Instance.NPCOpen[4] == false)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
