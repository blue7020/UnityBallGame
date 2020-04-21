using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //이게 있어야 UI에 관련된 기능을 사용할 수 있다.

public class UIController : MonoBehaviour
{
    //인스펙터가 연결된 변수의 이름을 바꾸면 연결이 끊어지며, 자료형이 바뀌어도 끊어진다.
    //만일 변수명을 바꿨으면 재연결 작업부터 먼저 해야한다.
    //UI에는 연산적인 기능을 넣지 않는 것이 좋다.
    [SerializeField]
    private Text mScoreText, mWaveText, mMessageText, mRestartText;
    [SerializeField]
    private GameObject[] mLifeObjArr;

    private GameController mGameController;

    public void ShowLife(int life)//현재 life값
    {
        for (int i = 0; i<mLifeObjArr.Length;i++)
        {
            if (i<life)
            {
                mLifeObjArr[i].SetActive(true);
            }
            else
            {
                mLifeObjArr[i].SetActive(false);
            }
        }
    }

    public void ShowWave(int amount)
    {
        mWaveText.text = "Wave: " + amount.ToString();
    }
    public void ShowScore(int amount)
    {
        mScoreText.text = "Score: " + amount.ToString();
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
