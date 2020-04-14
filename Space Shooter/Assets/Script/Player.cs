using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Fire Bolt")]
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;//GameObject만이 아니라 Transform으로도 같은 효과를 낼 수 있다.
    [SerializeField]
    private float mFireLate;
    private float mCurrentFireLate;

    private Rigidbody mRB;
    [Header("Movement")]
    [SerializeField]//인스펙터에 인스턴스 칸 추가
    private float mSpeed;
    [SerializeField]
    private float mXMax, mXMin, mZMax, mZMin;
    [SerializeField]
    private float mTilted = 30;

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mCurrentFireLate = mFireLate;
    }


    // Update is called once per frame
    void Update()
    {
        //이동
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        //방향벡터
        direction = direction.normalized;//대각선으로 갔을 때도 상하좌우와 같게 이동값을 1로 유지하는 것
        
        mRB.velocity = direction * mSpeed;
        //transform.Translate(direction * Time.deltaTime);//프레임에 따라 이동 속도를 조절하기 위해

        mRB.rotation = Quaternion.Euler(0, 0, horizontal * -mTilted);//Quaternion(사원소) == 벡터4라고 생각하면 된다. 회전을 할 때 값이 4개여야 하기 때문.

        //transform.position과 기능은 같지만 rigidbody.position을 권장함
        mRB.position = new Vector3(Mathf.Clamp(mRB.position.x, mXMin, mXMax),
                                  0,
                                  Mathf.Clamp(mRB.position.z, mZMin, mZMax));//Clamp는 Value의 최대값과 최솟값의 사이의 값을 가진다.

        //사격
        //if (Input.GetKey(KeyCode.Space))//GetKeyDown은 키를 누르는 시점, GetKeyUp은 키에서 손을 뗀 시점, GetKey는 누르고 있는 동안
        //{

        if (Input.GetButton("Fire1") && mCurrentFireLate >= mFireLate)//Axis 세팅에 의해 동작함.
        {
            //유니티 하이어라키에 만들 수 있는 것들이면 인스턴시에트가 가능하다. ***하이어라키에 만드는 것은 new는 사용하면 안됨
            //GameObject obj = Instantiate(Bolt);//제너릭이라서 타입을 뭘 넣어주냐에 따라 타입이 바뀐다.
            //obj.transform.position = mBoltPos.position;//그냥 .position은 월드 좌표값이 나온다.

            Bolt bolt = mBoltPool.GetFromPool();
            bolt.transform.position = mBoltPos.position;//어떤 오브젝트의 현재 좌표값을 따르고 싶다면 .localPosition을 해야한다.
            mCurrentFireLate = 0;
        }
        else
        {
            mCurrentFireLate += Time.deltaTime;//프레임과 프레임 사이에 전 프레임에서 지금 프레임으로 넘어왔을 때의 시간을 저장한 값이 Time.deltaTime이다. 
        }
    }
}
