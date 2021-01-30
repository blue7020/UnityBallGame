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
            if (Player.Instance.isReset==false)
            {
                int rand = Random.Range(11, 13);//Player laugh
                SoundController.Instance.SESound(rand);
            }
            GameController.Instance.Heal(Player.Instance.mMaxHP - Player.Instance.mCurrentHP);
        }   
    }

    public void ResetCheckPoint()
    {
        if (mCheckPointOn)
        {
            mCheckPointOn = false;
            mAnim.SetBool(AnimHash.CheckPoint, false);
        }
    }
}
