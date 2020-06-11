using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody2D mRB2D;
    private EnemySkill mEnemySkill;
    private float mDamage =1f;
    [SerializeField]
    protected float mSpeed;
    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        ResetDir();
    }

    public void ResetDir()
    {
        mRB2D.velocity = transform.forward * mSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            Player.Instance.Hit(mDamage);
            Destroy(gameObject);
        }
    }
}
