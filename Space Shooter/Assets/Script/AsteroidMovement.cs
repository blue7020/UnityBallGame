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
        //mRB.angularVelocity = Random.onUnitSphere;//반지름이 1인 구 표면에 있는 랜덤한 지점 A를 꼽아주는 것 = 회전력의 총합은 1
        //더 다양하게 회전하게 만들고 싶다면
        mRB.angularVelocity = Random.insideUnitSphere * mTorque;//이것은 길이가 랜덤하게 할 수 있다. = 회전력의 총합이 일정하지 않다.(0~1 사이에서)

        mRB.velocity = Vector3.back * mSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isBolt = other.gameObject.CompareTag("Bolt");
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isBolt||isPlayer)
        {
            gameObject.SetActive(false);

            

            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpAst);
            effect.transform.position = transform.position;
            mSoundController.PlayEffectSound(1);
            //Add Sound
            if (isBolt)
            {
                mGameController.AddScore(1);
                other.gameObject.SetActive(false);
            }

        }
    }
}
