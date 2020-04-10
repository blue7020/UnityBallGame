using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] //인스펙터에 보여주는 키이다.
    private GameObject mPlayerObj, mCamera;
    private Vector3 mOffset;
    // Start is called before the first frame update
    void Start()
    {
        mPlayerObj = GameObject.FindGameObjectWithTag("Player");// .find 사용 금지 / FindGameObjectsWithTag는 어레이를 찾으니까 헷갈리면 안된다.
        mOffset = transform.position - mPlayerObj.transform.position; //카메라의 위치 설정
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mPlayerObj.transform.position + mOffset;//카메라 위치 계속 갱신
    }
}
