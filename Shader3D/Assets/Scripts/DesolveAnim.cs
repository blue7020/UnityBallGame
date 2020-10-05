using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolveAnim : MonoBehaviour
{
    private Material mMat;

    private Coroutine mAnimRoutine;

    private void Start()
    {
        mMat = GetComponent<Renderer>().material;
    }

    public void Desolve(float time)
    {
        if (mAnimRoutine==null)
        {
            mAnimRoutine = StartCoroutine(DesolveRoutine(time));
        }
    }

    public IEnumerator DesolveRoutine(float time)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float currentTime = 0;
        while (currentTime<time)
        {
            yield return frame;
            currentTime += Time.fixedDeltaTime;
            mMat.SetFloat("_Desolve",currentTime/time);//0~1사이의 값
        }
        mAnimRoutine = null;
    }
}
