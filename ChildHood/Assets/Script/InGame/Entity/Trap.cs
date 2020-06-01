using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Player mPlayer;
    [SerializeField]
    private float mDamage;
    private bool TrapTrigger;//애니메이션 비례 함정 작동
    private bool PlayerOnTrap;//플레이어가 함정 위에 있는가

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.Instance.pause==false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TrapTrigger = true;
                mPlayer = other.GetComponent<Player>();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TrapTrigger = false;
        mPlayer = null;
    }

    public void Damage()
    {
        if(mPlayer!= null)
        {
            mPlayer.Hit(mDamage);
        }
    }
}
