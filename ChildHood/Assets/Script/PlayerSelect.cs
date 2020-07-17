﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private int mID;
#pragma warning restore 0649
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            PlayerSelectController.Instance.CharaChange(mID);
        }
    }
}
