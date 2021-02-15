using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingTile : MonoBehaviour
{
    public bool isMove,isTrap;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    public Transform Pos1, Pos2,mStartPos;
    public Vector3 mNextPos;

    private void Start()
    {
        mNextPos = mStartPos.position;
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            if (transform.position==Pos1.position)
            {
                mNextPos = Pos2.position;
            }
            if (transform.position == Pos2.position)
            {
                mNextPos = Pos1.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, mNextPos, mSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Pos1.position, Pos2.position);
    }

    public IEnumerator MoveDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(mSpeed + 2f);
        isMove = false;
        mRB2D.velocity = Vector2.zero;
        yield return delay;
        isMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !isTrap)
        {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            Player.Instance.mJumpForce = 13;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isTrap)
        {
            Player.Instance.mJumpForce = 10;
            other.gameObject.transform.SetParent(null);
        }
    }
}
