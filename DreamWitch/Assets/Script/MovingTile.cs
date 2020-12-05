using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingTile : MonoBehaviour
{
    public bool isMove,isDown, isUp;
    public int mDir;
    public float mTime;
    public Rigidbody2D mRB2D;
    public Vector2 mMinPos, mMaxPos;

    private void FixedUpdate()
    {
        if (isMove)
        {
                StartCoroutine(MoveDelay());
            switch (mDir)
            {
                case 0:
                    mRB2D.DOMove(mMinPos, mTime);
                    mDir = 1;
                    break;
                case 1:
                    mRB2D.DOMove(mMaxPos, mTime);
                    mDir = 0;
                    break;
            }
        }
    }

    public IEnumerator MoveDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(mTime + 2f);
        isMove = false;
        yield return delay;
        isMove = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.transform.SetParent(transform);
            other.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.transform.SetParent(null);
        }
    }
}
