using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mTorque;//토크(Torque): 회전력(=각속도)
    [SerializeField]
    private float mSpeed;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //mRB.angularVelocity = Random.onUnitSphere;//반지름이 1인 구 표면에 있는 랜덤한 지점 A를 꼽아주는 것 = 회전력의 총합은 1
        //더 다양하게 회전하게 만들고 싶다면
        mRB.angularVelocity = Random.insideUnitSphere * mTorque;//이것은 길이가 랜덤하게 할 수 있다. = 회전력의 총합이 일정하지 않다.(0~1 사이에서)

        mRB.velocity = Vector3.back * mSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolt")||
            other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //Add score
            //Add Efect
            //Add Sound
            other.gameObject.SetActive(false);
        }
    }
}
