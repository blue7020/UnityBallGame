using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtistWindowController : MonoBehaviour
{
    public static ArtistWindowController Instance;

    public ArtSlot mArtSlot;
    public Transform mParents;
    public ArtText mArt;
    public Image mShowArtWindow;

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

    private void Start()
    {
        for (int i = 0; i < ArtistController.Instance.mArtArr.Length; i++)
        {
            if (GameSetting.Instance.mArtInfoArr[i].Open==true)
            {
                ArtSlot mSlot = Instantiate(mArtSlot, mParents);
                mSlot.SetData(i, GameSetting.Instance.mArtInfoArr[i].ArtCode);
            }

        }
    }

    public void SelectArt(ArtText art)
    {
        mArt = art;
        if (GameSetting.Instance.mArtInfoArr[mArt.ID].Open == true)
        {
            ArtistController.Instance.mButton.interactable = true;
        }
        else
        {
            ArtistController.Instance.mButton.interactable = false;
        }
        if (GameSetting.Instance.Language == 0)//한국어
        {
            ArtistController.Instance.mArtTitleText.text = mArt.ID +"번\n"+ mArt.ContensFormat;
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            ArtistController.Instance.mArtTitleText.text = "Number "+mArt.ID + "\n" + mArt.EngContensFormat;
        }
    }

    public void ShowArt()
    {
        mShowArtWindow.sprite = ArtistController.Instance.mArtArr[mArt.ID];
        mShowArtWindow.gameObject.SetActive(true);
    }
}
