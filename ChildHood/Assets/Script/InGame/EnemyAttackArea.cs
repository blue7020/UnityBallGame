﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{

    public static EnemyAttackArea Instance;

    public Animator mAnim;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mAnim = GetComponent<Animator>();
    }

    //TODO 이펙트 풀을 사용하여 몬스터에 맞는 공격 스프라이트로 변경

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.mCurrentHP > 0)
            {
                other.gameObject.GetComponent<Player>().Hit(Enemy.Instance.mInfoArr[Enemy.Instance.mID].Atk);
            }


        }
    }
}