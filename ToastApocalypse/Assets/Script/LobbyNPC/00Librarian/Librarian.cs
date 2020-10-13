using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Librarian : MonoBehaviour
{
    public static Librarian Instance;
    public Image mWindow;
    public Text mTitle, mTutorialText, mLobbyGuideText;

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

    private void Start()
    {
        if (GameSetting.Instance.Language == 0)//한국어
        {
            mTitle.text = "사서: 무엇을 하고 싶은가?";
            mTutorialText.text = "게임 방법";
            mLobbyGuideText.text = "오프닝\n다시 보기";
        }
        else if (GameSetting.Instance.Language == 1)//영어
        {
            mTitle.text = "Librarian: What do you want to do?";
            mTutorialText.text = "How to play";
            mLobbyGuideText.text = "Replay\nopening";
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
