using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates : MonoBehaviour
{

    public delegate void VoidCallback();
    public delegate void IntInVoidRetrun(int input);
    //이렇게 생긴 함수를 넣을 수 있는 자료형을 만든 것.
    //여러 곳에서 쓰기 때문에 이렇게 따로 클래스를 만들어 배치하는 것이 좋다.
}
