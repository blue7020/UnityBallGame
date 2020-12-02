using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public bool mTrigger,mIsLoop;
    public GameObject mTriggerObj;
    public Animator mAnim;
    public float mDelay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBolt") &&!mTrigger)
        {
            mTrigger = true;
            Destroy(other.gameObject);
            mAnim.SetBool(AnimHash.On, true);
            mTriggerObj.gameObject.SetActive(true);
            if (mIsLoop)
            {
                StartCoroutine(Loop());
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
