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
    public GameObject Medal;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            ShowMedal();
            if (GameSetting.Instance.Language==0)
            {
                TitleText.text = "과거에 전쟁의 승리를 기원하던 제단...\n기부하면 좋은 일이 일어날 지도 모른다.";
                DonateText.text = "기부한다";
            }
            else if (GameSetting.Instance.Language==1)
            {
                TitleText.text = "An altar that wished for victory in war in the past...\nGood things may happen if you donate.";
                DonateText.text = "Donate";
            }
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
            mWindow.gameObject.SetActive(true);
        }
    }

    public void ShowMedal()
    {
        if (SaveDataController.Instance.mUser.DonateCount>=3)
        {
            Medal.SetActive(true);
        }
    }

    public void Donate()
    {
        //1달러 기부받기
        //1번 후원 시 후원자 전용 캐릭터 / 스킬 지급
        //2번 후원 시 출시 보상인 핑크 도넛과 돌진 스킬을 얻지 못했다면 지급
        //3번 후원 시 로비의 책 옆에 감사패 생성
        ShowMedal();
    }
}
