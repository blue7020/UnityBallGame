using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Librarian : MonoBehaviour
{
    public static Librarian Instance;
    public Image mWindow, mLobbyGuideWindow;
    public Text mTitle, mTutorialText, mLobbyGuideText;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (GameSetting.Instance.Language==0)//한국어
            {
                mTitle.text = "사서: 무엇이 궁금한가?";
                mTutorialText.text = "게임 방법";
                mLobbyGuideText.text = "로비 기능";
            }
            else if(GameSetting.Instance.Language==1)//영어
            {
                mTitle.text = "Librarian: What do you want to know?";
                mTutorialText.text = "How to play";
                mLobbyGuideText.text = "Lobby function";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoTutorial()
    {
        GameSetting.Instance.NowStage = 0;
        SceneManager.LoadScene(4);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mWindow.gameObject.SetActive(true);
        }
    }
}
