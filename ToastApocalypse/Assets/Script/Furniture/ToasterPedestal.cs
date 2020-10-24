using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToasterPedestal : MonoBehaviour
{
    public static ToasterPedestal Instance;

    public Sprite[] mSpt;
    public Image mPartsImage;
    public GameObject mLightRound;
    public SpriteRenderer mRenderer;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            ShowToasterImage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowToasterImage()
    {
        if (SaveDataController.Instance.mUser.FirstGameClearEvent == true)
        {
            mRenderer.sprite = mSpt[1];
            mLightRound.SetActive(true);
        }
        else
        {
            mRenderer.sprite = mSpt[0];
            mLightRound.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SaveDataController.Instance.mUser.FirstGameClearEvent ==false)
            {
                mPartsImage.gameObject.SetActive(true);
            }
        }
    }
}
