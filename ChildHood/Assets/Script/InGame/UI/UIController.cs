﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    [SerializeField]
    private Text mGoldText;
    [SerializeField]
    private Text mHPText;

    [SerializeField]
    private RawImage mMiniMapCamera;
    private bool Minimap;

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
        Minimap = false;
    }

    public void ShowMiniMap()
    {
        if (Minimap == true)
        {
            Minimap = false;
            mMiniMapCamera.gameObject.SetActive(false);
        }
        else if(Minimap==false)
        {
            Minimap = true;
            mMiniMapCamera.gameObject.SetActive(true);

        }
    }

    public float StatSetting
    {
        set
        {
            mGoldText.text = value.ToString();
            mHPText.text = value.ToString();
        }
    }

    public void ShowGold()
    {
        mGoldText.text = Player.Instance.mInfoArr[Player.Instance.mID].Gold.ToString();
    }

    public void ShowHP()
    {
        string HP = string.Format("{0} / {1}", Player.Instance.mCurrentHP.ToString(), Player.Instance.mInfoArr[Player.Instance.mID].Hp.ToString());
        mHPText.text = HP;
    }
}