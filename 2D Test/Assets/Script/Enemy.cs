using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private Player mPlayer;

    [Header("Status")]
    [SerializeField]
    public float mAtk;
    [SerializeField]
    public float mSpeed;
    [SerializeField]
    public float mMaxHP;
    public float mCurrentHP;


    private void Awake()
    {
        mCurrentHP = mMaxHP;
        mAnim = GetComponent<Animator>();
    }

    public void Hit(float value)
    {
        mCurrentHP -= value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            mPlayer.Hit(mAtk);
        }
    }
}
