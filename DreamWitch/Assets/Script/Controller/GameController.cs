using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform mStartPoint, mCanvas,mHeartFrameCanvas;
    public Image mHeart,mHeartFrame;
    public List<Image> mPlayerHP;
    public List<Image> mHPFrame;
    public bool Pause,isShowUI;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mPlayerHP = new List<Image>();
            mHPFrame = new List<Image>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetHP(Player.Instance.mCurrentHP);
    }

    public void GamePause()
    {
        if (Pause)
        {
            Time.timeScale = 1;
            Pause = false;
        }
        else
        {
            Time.timeScale = 0;
            Pause = true;
        }
    }


    public void ShowUI()
    {
        if (isShowUI)
        {
            UIController.Instance.mItemBoxImage.gameObject.SetActive(true);
            mHeartFrameCanvas.gameObject.SetActive(true);
            mCanvas.gameObject.SetActive(true);
            isShowUI = false;
        }
        else
        {
            UIController.Instance.mItemBoxImage.gameObject.SetActive(false);
            mHeartFrameCanvas.gameObject.SetActive(false);
            mCanvas.gameObject.SetActive(false);
            isShowUI = true;
        }
    }

    public void SetHP(float count)
    {
        for (int i = 0; i < count; i++)
        {
            mHPFrame.Add(Instantiate(mHeartFrame, mHeartFrameCanvas));
            mPlayerHP.Add(Instantiate(mHeart, mCanvas));
        }
    }
    public void RemoveHealth()
    {
        Destroy(mPlayerHP[0].gameObject);
        mPlayerHP.RemoveAt(0);

        //추가 최대 체력 초기화
        if (Player.Instance.mMaxHP>3)
        {
            for (int i = 0; i < Player.Instance.mMaxHP-3; i++)
            {
                Destroy(mHPFrame[i - 1].gameObject);
                mHPFrame.RemoveAt(i - 1);
            }
        }
    }

    public void Heal(float count)
    {
        for (int i = 0; i < count; i++)
        {
            mPlayerHP.Add(Instantiate(mHeart, mCanvas));
        }
        int rand = Random.Range(11, 13);//Player laugh
        SoundController.Instance.SESound(rand);
        Player.Instance.mCurrentHP += count;
    }

    public void Damege()
    {
        if (mPlayerHP.Count==1)
        {
            GameOver();
        }
        else
        {
            int rand = Random.Range(7, 10);//hitSound
            SoundController.Instance.SESound(rand);
            Destroy(mPlayerHP[mPlayerHP.Count - 1].gameObject);
            mPlayerHP.RemoveAt(mPlayerHP.Count - 1);
        }
    }

    public void GameOver()
    {
        TitleController.Instance.PlayCount -= 1;
        if (TitleController.Instance.PlayCount ==0)
        {
            TitleController.Instance.PlayCount = 3;
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
