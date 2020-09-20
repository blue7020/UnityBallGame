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
            if (other.gameObject.GetComponent<Enemy>().mCurrentHP >0)
            {
                other.gameObject.GetComponent<Enemy>().Hit(mDamage, 2);
            }
        }
    }
}
