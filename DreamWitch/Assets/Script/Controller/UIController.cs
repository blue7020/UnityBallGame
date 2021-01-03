using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : InformationLoader
{
    public static UIController Instance;

    public Image mPlayCountSceen,mItemImage,mItemBoxImage,mDialogueImage,mDialogueFaceImage;
    public Sprite mNull;
    public Sprite[] mFaceSprite;
    public Text mPlayCountText,mDialogue,mCheckPointText,mTutorialText;
    public CutScenePoint mCutScenePoint;
    public Button mCutSceneSkipButton;

    public DialogueText[] mDialogueTextArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            LoadJson(out mDialogueTextArr, Path.DIALOGUE_TEXT);
            if (TitleController.Instance.mLanguage == 0)
            {
                mCheckPointText.text = "*체크포인트가 갱신되었습니다!";
                mTutorialText.text = "이동: WASD / 점프: 스페이스바\n마법: Q / 줍기: F / 사용: E\n메뉴: ESC";
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                mCheckPointText.text = "*Checkpoints updated!";
                mTutorialText.text = "Move: WASD / Jump: Space Bar\nMagic: Q / Pick: F / Use: E\nMenu: ESC";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowPlayCountScreen());
    }

    public void TextBoxCheck()
    {
        if (Player.Instance.mTextBoxChecker)
        {
            mDialogueImage.transform.localPosition = new Vector3(0, 330f, 0);
        }
        else
        {
            mDialogueImage.transform.localPosition = new Vector3(0,-330f,0);
        }
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

    public void SkipCutScene()
    {
        mCutScenePoint.StopAllCoroutines();
        mCutScenePoint.EndCutScene();
        mCutScenePoint = null;
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

    public void ShowDialogue(int id)
    {
        TextBoxCheck();
        if (TitleController.Instance.mLanguage == 0)
        {
            mDialogue.text = mDialogueTextArr[id].text_kor;
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            mDialogue.text = mDialogueTextArr[id].text_eng;
        }
        mDialogueImage.gameObject.SetActive(true);
    }

    public IEnumerator ShowDialogueTimer(int id, float time, int FaceID = 0)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        mDialogue.text = "";
        mDialogueFaceImage.sprite=mFaceSprite[FaceID];
        string text="";
        if (TitleController.Instance.mLanguage == 0)
        {
            text = mDialogueTextArr[id].text_kor;
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            text = mDialogueTextArr[id].text_eng;
        }
        StartCoroutine(TypingText(text, time));
        mDialogueImage.gameObject.SetActive(true);
        yield return delay;
        mDialogueImage.gameObject.SetActive(false);
    }
    private IEnumerator TypingText(string text, float time)
    {
        foreach (char letter in text.ToCharArray())
        {
            mDialogue.text += letter;
            //int rand = Random.Range(0, 4);
            //switch (rand)
            //{
            //    case 0:
            //        SoundController.Instance.SESound(18);
            //        break;
            //    case 1:
            //        SoundController.Instance.SESound(20);
            //        break;
            //    case 2:
            //        SoundController.Instance.SESound(19);
            //        break;
            //    case 3:
            //        SoundController.Instance.SESound(21);
            //        break;
            //}
            yield return null;
        }
    }

    public void HideDialogue()
    {
        StopCoroutine(ShowDialogueTimer(0,0f));
        mDialogueImage.gameObject.SetActive(false);
    }
}
