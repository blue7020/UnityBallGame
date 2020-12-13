using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance;

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

    public void UseItem(int id)
    {
        switch (id)
        {
            case 0:
                Player.Instance.ShowAction(3);
                break;
            default:
                break;
        }
    }
}
