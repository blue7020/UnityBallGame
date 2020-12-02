using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBolt : MonoBehaviour
{
    public Rigidbody2D mRB2D;
    public float mDamage,mSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
