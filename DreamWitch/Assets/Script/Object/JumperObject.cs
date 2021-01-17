using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperObject : MonoBehaviour
{
    public Animator mAnim;
    public int mDir; //up:0, down:1, right:2, left:3
    public bool isCooltime;
    public float mJumpForce;

    public FallingTile mTile;

    public IEnumerator Jump(Player player)
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        isCooltime = true;
        mAnim.SetBool(AnimHash.On, true);
        player.isMultipleJump = true;
        player.mJumpToken = player.mMaxmJumpToken;
        switch (mDir)
        {
            case 0:
                player.mRB2D.velocity = new Vector2(player.mRB2D.velocity.x, mJumpForce);
                break;
            case 1:
                player.mRB2D.velocity = new Vector2(player.mRB2D.velocity.x, -mJumpForce);
                break;
            case 2:
                player.mRB2D.velocity = new Vector2(mJumpForce, mJumpForce/2);
                break;
            case 3:
                player.mRB2D.velocity = new Vector2(-mJumpForce, mJumpForce/2);
                break;
            default:
                break;
        }
        SoundController.Instance.SESound(18);
        yield return delay;
        if (mTile != null)
        {
            mTile.mAnim.SetBool(AnimHash.Falling, true);
        }
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
