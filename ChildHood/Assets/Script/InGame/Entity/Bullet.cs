using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBulletType
{
    normal,
    homing
}

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float mDamage;
    [SerializeField]
    public float mSpeed;
    public Rigidbody2D mRB2D;
    [SerializeField]
    public eBulletType Type;

    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Type ==eBulletType.homing)
        {
            StartCoroutine(MovetoPlayer());
        }
        
    }

    private IEnumerator MovetoPlayer()
    {
        WaitForSeconds one = new WaitForSeconds(0.1f);
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        mRB2D.velocity = dir.normalized * mSpeed;
        yield return one;
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
