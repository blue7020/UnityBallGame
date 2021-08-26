using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController Instance;
    public Text mStartText,mRankingText,mBestRankingText;
    public Image mTitleImage,mRankingImage;
    public Sprite[] mTitleSprite;

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
        RankingController.Instance.UserID = SaveDataController.Instance.mUser.ID;
        if (SaveDataController.Instance.mUser.Language==1)
        {
            mTitleImage.sprite = mTitleSprite[1];
            mStartText.text = "터치하여 시작";
        }
        else
        {
            mTitleImage.sprite = mTitleSprite[0];
            mStartText.text = "Touch to Start";
        }
    }

    public void UpdateRanking()
    {
        RankingController.Instance.UpdateRecode();
        mRankingImage.gameObject.SetActive(true);
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
            ExitGame();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }

    public void ExitGame()
    {
        SaveDataController.Instance.Save();
        Application.Quit();
    }

    public void ButtonSound()
    {
        SoundController.Instance.SESound(3);
    }
}
