﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyCamera : MonoBehaviour
{

    public static MainLobbyCamera Instance;
    [SerializeField]
    private GameObject mPlayerObj, mCamera;
    private Vector3 mOffset;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mPlayerObj = GameObject.FindGameObjectWithTag("Player");// .find 사용 금지 / FindGameObjectsWithTag는 어레이를 찾으니까 헷갈리면 안된다.
            mOffset = transform.position - mPlayerObj.transform.position; //카메라의 위치 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = mPlayerObj.transform.position + mOffset;
    }

}