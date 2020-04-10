using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField]
    private Vector3 mTumble;
    private Vector3 mRealTumble;
    private Player mPlayer;

    private void Start()
    {
        //공식과 결과값이 고정되어있으면 스타트에 넣는 것이 좋다.
        mRealTumble = mTumble * Time.fixedDeltaTime; //Time.fixedDeltaTime == 고정 프레임 타임  프레임은 1/50초 
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");//태그 찾기
        mPlayer = playerObj.GetComponent<Player>();//playerObj의 컴포넌트 찾기
    }

    private void FixedUpdate()//고정 프레임
    {
        transform.Rotate(mRealTumble);
    }

    //private void Update()
    //{
    //    transform.Rotate(mTumble * Time.deltaTime); //Time.deltaTime == 유동 프레임 타임 
    //}

    //둘 중 하나의 오브젝트에 Rigidbody가 있어야 작동한다.
    //Enter=겹침 혹은 충돌 시 / Exit= 겹쳐있다가 겹치지 않을 때, 혹은 충돌하고 떨어질 때 / Stay=겹쳐져 있거나 충돌해 있을 때
    private void OnTriggerEnter(Collider other) //겹침
    {
        //둘 중 하나 이상 트리거일 때 인식


        if(other.gameObject.CompareTag("Player"))//태그 비교로 태그가 Player일 때 작동
        {
            mPlayer.AddScore();
            gameObject.SetActive(false);
        }
        
    }

    //private void OnCollisionEnter(Collision collision)//충돌
    //{
    //    //둘다 콜리전일 때 인식

    //}
}
