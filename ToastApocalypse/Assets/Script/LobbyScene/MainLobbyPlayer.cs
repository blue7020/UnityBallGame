﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyPlayer : MonoBehaviour
{
    public static MainLobbyPlayer Instance;

    public VirtualJoyStick joyskick;

    public int mID;
    public Sprite PlayerImage;

    public int mSpeed;

    public SpriteRenderer mRenderer;
    public Rigidbody2D mRB2D;
    public Animator mAnim;

    public float hori;
    public float ver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            joyskick = PlayerSelectController.Instance.mStick;
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Moveing();
    }

    private void Moveing()
    {
        hori = joyskick.Horizontal();
        ver = joyskick.Vectical();
        Vector2 dir = new Vector2(hori, ver);
        dir = dir.normalized * mSpeed;
        if (hori > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            mRenderer.flipX = true;

        }
        else if (hori < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            //transform.rotation = Quaternion.identity;
            mRenderer.flipX = false;
        }
        else if (ver > 0 || ver < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }

        mRB2D.velocity = dir;
    }
}
