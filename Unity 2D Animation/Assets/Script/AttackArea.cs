﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    private float mDamage;
    public void SetDamage(float value)
    {
        mDamage = value;
        EnteredArr=new List<GameObject>();
    }

    List<GameObject> EnteredArr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //한 대상만 때리기
            //if (!EnteredArr.Contains(collision.gameObject))
            //{
            //    EnteredArr.Add(collision.gameObject);
            //}
            //EnteredArr[0].SendMessage("Hit", mDamage);
            //if (!EnteredArr[0].activeInHierarchy)
            //{
            //    EnteredArr.RemoveAt(0);
            //}
            Debug.Log("Attack Enemy" + mDamage);
        }
    }
}
