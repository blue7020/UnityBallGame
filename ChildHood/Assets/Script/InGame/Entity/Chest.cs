using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //상자 애니메이션 출력

            //상자의 등급에 따라 아이템 배열 다르게
            //하이템의 위치는 상자의 위치로
        }
    }
}
