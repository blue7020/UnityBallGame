using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D mRB2D;
    private EnemySkill mEnemySkill;
    private float mDamage =1f;
    [SerializeField]
    public float mSpeed;

    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Hit(mDamage);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            gameObject.SetActive(false);
        }
    }
}
