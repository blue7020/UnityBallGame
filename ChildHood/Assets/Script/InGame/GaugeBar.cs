using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    public Image mGauge;
    public Enemy mEnemy;

    public void SetGauge(float current, float max)
    {
        float fillAmount = (current / max);
        mGauge.fillAmount = fillAmount;
    }

    public void CloseGauge()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (mEnemy!=null)
        {
            transform.position = mEnemy.mHPBarPos.transform.position;
        }
    }
}
