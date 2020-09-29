using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelStatue : MonoBehaviour
{
    public static AngelStatue Instance;
    public Image mWindow;
    public Text TitleText, DonateText;
    public Button DonateButton;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(false);
        }
    }

    public void Donate()
    {
        //1달러 기부받기
        //1번 후원 시 후원자 전용 캐릭터 / 스킬 지급
        //2번 후원 시 출시 보상인 핑크 도넛과 돌진 스킬을 얻지 못했다면 지급
        //3번 후원 시 로비의 책 옆에 감사패 생성
    }
}
