using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int mCurrentLife, mMaxLife;
    private EffectPool mEffectPool;
    private GameController mGameController;
    private SoundController mSoundController;
    private UIController mUIController;
    [Header("Fire Bolt")]
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;//GameObject만이 아니라 Transform으로도 같은 효과를 낼 수 있다.
    [SerializeField]
    private float mFireLate;
    private float mCurrentFireLate;
    private int mBoltIndex;
    [SerializeField]
    private int mMaxBoltCount; //max =5
    [SerializeField]
    private float mBoltXGap;
    [SerializeField]
    public int mCurrentBoltCount;
    private Coroutine mBoltChangeRoutine;

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
        GameObject effectPool = GameObject.FindGameObjectWithTag("EffectPool");
        mEffectPool= effectPool.GetComponent<EffectPool>();
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        
    }

    public void Init(GameController GameController, UIController UIController)//Init == 이니셜라이즈
    {
        //게임 컨트롤러가 플레이어를 알고 있고 시작과 동시에 실행시켜 줄 것이다.
        //외부에서 들어온 값을 초기화하게끔 메서드로 빼주는 것이다. == 이게 더 편리할 것임.
        mGameController = GameController;
        mUIController = UIController;
        mCurrentFireLate = mFireLate;
        mCurrentBoltCount = 1;
        mUIController.ShowLife(mCurrentLife);
    }

    // Update is called once per frame
    //기본 조작
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

        //유니티 하이어라키에 만들 수 있는 것들이면 인스턴시에트가 가능하다. ***하이어라키에 만드는 것은 new는 사용하면 안됨
        //GameObject obj = Instantiate(Bolt);//제너릭이라서 타입을 뭘 넣어주냐에 따라 타입이 바뀐다.
        //obj.transform.position = mBoltPos.position;//그냥 .position은 월드 좌표값이 나온다.
        if (Input.GetButton("Fire1") && mCurrentFireLate >= mFireLate)//Axis 세팅에 의해 동작함.
        {
            //알고리즘 - Multi Shot을 사용 시 최대 5개까지 발사할 수 있도록 만드는 알고리즘
            float currentXStart = -mBoltXGap * ((mCurrentBoltCount-1)/2);//왼쪽시작값
            Vector3 Xpos = new Vector3(currentXStart, 0, 0);
            for (int i=0; i<mCurrentBoltCount;i++)
            {
                Bolt bolt = mBoltPool.GetFromPool(mBoltIndex);
                //bolt.transform.position = mBoltPos.position;//어떤 오브젝트의 현재 좌표값을 따르고 싶다면 .localPosition을 해야한다.
                //해당 방향을 바라본다.
                bolt.gameObject.transform.position = mBoltPos.position+ Xpos;
                bolt.gameObject.transform.rotation = mBoltPos.rotation;
                bolt.ResetDir();//속도 갱신(발사 시 플레이어의 뒤에서 발사체가 나오지 않게)
                Xpos.x += mBoltXGap;
            }
            //
            mCurrentFireLate = 0;
            mSoundController.PlayEffectSound((int)eSFXType.FirePlayer);
        }
        else
        {
            mCurrentFireLate += Time.deltaTime;//프레임과 프레임 사이에 전 프레임에서 지금 프레임으로 넘어왔을 때의 시간을 저장한 값이 Time.deltaTime이다. 
        }

    }

    public void StartHoming(float time)
    {
        if (mBoltChangeRoutine != null)
        {
            StopCoroutine(mBoltChangeRoutine);
        }
        mBoltChangeRoutine = StartCoroutine(ChangeBoltID(1, time));

    }

    private IEnumerator ChangeBoltID(int id, float gap)
    {
        mBoltIndex +=id;
        yield return new WaitForSeconds(gap);
        mBoltIndex -=id;
        mBoltChangeRoutine = null;
    }

    public void AddBoltCount()
    {
        if (mCurrentBoltCount<mMaxBoltCount)
        {
            mCurrentBoltCount++;
        }
    }

    public void AddLife()
    {
        if (mCurrentLife<mMaxLife)
        {
            mCurrentLife++;
            mUIController.ShowLife(mCurrentLife);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("EnemyBolt"))
        {
            mCurrentLife--;
            mUIController.ShowLife(mCurrentLife);
            if (mCurrentLife <= 0)
            {
                gameObject.SetActive(false);
                mGameController.PlayerDie();
                mSoundController.PlayEffectSound((int)eSFXType.ExpEnemy);
            }
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpPlayer);
            effect.transform.position = transform.position;

        }
    }
}
