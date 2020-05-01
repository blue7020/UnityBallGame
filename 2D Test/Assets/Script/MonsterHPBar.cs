using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    public Image mHPBar;
    private Enemy mEnemy;
    //private float mNowHP;
    //private float mNowMaxHP;

    private void Awake()
    {
        mHPBar = GetComponent<Image>();
    }
    private void FixedUpdate()
    //해야할 것: 몬스터 머리 위에 hp바 표시
    {
        transform.position = mEnemy.transform.position;
    }
}
