using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperObject : MonoBehaviour
{
    public Animator mAnim;
    public int mDir; //up:0, down:1, right:2, left:3
    public bool isCooltime;
    public float mJumpForce;

    public IEnumerator Jump(Player player)
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        isCooltime = true;
        mAnim.SetBool(AnimHash.On, true);
        switch (mDir)
        {
            case 0:
                player.mRB2D.velocity = new Vector2(player.mRB2D.velocity.x, mJumpForce);
                break;
            case 1:
                player.mRB2D.velocity = new Vector2(player.mRB2D.velocity.x, -mJumpForce);
                break;
            //case 2:
            //    player.mRB2D.velocity = new Vector2(player.mRB2D.velocity.x * mJumpForce, player.mRB2D.velocity.y);
            //    break;
            //case 3:
            //    player.mRB2D.velocity = new Vector2(-player.mRB2D.velocity.x * mJumpForce, player.mRB2D.velocity.y);
            //    break;
            default:
                break;
        }
        //띠용 사운드
        yield return delay;
        isCooltime = false;
        mAnim.SetBool(AnimHash.On, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!isCooltime)
        {
            Player player = other.gameObject.GetComponent<Player>();
            StartCoroutine(Jump(player));
        }
    }
}
