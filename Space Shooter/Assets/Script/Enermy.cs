using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed, mHoriSpeed;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private BoltPool mBoltPool;
    public BoltPool BoltPool { set { mBoltPool = value; } } //(1)//프로포티는 인스펙터에 안 보이기 때문에 프로포티는 쓰려면 따로 만들어야한다.
    [SerializeField]
    private float mFireRate;
    //(1)과 같은 동작임
    //public void SetBoltPool(BoltPool pool)
    //{
    //    mBoltPool = pool;
    //}
    private EffectPool mEffectPool;
    private GameController mGameController;
    private SoundController mSoundController;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        GameObject effectPool = GameObject.FindGameObjectWithTag("EffectPool");
        mEffectPool = effectPool.GetComponent<EffectPool>();
        mGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }



    private void OnEnable()
    {
        mRB.velocity = Vector3.back * mSpeed;
        StartCoroutine(Movement());

        //InvokeRepeating("Fire",mFireRate,mFireRate);//mFireRate만큼 쉬면서 mFireRate초 만큼 실행
        //invoke는 꺼놔도 실행되야하는 것들에만 사용해야한다.
        StartCoroutine(AutoFire());
        Random.Range(0, 100);
    }


    private IEnumerator AutoFire()
    {
        WaitForSeconds fireRate = new WaitForSeconds(mFireRate);
        while (true)
        {
            yield return fireRate;
            Bolt bolt = mBoltPool.GetFromPool();
            mSoundController.PlayEffectSound(3);
            bolt.transform.position = mBoltPos.position;//월드 좌표값이기에 가능
            bolt.transform.rotation = mBoltPos.rotation;
        }
    }


    private IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1));
            float direction;
            if (transform.position.x < 0)
            {
                //오른쪽
                direction = Random.Range(2f, 3f);
                mRB.velocity += Vector3.right * direction;
            }
            else
            {
                //왼쪽
                direction = Random.Range(-3f, -2f);
                mRB.velocity += Vector3.right * direction;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1));
            //직진
            mRB.velocity -= Vector3.right * direction;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isBolt = other.gameObject.CompareTag("Bolt");
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isBolt || isPlayer)
        {
            gameObject.SetActive(false);

            mGameController.AddScore(2);

            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpEnemy);
            effect.transform.position = transform.position;
            mSoundController.PlayEffectSound(1);
            if (isBolt)
            {
                other.gameObject.SetActive(false);
            }
        }
    }

}
