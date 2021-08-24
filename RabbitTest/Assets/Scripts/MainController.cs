using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController Instance;
    public Text mHighScoreText,mStartText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveDataController.Instance.LoadGame();
        if (SaveDataController.Instance.mUser.Language==1)
        {
            mStartText.text = "터치하여 시작";
            mHighScoreText.text = "최고 점수 : " + SaveDataController.Instance.mUser.HighScore.ToString();
        }
        else
        {
            mStartText.text = "Touch to Start";
            mHighScoreText.text = "High Score : " + SaveDataController.Instance.mUser.HighScore.ToString();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    int ClickCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            Application.Quit();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
