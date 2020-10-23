using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardWindow : MonoBehaviour
{
    public Image mIcon;
    public Text mAmountText;

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
