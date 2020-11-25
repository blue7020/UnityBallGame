using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetProjectile : MonoBehaviour
{

    public float XPos,YPos;
    public bool isMagnet,PlayerDamage;
    public float mDamage;
    public Vector3 Pos;
    public Rigidbody2D mRB2D;
    public Enemy mEnemy;

    private void Awake()
    {
        PlayerDamage = false;
        if (isMagnet==true)
        {
            StartCoroutine(Magnet());
        }
        else
        {
            StartCoroutine(Falling());
        }
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
                    StartCoroutine(RemoveObj());
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

    private IEnumerator Falling()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        int count = 0;
        while (true)
        {
            if (count >= 25)
            {
                PlayerDamage = true;
                Pos = Player.Instance.transform.position-new Vector3(0, YPos,0);
                count++;
                yield return delay;
                if (count >= 26)
                {
                    StartCoroutine(RemoveObj());
                    mRB2D.DOMove(Pos, 0.25f);
                    break;
                }
            }
            else
            {
                Pos = new Vector3(0, YPos, 0);
                gameObject.transform.position = Player.Instance.transform.position + Pos;
                count++;
            }
            yield return delay;
        }
    }

    private IEnumerator RemoveObj()
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
            Player.Instance.LastHitEnemy = mEnemy;
            Player.Instance.DoEffect(6,1f,5);
            gameObject.SetActive(false);
        }
        if (PlayerDamage == true && other.gameObject.CompareTag("MagnetBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
