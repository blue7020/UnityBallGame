using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private float mDamage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ouch!");
            mPlayer.mCurrentHP -= mDamage;
        }
    }
}
