using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mClearText;
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed = 5;
    [SerializeField]
    private int mScore;

    // Start is called before the first frame update
    void Start()//컨스트럭터의 기능
    {
        mScore = 0;
        mRB = GetComponent<Rigidbody>();//리깃 바디를 찾아옴
        //mRB = gameObject.GetComponent<Rigidbody>();를 축약해 사용한 것임
        //만약 다른 오브젝트에서 컴퍼넌트를 가져오는 것이면 gameObject를 가져오려는 오브젝트의 이름으로 지정한다.

        //GetComponent는 컴퍼넌트에 붙어 있는 것의 값을 가져오기 때문에 new를 사용하지 않는다.
        //컴퍼넌트의 인스턴스를 잘못 가져오면 오류가 뜬다.

        mScoreText.text = "Score: " + mScore.ToString();
        mClearText.text = "";

        

        //gameObject;//현재 스크립트가 붙어 있는 이 컴퍼넌트의 gameObject 카테고리(상단)
        //GameObject;//스트럭트
        //transform;//현재 스크립트가 붙어 있는 이 컴퍼넌트의 transform 카테고리
        //Transform;
    }

    public void AddScore()
    {
        mScore++;
        mScoreText.text = "Score: " + mScore.ToString();
        if (mScore >=4)
        {
            mClearText.text = "Game Clear!";
        }
        //UI 출력
    }

    // Update is called once per frame
    void Update()//언제 어떤 시점에서 해야할 지 모르겠는 것(ex 이동 조작)은 업데이트에서 처리한다.
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //GetAxis는 유저의 입력을 통해 -1 ~ 1 내의 값을 인지해 받아온다.

        //Log.Format == String.Format을 압축해놓은 것
        Vector3 axis = new Vector3(horizontal,0,vertical);//Y로는 움직이지 않기 때문에 0
        //Stack 영역에 만들어지기 때문에 종료되면 값이 초기화된다.
        //Vector3 ax = axis;//이럴 경우는 값을 복사한다.

        //크기가 항상 -1 혹은 1로 잡아주기 위해 사용함.
        axis = axis.normalized *5;
        axis.y = mRB.velocity.y;
        //float velY=mRB.velocity,y;(1)

        //mRB.AddForce(axis);//조작을 어렵게 하고 싶으면 AddForce를 사용하면 된다. 현실 물리를 적용시키기 때문에 가중치가 붙어 수치 변동이 즉각 변하지 않기 때문.
        mRB.velocity = axis;//Velocity = 속도
        //Y값을 주기 때문에 axis에다가 배속을 주지 않는다. 만약 점프 기능이 있다면 점프의 속도가 배속이 되기 때문에.

        //mRB.celocity.y = velY;//(1)접근한 것에서 한 값을 바꾸는 것은 안되고 전체를 바꾸는 것만 허용하기 때문.
    }
}
