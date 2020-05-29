using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField]
    private GameObject mPlayerObj, mCamera;
    private Vector3 mOffset;
    // Start is called before the first frame update
    void Start()
    {
        mPlayerObj = GameObject.FindGameObjectWithTag("Player");// .find 사용 금지 / FindGameObjectsWithTag는 어레이를 찾으니까 헷갈리면 안된다.
        mOffset = transform.position - mPlayerObj.transform.position; //카메라의 위치 설정
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = mPlayerObj.transform.position + mOffset;
    }

}
