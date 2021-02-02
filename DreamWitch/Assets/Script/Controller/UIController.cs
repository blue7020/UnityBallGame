using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : InformationLoader
{
    public static UIController Instance;

    public Image mPlayCountSceen,mItemImage,mItemBoxImage,mDialogueImage,mDialogueFaceImage, mTextBoxImage, mScreenSaver, mMenuWindow,mCollectionImage, mSoundMenu, mScreenEffect,mGameOverImage;
    public Sprite mNull;
    public Sprite[] mFaceSprite;
    public Text mPlayCountText,mDialogue,mCheckPointText,mTutorialText,mSkipText,mNextDialogueText,mTextBoxText,mCloseText,mMapText, mMenuMainButtonText, mMenuSoundText, mCollectionText, mMenuCloseText;
    public Text mSoundText, mBGMText, mSEText, mBGMVolumeText, mSEVolumeText, mBackText;
    public bool isShowTextBox, isShowMenu,isCollect;
    public Transform Top, End, CollectionTop, CollectionEnd;

    public StageInfo[] mInfoArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mInfoArr, Path.STAGE_INFO);
            mBGMVolumeText.text = SoundController.Instance.BGMVolume.ToString();
            mSEVolumeText.text = SoundController.Instance.SEVolume.ToString();
            if (TitleController.Instance.mLanguage == 0)
            {
                mCheckPointText.text = "*체크포인트가 갱신되었습니다!";
                mTutorialText.text = "이동: WASD / 점프: 스페이스바\n마법: Q / 줍기: F / 사용: E\n메뉴: ESC";
                mSkipText.text = "건너뛰기: C";
                mCloseText.text = "닫기: Z";
                mNextDialogueText.text= "다음: Z";
                mMenuMainButtonText.text = "스테이지 선택으로";
                mMenuCloseText.text = "닫기: ESC";
                mMenuSoundText.text = "소리 설정";
                mSoundText.text = "소리 설정";
                mBackText.text = "뒤로";
                mBGMText.text = "배경 음악";
                mSEText.text = "효과음";
                mMapText.text = mInfoArr[TitleController.Instance.NowStage].title_kor;
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                mCheckPointText.text = "*Checkpoints updated!";
                mTutorialText.text = "Move: WASD / Jump: Space Bar\nMagic: Q / Pick: F / Use: E\nMenu: ESC";
                mSkipText.text = "Skip: C";
                mCloseText.text = "Close: Z";
                mNextDialogueText.text = "Next: Z";
                mMenuMainButtonText.text = "To the stage select";
                mMenuCloseText.text = "Close: ESC";
                mMenuSoundText.text = "Sound Setting";
                mSoundText.text = "Sound Setting";
                mBackText.text = "Back";
                mBGMText.text = "BGM";
                mSEText.text = "SE";
                mMapText.text = mInfoArr[TitleController.Instance.NowStage].title_eng;
            }
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(ShowPlayCountScreen());
    }

    public void CheckPointSet()
    {
        StartCoroutine(CheckPointAnim());
        SoundController.Instance.SESound(5);
    }
    public IEnumerator CheckPointAnim()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        mCheckPointText.color = new Color(0.005f, 1, 0, 1);
        mCheckPointText.gameObject.SetActive(true);
        float halfTime = 1.3f;
        Color color = new Color(0, 0, 0, 1 / halfTime * Time.fixedDeltaTime);
        while (true)
        {
            yield return delay;
            mCheckPointText.color -= color;
            if (mCheckPointText.color.a >= 1)
            {
                mCheckPointText.gameObject.SetActive(false);
                break;
            }
        }
    }

    public IEnumerator ShowPlayCountScreen()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(2f);
        GameController.Instance.GamePause();
        mPlayCountText.text = "x"+TitleController.Instance.PlayCount;
        mPlayCountSceen.gameObject.SetActive(true);
        yield return delay;
        mPlayCountSceen.gameObject.SetActive(false);
        GameController.Instance.GamePause();
        GameController.Instance.mMapMaterialController.StartCutScene();
    }

    public void ShowTutorial()
    {
        mTutorialText.gameObject.SetActive(true);
    }
    public void HideTutorial()
    {
        mTutorialText.gameObject.SetActive(false);
    }

    public void ItemImageChange(Sprite spt=null)
    {
        mItemImage.sprite = spt;
    }

    public void ShowDialogue(string text)
    {
        Player.Instance.isCutScene = true;
        isShowTextBox = true;
        mTextBoxText.text = "";
        StartCoroutine(TypingTextToTextBox(text));
        mTextBoxImage.gameObject.SetActive(true);
    }
    private IEnumerator TypingTextToTextBox(string text)
    {
        WaitForSecondsRealtime delay = null;
        foreach (char letter in text.ToCharArray())
        {
            mTextBoxText.text += letter;
            yield return delay;
        }
    }

    public void GotoStage()
    {
        Time.timeScale = 1;
        Loading.Instance.StartLoading(2);
        SoundController.Instance.BGMChange(1);
    }

    public IEnumerator MenuClose()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.35f);
        CameraMovement.Instance.mFollowing = false;
        if (!isCollect)
        {
            mCollectionImage.GetComponent<Rigidbody2D>().DOMove(CollectionTop.position, 0.3f);
        }
        mMenuWindow.GetComponent<Rigidbody2D>().DOMove(Top.position, 0.3f);
        yield return delay;
        CameraMovement.Instance.mFollowing = true;
        mScreenSaver.gameObject.SetActive(false);
        GameController.Instance.GamePause();
        isShowMenu = false;
    }
    public IEnumerator MenuShow()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.35f);
        CameraMovement.Instance.mFollowing = false;
        mSoundMenu.gameObject.SetActive(false);
        ShowCollection();
        mScreenSaver.gameObject.SetActive(true);
        if (!isCollect)
        {
            mCollectionImage.GetComponent<Rigidbody2D>().DOMove(CollectionEnd.position, 0.3f);
        }
        mMenuWindow.GetComponent<Rigidbody2D>().DOMove(End.position, 0.3f);
        yield return delay;
        CameraMovement.Instance.mFollowing = true;
        GameController.Instance.GamePause();
        isShowMenu = true;
    }

    public void ShowCollection()
    {
        mCollectionText.text = "x"+SaveDataController.Instance.mUser.CollectionAmount.ToString();
    }

    public IEnumerator CollectAnimation()
    {
        float time = 0.4f;
        WaitForSeconds delay = new WaitForSeconds(time);
        isCollect = true;
        mCollectionImage.GetComponent<Rigidbody2D>().DOMove(CollectionEnd.position, 0.3f);
        SaveDataController.Instance.mUser.CollectionAmount += 1;
        yield return delay;
        ShowCollection();
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        mCollectionImage.GetComponent<Rigidbody2D>().DOMove(CollectionTop.position, 0.5f);
        isCollect = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&& isShowTextBox&&!isShowMenu)
        {
            Player.Instance.isCutScene = false;
            mTextBoxImage.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isShowMenu)
            {
                StartCoroutine(MenuClose());
            }
            else
            {
                StartCoroutine(MenuShow());
            }
        }
    }

    public void BGMPlus()
    {
        SoundController.Instance.BGMPlus();
        mBGMVolumeText.text = SoundController.Instance.BGMVolume.ToString();
    }
    public void BGMMinus()
    {
        SoundController.Instance.BGMMinus();
        mBGMVolumeText.text = SoundController.Instance.BGMVolume.ToString();
    }
    public void SEPlus()
    {
        SoundController.Instance.SEPlus();
        mSEVolumeText.text = SoundController.Instance.SEVolume.ToString();
    }
    public void SEMinus()
    {
        SoundController.Instance.SEMinus();
        mSEVolumeText.text = SoundController.Instance.SEVolume.ToString();
    }
}
