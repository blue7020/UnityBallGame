using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    public Enemy mEnemy;
    public float mDamage,mSpeed;
    public Rigidbody2D mRB2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Damage(mDamage);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("BoltTrasher"))
        {
            gameObject.SetActive(false);
        }
    }
}
