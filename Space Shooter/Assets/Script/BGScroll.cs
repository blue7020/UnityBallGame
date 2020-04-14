﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BGTrigger"))//자기들끼리 충돌하면 안되기 때문에 비교해준다.
        {
            transform.position += new Vector3(0, 0, 40.96f);
        }
    }
}
