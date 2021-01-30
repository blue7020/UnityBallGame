using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public MapMaterialController[] mMapMaterialControllerArr;
    public MapMaterialController mMapMaterialController;

    public GameObject[] mStageObject;
    public Transform[] mStartPointArr;

    public Player mPlayer;

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
            mStageObject[TitleController.Instance.NowStage].SetActive(true);
            mMapMaterialController = mMapMaterialControllerArr[TitleController.Instance.NowStage];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mStartPoint = mStartPointArr[TitleController.Instance.NowStage];
        mPlayer.CheckPointPos = mStartPoint.transform.position + new Vector3(0, 2f, 0);
        mPlayer.gameObject.SetActive(true);
        switch (TitleController.Instance.NowStage)
        {
            case 0:
                mPlayer.transform.position = new Vector3(-32, 1.76f, 0);
                break;
            default:
                mPlayer.transform.position = mStartPoint.position;
                break;
        }
        SetHP(Player.Instance.mCurrentHP);
        for (int i = 0; i < mMapMaterialController.mCutsceneArr.Length; i++)
        {
            CutSceneController.Instance.mCutSceneList.Add(mMapMaterialController.mCutsceneArr[i].mTrigger);
        }
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


    public void GotoStageSelect(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
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
        Player.Instance.mCurrentHP += count;
    }

    public void Damege()
    {
        if (mPlayerHP.Count==1)
        {
            int rand = Random.Range(7, 10);//hitSound
            SoundController.Instance.SESound(rand);
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
        if (TitleController.Instance.PlayCount <=0)
        {
            StartCoroutine(LobbyLoad());
        }
        else
        {
            UIController.Instance.mBlackScrean.gameObject.SetActive(true);
            RemoveHealth();
            Heal(Player.Instance.mMaxHP - Player.Instance.mCurrentHP);
            Player.Instance.isReset = true;
            StartCoroutine(DeathLoad());
        }
    }
    private IEnumerator DeathLoad()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mMapMaterialController.RefreshMap();
        Player.Instance.isNoDamage = true;
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.transform.position = Player.Instance.CheckPointPos;
        yield return delay;
        UIController.Instance.mBlackScrean.gameObject.SetActive(false);
        Player.Instance.isNoDamage = false;
        Player.Instance.isReset = false;
        StartCoroutine(UIController.Instance.ShowPlayCountScreen());
    }
    private IEnumerator LobbyLoad()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        UIController.Instance.mBlackScrean.gameObject.SetActive(true);
        TitleController.Instance.PlayCount = 3;
        yield return delay;
        SceneManager.LoadScene(2);
    }
}
