﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TilCtrl : MonoBehaviour {

    public bool Moving;
    public float moveSpeed;
    public SpriteRenderer mRenderer;
    public Sprite[] mSprite;
    int moveFlag = 1;

    private void Start()
    {
        mRenderer.sprite = mSprite[GameController.Instance.mStage];
        moveSpeed = Random.Range((moveSpeed - 0.5f), (moveSpeed+0.5f));
        if (Moving)
        {
            StartCoroutine(MovingTile());
        }
    }
    //-------------------
    // 화면 아래를 벗어나면 제거
    void Update()
    {
        // 월드 좌표를 스크린 좌표로 변환 
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);
        if (view.y < -50)
        {
            if (Moving)
            {
                transform.DOPause();
            }
            Destroy(gameObject);
        }
    }
    //x-4~4까지 이동
    public IEnumerator MovingTile()
    {
        WaitForSeconds time = new WaitForSeconds(moveSpeed);
        if (moveFlag==1)
        {
            transform.DOMoveX(3, moveSpeed);
            moveFlag = 2;
        }
        else
        {
            transform.DOMoveX(-3, moveSpeed);
            moveFlag = 1;
        }
        yield return time;
        StartCoroutine(MovingTile());
    }
}
