using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    protected Rigidbody mRB;
    [SerializeField]
    protected float mSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        //mRB.velocity = new Vector3(0, 0, mSpeed);
        //mRB.velocity = Vector3.forward*mSpeed; //new를 사용하지 않은 벡터는 기본값(0,0,1)이 있기 때문
        ResetDir(); 
    }

    public void ResetDir()
    {
        //자기가 바라보는 방향으로 발사 시 속도 조절
        mRB.velocity = transform.forward * mSpeed;//상대적인 값
    }
}