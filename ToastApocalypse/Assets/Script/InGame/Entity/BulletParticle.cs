using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    public float mDamage;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy.mCurrentHP >0)
            {
                if (enemy.isFire==false)
                {
                    enemy.StartCoroutine(enemy.FireDamage(mDamage));
                }
            }
        }
    }
}
