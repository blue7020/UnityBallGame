using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperObject : MonoBehaviour
{
    public Animator mAnim;
    public bool isCooltime;
    public float mJumpForce;

    public IEnumerator Jump(Player player)
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        isCooltime = true;
        mAnim.SetBool(AnimHash.On, true);
        player.mRB2D.velocity = Vector2.up * mJumpForce;
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
