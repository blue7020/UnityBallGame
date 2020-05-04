using System.Collections;
using System.Collections.Generic;
using TMPro;//외부 플러그인이었기 때문에 TextMeshPro를 사용하려면 이걸 삽입해야한다.
using UnityEngine;
using UnityEngine.UI;


public class ShopController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Button[] mButtonArr;
    [SerializeField]
    private UIElement[] mElementArr;


    private Delegates.IntInVoidRetrun call;
    private Delegates.VoidCallback mCallback;
    [SerializeField]
    private GameController mController;

    // Start is called before the first frame update
    void Start()
    {
        mText.text = "asdf";
        mElementArr[0].Init(0, "공격 증가", "공격력이 1 증가합니다.", 0, 10, LevelUP);
        mElementArr[1].Init(1, "방어 증가", "방어력이 0.1 증가합니다.", 0, 15, LevelUP);
        mElementArr[2].Init(2, "체력 증가", "체력이 1 증가합니다.", 0, 20, LevelUP);

        //주소를 저장해서 나중에 저장한 주소에 있는 값을 실행하는 것.
        //for (int i=0; i < mButtonArr.Length; i++)
        //{
        //mButtonArr[i].onClick.AddListener(()=> { LevelUP(i); });
        //메모리 어딘가에 i에 대해 임시 변수를 만들어놓고, i가 변해도 같은 위치, 같은 값(i)이 존재하기 때문에
        //바뀐 값으로 덮어씌워진다. -람다 함수의 특성으로 인해 반복문으로 원하는 값을 조절할 수 없다.
        //i의 변수를 조작하더라도 같은 값만 나오게 된다.
        //}

    }

    public void ButtonCall()
    {
        if (mCallback!=null)
        {
            mCallback();
        }
    }

    public void ButtonCallAdd()
    {
        mCallback += () =>{Debug.Log("Test!!");};
    }

    public void LevelUP(int id)
    {
        Debug.Log("Level Up: " +id);
    }

    public void GoldSpend1()
    {
        mController.UseGold(1, () => { Debug.Log("Spend 1 Gold"); });
    }

    public void GoldSpend2()
    {
        mController.UseGold(150, () => { Debug.Log("Use 150 Gold"); });
    }

    public void GoldSpend3()
    {
        mController.UseGold(10, () => { Debug.Log("Spend 10 Gold"); });
    }
}
