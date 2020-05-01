using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private float mDamage;
    [SerializeField]
    private float Tick;
    private bool mDamageOn;
    private bool TrapTrigger;

    private void FixedUpdate()
    {
        if (TrapTrigger==true)
        {
            StartCoroutine("SpikeTrap");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TrapTrigger = true;

        }
    }


    //가시 함정
    IEnumerator SpikeTrap()
    {
        if (mDamageOn==false)
        {
            mPlayer.mCurrentHP -= mDamage;
            mDamageOn = true;
            yield return new WaitForSeconds(Tick);
            mDamageOn = false;
            TrapTrigger = false;

        }
    }
}
