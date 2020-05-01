using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private float mDamage;
    [SerializeField]
    private float Tick;//애니메이션 비례 함정 대기시간
    public bool mDamageOn;//함정 데미지 발동
    private bool TrapTrigger;//애니메이션 비례 함정 작동
    private bool PlayerOnTrap;//플레이어가 함정 위에 있는가

    private void Update()
    {
        if (TrapTrigger == true)
        {
            StartCoroutine("SpikeTrap");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //아까랑 똑같이 업데이트에서 처리하되 온트리거익스트로 나가면 꺼지게끔!
            TrapTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TrapTrigger = false;
    }

    //가시 함정
    IEnumerator SpikeTrap()
    {
        if (mDamageOn == true)
        {
            mPlayer.mCurrentHP -= mDamage/Tick;
            yield return new WaitForSeconds(Tick);
        }
    }
}
