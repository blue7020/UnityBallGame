using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBolt : Bolt //Bolt도 :MonoBehaviour 이기 때문에 Bolt를 상속받아도 문제 없이 사용 가능하다!!
{
    [SerializeField]
    private string mTargetTag;//태그를 비교해서 적이면 Player를, 플레이어면 Enemy를 지목한다.

    private void OnEnable()
    {
        StartCoroutine(Guide());
    }


    private IEnumerator Guide()
    {
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);//0.3초마다 타겟의 좌표 찾기
        while (true)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(mTargetTag);//태그가 중복되면 가장 가까운 태그를 지목한다.
            if (obj!=null)//obj가 있을때만 작동
            {
                Vector3 pos = obj.transform.position;
                Vector3 direction = pos - transform.position;//방향벡터 = 목적지 - 나
                direction = direction.normalized;
                mRB.velocity = direction * mSpeed;
                transform.LookAt(pos);//해당 방향으로 향한다.
            }
            
            yield return pointThree;//값을 찾았기 때문에 동작을 휴식한다.
            //이걸 if문 안에 넣으면 찾지 못했을 때는 무한 반복되어 게임이 꺼지지 않는다!
        }
    }
}
