﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    private const int SLOT_COUNT = 16;
    public static InventoryController Instance;

#pragma warning disable 0649
    public InventorySlot mSlotPrefab;
    [SerializeField]
    private Transform mSlotParents;
    [SerializeField]
    private Sprite mVoidImage;
#pragma warning restore 0649

    public InventorySlot[] mSlotArr;
    public int nowIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mSlotArr = new InventorySlot[SLOT_COUNT];
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                mSlotArr[i] = Instantiate(mSlotPrefab, mSlotParents);
                mSlotArr[i].Init(i, mVoidImage);
            }
        }
        else
        {
            Delete();
        }
        nowIndex = 0;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Additem(int nowIndex, Artifacts art)
    {
        mSlotArr[nowIndex].artifact = art;
        mSlotArr[nowIndex].mItemImage.sprite = art.mRenderer.sprite;
    }
}