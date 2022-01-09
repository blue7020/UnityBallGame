using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TilCtrl : MonoBehaviour {

    public bool Moving;
    public bool Breaking;
    public float moveSpeed;
    public SpriteRenderer mRenderer;
    public Animator mAnim;
    int moveFlag = 1;
    public float MovingTilePos;

    private void Start()
    {
        if (!SaveDataController.Instance.mUser.isFolderble)
        {
            MovingTilePos = SaveDataController.Instance.mMovingTileNormalPos;
        }
        else
        {
            MovingTilePos = SaveDataController.Instance.mMovingTileFolderblePos;
        }
        if (GameController.Instance.mStage == 0)
        {
            mAnim.Play("Normal");
        }
        else
        {
            string st = (GameController.Instance.mStage + 1).ToString();
            mAnim.Play("Stage" + st);
        }
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
            transform.DOMoveX(MovingTilePos, moveSpeed);
            moveFlag = 2;
        }
        else
        {
            transform.DOMoveX(-MovingTilePos, moveSpeed);
            moveFlag = 1;
        }
        yield return time;
        StartCoroutine(MovingTile());
    }

    public void BreakPlatform()
    {
        if (Breaking)
        {
            mAnim.Play("Break");
            SoundController.Instance.SESound(6);
            Invoke("RemoveObj", 0.5f);

        }
    }

    public void RemoveObj()
    {
        Destroy(gameObject);
    }
}
