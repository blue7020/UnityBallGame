using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //매우 유용하게 쓰이는 스크립트
    [SerializeField]
    private float mTime;

    private void OnEnable()
    {
        StartCoroutine(Time());
    }

    private IEnumerator Time()
    {
        yield return new WaitForSeconds(mTime);
        gameObject.SetActive(false);
    }
}
