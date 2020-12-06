using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBolt : MonoBehaviour
{
    public Rigidbody2D mRB2D;
    public float mDamage,mSpeed,mLifeTime;

    private void Awake()
    {
        StartCoroutine(LifeTime());
    }

    public IEnumerator LifeTime()
    {
        WaitForSeconds delay = new WaitForSeconds(mLifeTime);
        yield return delay;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
