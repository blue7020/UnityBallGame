using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mMessageText, mRestartText;
    //인스펙터가 연결된 변수의 이름을 바꾸면 연결이 끊어지며, 자료형이 바뀌어도 끊어진다.
    //만일 변수명을 바꿨으면 재연결 작업부터 먼저 해야한다.


    //UI에는 연산적인 기능을 넣지 않는 것이 좋다.

    public void ShowScore(int amout)
    {
        mScoreText.text = "Score: " + amout.ToString();
    }

    public void ShowMessageText(string data)
    {
        mMessageText.text = data;
    }

    public void ShowRestart(bool isActive)
    {
        mRestartText.gameObject.SetActive(isActive);
    }
}
