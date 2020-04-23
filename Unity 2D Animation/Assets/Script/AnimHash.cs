using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash
{
    //몬헤비어를 상속하지 않아도 유징 유니티 엔진덕분에 기능은 다 쓸 수 있다.
    //컨스턴트 변수를 담아내는 스크립트

    public static readonly int Attack = Animator.StringToHash("IsAttack");
    //readonly는 할당은 되지만 외부에서 수정하지 못하게 읽기 전용으로 바꿔줌
    //const는 컴파일 되는 시점에 이 값이 뭔지 알고 있어야 한다. 하지만 위의 것은 런타임(실행 시 할당됨)이기 때문에 안된다.
    public static readonly int Dead = Animator.StringToHash("IsDead");
    public static readonly int Walk = Animator.StringToHash("IsWalk");
}
