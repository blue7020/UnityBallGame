using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public bool mTrigger,mIsLoop,mMovingTrigger,mShowBlock,mFallingBlock;
    public GameObject mTriggerObj;
    public Animator mAnim;
    public float mDelay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBolt") &&!mTrigger)
        {
            mAnim.SetBool(AnimHash.On, true);
            SoundController.Instance.SESound(3);
            mTrigger = true;
            Destroy(other.gameObject);
            if (mShowBlock)
            {
                mTriggerObj.gameObject.SetActive(true);
            }
            else
            {
                mTriggerObj.gameObject.SetActive(false);
            }
            if (mIsLoop)
            {
                StartCoroutine(Loop());
            }
            if (mMovingTrigger)
            {
                mTriggerObj.GetComponent<MovingTile>().isMove = true;
            }
            if (mFallingBlock)
            {
                mTriggerObj.GetComponent<FallingObject>().Falling();
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
        if (mShowBlock)
        {
            mTriggerObj.gameObject.SetActive(false);
        }
        else
        {
            mTriggerObj.gameObject.SetActive(true);
        }
        mAnim.SetBool(AnimHash.On, false);
        SoundController.Instance.SESound(4);
        mTrigger = false;
    }
}
