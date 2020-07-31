using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mDamage;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    public eEnemyBulletType Type;
    public Animator mAnim;
    public GameObject[] Spirte;
    public bool DamageOn;

    private void Awake()
    {
        DamageOn = true;
    }

    private void Update()
    {
        if (Type ==eEnemyBulletType.homing)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DamageOn == true)
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
}
