using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool mCheckPointOn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& mCheckPointOn==false)
        {
            mCheckPointOn = true;
            Player.Instance.CheckPointPos = new Vector3(transform.position.x, 2f,0);
            GameController.Instance.Heal(Player.Instance.mMaxHP - Player.Instance.mCurrentHP);
        }   
    }
}
