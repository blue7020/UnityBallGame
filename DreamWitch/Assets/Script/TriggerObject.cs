using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public bool mTrigger,mIsLoop,mMovingTrigger;
    public int mMovingDirCode;
    public GameObject mTriggerObj;
    public Animator mAnim;
    public float mDelay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBolt") &&!mTrigger)
        {
            mAnim.SetBool(AnimHash.On, true);
            mTrigger = true;
            Destroy(other.gameObject);
            mTriggerObj.gameObject.SetActive(true);
            if (mIsLoop)
            {
                StartCoroutine(Loop());
            }
            if (mMovingTrigger)
            {

                mTriggerObj.GetComponent<MovingTile>().mDir = mMovingDirCode;
                mTriggerObj.GetComponent<MovingTile>().isMove = true;
            }
        }
    }

    public IEnumerator Loop()
    {
        float time = mDelay / 6;
        SpriteRenderer renderer = mTriggerObj.GetComponent<SpriteRenderer>();
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
        yield return delay;
        renderer.color = new Vector4(1,1,1,0.3f);
        yield return delay;
        renderer.color = Color.white;
        yield return delay;
        renderer.color = new Vector4(1, 1, 1, 0.3f);
        yield return delay;
        renderer.color = Color.white;
        yield return delay;
        mTriggerObj.gameObject.SetActive(false);
        mAnim.SetBool(AnimHash.On, false);
        mTrigger = false;
    }
}
