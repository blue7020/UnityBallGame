using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private static readonly int MoveHash = Animator.StringToHash("Move");//이 변수를 알아내려면 게임이 플레이 중이어야하기때문에 const에 넣지 못한다.
    //const는 게임 시작 전에 값을 불러왔어야한다.

    [SerializeField]
    private GaugeBar mGaugeBar;

    [SerializeField]
    private Animator[] mWindowAnimArr;

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
    }

    public void ShowWindow(int id)
    {
        mWindowAnimArr[id].SetTrigger(MoveHash);
    }

    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}", UnitSetter.GetUnitStr(current), UnitSetter.GetUnitStr(max));
        //표준 숫자 서식 문자열
        //"N0"소숫점 자리 안보이게 하는 정수 형태, N형식은 1000단위를 넘어가면 자동으로 쉼표를 찍어준다. 뒤의 숫자는 n번째 소숫점까지 표현한다는 뜻이다.
        //% 표현은 P 형식을 해주면 된다. string progressStr = progress.Tostring("P2");
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress,progressStr);
    }
}
