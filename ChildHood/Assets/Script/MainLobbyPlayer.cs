using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyPlayer : MonoBehaviour
{
    public static MainLobbyPlayer Instance;

    public VirtualJoyStick joyskick;

    public int mID;
    public Sprite PlayerImage;
    public eDirection Look;

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
        }
        else
        {
            Delete();
        }
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mID= GameSetting.Instance.PlayerID;
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
            Look = eDirection.Right;
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        else if (hori < 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            Look = eDirection.Left;
            transform.rotation = Quaternion.identity;
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
