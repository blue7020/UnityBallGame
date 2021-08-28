using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController Instance;
    public Text mStartText,mRankingText,mBestRankingText;
    public Image mTitleImage,mRankingImage,mTitleBGImage;
    public Sprite[] mTitleSprite,mTitleBGSprite;
    public int TitleClickCount;
    public bool suprise;

    float vol;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            vol = SoundController.Instance.mBGM.volume;
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
        TitleClickCount = 0;
        suprise = false;
        mTitleBGImage.sprite = mTitleBGSprite[0];
    }

    public void UpdateRanking()
    {
        RankingController.Instance.UpdateRecode();
        mRankingImage.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        SoundController.Instance.mBGM.volume = vol;
        SoundController.Instance.mSE2.mute = true;
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
        SoundController.Instance.mBGM.volume = vol;
        SoundController.Instance.mSE2.mute = true;
        SaveDataController.Instance.Save();
        Application.Quit();
    }

    public void ButtonSound()
    {
        SoundController.Instance.SESound(3);
    }

    public void TitleClick()
    {
        if (!suprise)
        {
            if (TitleClickCount < 10)
            {
                TitleClickCount++;
            }
            else
            {
                StartCoroutine(ShowSomething());
            }
        }
    }

    public IEnumerator ShowSomething()
    {
        WaitForSecondsRealtime time = new WaitForSecondsRealtime(5.5f);
        TitleClickCount = 0;
        mTitleBGImage.sprite = mTitleBGSprite[1];
        float vol = SoundController.Instance.mBGM.volume;
        SoundController.Instance.mBGM.volume = 0f;
        SoundController.Instance.mSE2.mute = false;
        SoundController.Instance.SE2Sound(0);
        yield return time;
        SoundController.Instance.mBGM.volume = vol;
        mTitleBGImage.sprite = mTitleBGSprite[0];
        suprise = false;
    }
}
