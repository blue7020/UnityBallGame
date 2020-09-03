using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjAttackArea : MonoBehaviour
{
    public bool TargetSetting;
    public bool XFlip;
    public Enemy mEnemy;
    public SpriteRenderer mRenderer;

    private void Update()
    {
        if (XFlip == true)
        {
            if (mEnemy.transform.rotation.y == 180f)
            {
                mRenderer.flipX = true;

            }
            else
            {
                mRenderer.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TargetSetting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TargetSetting = false;
        }
    }
}
