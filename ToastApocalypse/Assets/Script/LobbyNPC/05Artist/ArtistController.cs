using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtistController : MonoBehaviour
{
    public static ArtistController Instance;

    public Image mArtistWindow;
    public Text mTitle,mArtTitleText,mButtonText;
    public Button mButton;
    public Sprite[] mArtArr;
    public Sprite[] mIcon;//0은 메인 컷신 / 1은 일반 일러스트

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
            if (GameSetting.Instance.Language == 0)
            {
                mTitle.text = "갤러리";
                mArtTitleText.text = "볼 그림을 선택해주세요";
                mButtonText.text = "보기";
            }
            else if (GameSetting.Instance.Language == 1)
            {
                mTitle.text = "Gallery";
                mArtTitleText.text = "Please select a picture to view";
                mButtonText.text = "View";
            }
            mButton.interactable = false;
            mArtistWindow.gameObject.SetActive(true);
        }
    }
}
