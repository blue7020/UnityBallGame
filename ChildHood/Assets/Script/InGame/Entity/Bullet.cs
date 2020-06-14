using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float mDamage;
    [SerializeField]
    public float mSpeed;
    public Rigidbody2D mRB2D;

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
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            gameObject.SetActive(false);
        }
    }
}
