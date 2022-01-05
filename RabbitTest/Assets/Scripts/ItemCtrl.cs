using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCtrl : MonoBehaviour {

    public Transform txtScore, goldScore;      // 프리팹 
    public int Score;
    public Animator anim;
    public bool isGold;

    //-------------------
    // 화면을 벗어난 Gift 제거
    //-------------------
    void Update()
    {
        // World 좌표를 Screen 좌표로 변환 
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);
        if (view.y < -50)
        {
            Destroy(gameObject);
        }
    }
    public void ItemRemove()
    {

        // Gift 제거 
        Destroy(gameObject);
    }


    //-------------------
    // 점수 처리 - 외부 호출 
    //-------------------
    void DisplayScore()
    {
        anim.Play("Effect");
        if (isGold)
        {
            SoundController.Instance.SESound(8);
            int point = Score;
            // 점수 표시용 UIText 출력
            Transform obj = Instantiate(goldScore, transform.position, Quaternion.identity) as Transform;
            obj.GetComponent<Text>().text = "+" + point;
            GameController.Instance.AddGold(point);
            //골드 추가
        }
        else
        {
            SoundController.Instance.SESound(1);
            int point = Score + ((GameController.Instance.mStage + 1) * 10);
            // 점수 표시용 UIText 출력
            Transform obj = Instantiate(txtScore, transform.position, Quaternion.identity) as Transform;
            obj.GetComponent<Text>().text = "+" + point;
            GameController.Instance.AddScore(point);
        }

        // World 좌표를 Viewport 좌표로 변환 
        var pos = Camera.main.WorldToViewportPoint(transform.position);
    }
}
