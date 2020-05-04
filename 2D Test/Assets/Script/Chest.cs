using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private Item mItem;
    private bool mItemSpawn = false;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (!mItemSpawn == true)
            {
                mAnim.SetBool(AnimHash.Open, true);
                mItemSpawn = true;
                mItem.IsItemShow();
            }
            
        }
    }
}
