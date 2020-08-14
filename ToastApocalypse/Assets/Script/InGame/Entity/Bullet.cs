using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mDamage;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    public eEnemyBulletType eType;
    public Animator mAnim;
    public GameObject[] Spirte;
    public bool DamageOn;

    private void Awake()
    {
        DamageOn = true;
    }

    private void Update()
    {
        if (eType ==eEnemyBulletType.homing)
        {
            StartCoroutine(MovetoPlayer());
        }
        
    }


    public void ShowWarning()
    {
        Spirte[1].gameObject.SetActive(false);
        Spirte[0].gameObject.SetActive(true);
    }
    public void Warning()
    {
        Spirte[0].gameObject.SetActive(false);
        Spirte[1].gameObject.SetActive(true);
    }

    public void Boom()
    {
        DamageOn = true;
        gameObject.SetActive(false);
    }


    private IEnumerator MovetoPlayer()
    {
        WaitForSeconds one = new WaitForSeconds(0.1f);
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        mRB2D.velocity = dir.normalized * mSpeed;
        yield return one;
    }

    public void RemoveBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DamageOn == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player.Instance.Hit(mDamage);
                RemoveBullet();
            }
        }
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            RemoveBullet();
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            if (eType != eEnemyBulletType.boom)
            {
                RemoveBullet();
            }
        }

    }
}
