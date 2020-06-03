using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //이게 없으면 Tween 기능을 사용할 수 없다. 이게 없으면 일반 오브젝트와 똑같음

public class Move : MonoBehaviour
{
    [SerializeField]
    private Transform mDestination;
    [SerializeField]
    private Transform[] mWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //mDestination의 위치로 1초간 이동한다. 
            //Ease는 애니메이션이 어떤 커브값을 가지고 이동하는지를 설정하는 것이다.
            transform.DOMove(mDestination.position, 1).SetEase(Ease.OutBounce).OnComplete(() => { Debug.Log("Crush"); }).OnPlay(() => { Debug.Log("Start"); });
            //뒤에 .OnComplete를 사용하면 델리게이트처럼 메서드를 넣을 수 있다.
            //OnPlay는 시작 OnComplete는 끝날 때 //OnComplete는 한번밖에 사용할 수 없다.
            //OutBounce는 충돌 시 약간 통통 튐
            //속도가 줄지 않고 그대로 가고 싶다면 Linear

            //뒤에 .을 붙여 계속 이어나갈 수 있다

            

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //이동만 할거면 DOMove만 사용하면 되고, 크기를 조작한다거나 여러가지를 하고 싶다면 Sequence를 사용한다.
            List<Vector3> path = new List<Vector3>();
            foreach (Transform t in mWaypoint)
            {
                path.Add(t.position);
            }
            //foreach는 거기에 있는 엘리멘트를 뽑아서 뭘 할때 사용하며 주로 리스트에 쓴다.
            //var는 알아서 타입을 맞춰주기 때문에 되도록이면 쓰지 말자, 코드를 읽을 때 모호성이 생긴다.
            Sequence seq = DOTween.Sequence();
            //DOPath는 Vec3[] 로만 받는다.
            seq.Append(transform.DOPath(path.ToArray(), 4, PathType.CatmullRom)).SetEase(Ease.Linear).Join(transform.DOScale(Vector3.one * 2, 2)).AppendCallback(() => { Debug.Log("ScaleFinish"); }).
                AppendInterval(2).Append(transform.DOScale(Vector3.one * 4, 2));
            //Interval은 딜레이 시간, Join으로는 Interval이 안되기 때문에 별도의 시퀀스를 잡아서 딜레이 시간을 넣어줘야 한다

            //어펜드(시퀀스에 등록 == 붙이는 것) //어펜드 콜백 / Join(동시에) 정도가 기본 기능
            //시퀀스는 순서를 어디에 붙이느냐에 실행 순서가 달라짐

           //PathType.CatmullRom은 곡선움직임(배지어 곡선)
        }
    }
}
