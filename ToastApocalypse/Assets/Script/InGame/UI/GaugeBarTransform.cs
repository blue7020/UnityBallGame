using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBarTransform : MonoBehaviour
{
    public static GaugeBarTransform Instance;

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

    private void Start()
    {

        GaugeBarPool.Instance.mGaugeBarArea = gameObject.transform;
    }
}
