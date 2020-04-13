using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        //mRB.velocity = new Vector3(0, 0, mSpeed);
        //mRB.velocity = Vector3.forward*mSpeed; //new를 사용하지 않은 벡터는 기본값(0,0,1)이 있기 때문
        mRB.velocity = transform.forward*mSpeed; //상대적인 값
    }
}