using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool mCheckPointOn;
    public Animator mAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& mCheckPointOn==false)
        {
            mCheckPointOn = true;
            mAnim.SetBool(AnimHash.CheckPoint, true);
            UIController.Instance.CheckPointSet();
            Player.Instance.CheckPointPos = new Vector3(transform.position.x, transform.position.y+1f, 0);
            GameController.Instance.Heal(Player.Instance.mMaxHP - Player.Instance.mCurrentHP);
        }   
    }
}
