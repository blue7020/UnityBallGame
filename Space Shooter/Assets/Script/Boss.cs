using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int mMaxHP;
    private int mCurrentHP;
    private bool mbInvincible;

    [SerializeField]
    private Vector3 mSpawnStartPos, mSpawnEndPos;
    [SerializeField]
    private float mSpawnTime;
    private EffectPool mEffectPool;
    

    private void OnEnable()
    {
        transform.position = mSpawnStartPos;
        mbInvincible = true;
        mCurrentHP = mMaxHP;
        StartCoroutine(Appear());
    }

    private void Awake()
    {
        mEffectPool = GameObject.FindGameObjectWithTag("EffectPool").GetComponent<EffectPool>();
    }

    private IEnumerator Appear()
    {
        Vector3 StartSize = Vector3.one * 1.5f;
        Vector3 EndSize = Vector3.one * 3f;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float CurrentTime = 0;
        float progress = 0;
        while (CurrentTime <= mSpawnTime)
        {
            //선형 보간 (Linear interpolate)
            transform.position = Vector3.Lerp(mSpawnStartPos, mSpawnEndPos, progress);
            transform.localScale = Vector3.Lerp(StartSize, EndSize, progress);
            CurrentTime += Time.fixedDeltaTime;
            progress = CurrentTime / mSpawnTime;
            
            yield return frame;

        }
        mbInvincible = false;
        //patern

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaa");
        if (other.gameObject.CompareTag("Bolt"))
        {
            if (!mbInvincible)
            {
                mCurrentHP--;
                Debug.LogFormat("{0}/{1}", mCurrentHP, mMaxHP);
                if (mCurrentHP <= 0)
                {
                    mbInvincible = true;
                }
            }
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpAst);
            effect.transform.position = other.ClosestPoint(transform.position);//ClosestPoint = 벡터3(other)에서 가장 가까운 지점
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Die()
    {
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);
        for (int i=0;i<10; i++)
        {
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpEnemy);
            effect.transform.position = Random.insideUnitSphere * 4 + Vector3.up;
            yield return pointThree;
        }
        gameObject.SetActive(false);
        
    }
}
