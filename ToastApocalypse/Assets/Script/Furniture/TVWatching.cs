using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVWatching : MonoBehaviour
{
    public Image mWindow;
    public Text mTooltipText, mButtonText;
    public Button mAdButton;

    private void Awake()
    {
        if (GameSetting.Instance.Language==0)
        {
            mTooltipText.text = "광고를 보시겠습니까?\n(매일 한번 광고 시청 시 시럽 지급)";
            mButtonText.text = "본다";
        }
        else if (GameSetting.Instance.Language == 1)
        {
            mTooltipText.text = "Would you like to see the AD?\n(Syrup is given once a day when watching an AD.)";
            mButtonText.text = "Yes";
        }
    }

    public void ShowAds()
    {
        GameSetting.Instance.ShowAds();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
