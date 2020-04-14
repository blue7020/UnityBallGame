using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private void OnTriggerExit(Collider other)//해당 공간을 벗어난 모든 오브젝트를 감지해야하니까 모든 콜라이더
    {

        other.gameObject.SetActive(false);
        //other.enabled = false;//타겟이 GameObject가 아닌 이상 Mesh Renderer가 비활성화
        //Destroy(other);//이건 ()안에 들어간 오브젝트의 컴퍼넌트(=콜라이더)를 삭제하는 것이다.
        //Destroy(other.gameObject);
    }
}
