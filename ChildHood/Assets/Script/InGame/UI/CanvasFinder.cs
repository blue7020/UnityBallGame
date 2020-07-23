using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFinder : MonoBehaviour
{
    public static CanvasFinder Instance;

    public Text[] mPriceText = new Text[3];
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

    public void DeletePrice(int index)
    {
        mPriceText[index].gameObject.SetActive(false);
    }
}
