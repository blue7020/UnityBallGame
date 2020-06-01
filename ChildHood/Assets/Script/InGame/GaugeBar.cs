using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{

    [SerializeField]
    private Image mGauge;

    public void SetGauge(float current, float max)
    {
        float fillAmount = (current / max);
        mGauge.fillAmount = fillAmount;
    }
}
