using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    public const float TIME = 0.5f;
    public float mCurrentTime,mRespawnTime;
    public Animator mAnim;
    public bool isRespawn;
    public Vector3 StartPos;
    public GameObject mTile, mSilhouette;

    private void Awake()
    {
        isRespawn = false;
        StartPos = transform.position;
        mCurrentTime = TIME;
    }

    public IEnumerator ReSpawning()
    {
        WaitForSeconds delay = new WaitForSeconds(mRespawnTime);
        isRespawn = true;
        mSilhouette.gameObject.SetActive(true);
        mTile.gameObject.SetActive(false);
        yield return delay;
        mAnim.SetBool(AnimHash.Falling, false);
        isRespawn = false;
        mCurrentTime = TIME;
        mTile.transform.position = StartPos;
        mSilhouette.gameObject.SetActive(false);
        mTile.gameObject.SetActive(true);
    }

    public void Respawn()
    {
        StartCoroutine(ReSpawning());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!isRespawn)
        {
            mAnim.SetBool(AnimHash.Falling, true);
        }
    }
}
