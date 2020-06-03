using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIMove : MonoBehaviour
{
    [SerializeField]
    private Transform[] mMovingObjArr;
    [SerializeField]
    private Transform mDest;
    [SerializeField]
    private float mInterval = 0.5f;
    [SerializeField]
    private Text mText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void StartMove()
    {
        for (int i = 0; i < mMovingObjArr.Length; i++)
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(i*mInterval).Append(mMovingObjArr[i].DOMove(mDest.position, 2)).
                Join(mMovingObjArr[i].DOScale(Vector3.one * 2, 2)).AppendCallback(mMovingObjArr[i].SetAsLastSibling).Append(mMovingObjArr[i].DOScale(Vector3.one, 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//숫자키 1
        {
            StartMove();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //텍스트 한글자씩 출력(data에 들어있는 값으로 텍스트를 변환하여 출력)
            string data = "abcdefg";
            mText.text = "";//전의 텍스트 위에 덮어쓰기 때문에 텍스트를 출력하기 전에 빈 문자열로 만들어야한다.
            mText.DOText(data, 2, scrambleMode: ScrambleMode.All).SetEase(Ease.Linear);//디폴트 패러미터 ScrambleMode 사용하고 싶을 때
        }
    }
}
