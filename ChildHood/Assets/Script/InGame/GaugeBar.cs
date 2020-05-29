using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    public static GaugeBar Instance;

    [SerializeField]
    private Image mGauge;

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

    public void SetGauge(float current, float max)
    {
        float fillAmount = (current / max);
        mGauge.fillAmount = fillAmount;
    }
}
