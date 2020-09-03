using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetProjectile : MonoBehaviour
{

    public float XPos;
    public bool PlayerDamage;
    public float mDamage;
    public Vector3 Pos;
    public Rigidbody2D mRB2D;

    private void Awake()
    {
        PlayerDamage = false;
        StartCoroutine(Magnet());
    }

    private IEnumerator Magnet()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        int count = 0;
        while (true)
        {
            if (count>=30)
            {
                PlayerDamage = true;
                Pos = Player.Instance.transform.position;
                count++;
                yield return delay;
                if (count >= 31)
                {
                    StartCoroutine(removeObj());
                    mRB2D.DOMove(Pos, 0.25f);
                    break;
                }
            }
            else
            {
                Pos = new Vector3(XPos,0,0);
                gameObject.transform.position = Player.Instance.transform.position + Pos;
                count++;
            }
            yield return delay;
        }
    }

    private IEnumerator removeObj()
    {
        WaitForSeconds delay = new WaitForSeconds(0.35f);
        yield return delay;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (PlayerDamage==true&&other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Hit(mDamage);
            Player.Instance.DoEffect(6,1f,5);
            gameObject.SetActive(false);
        }
        if (PlayerDamage == true && other.gameObject.CompareTag("MagnetBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
