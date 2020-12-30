using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mPlayCountSceen,mItemImage,mItemBoxImage,mDialogueImage;
    public Sprite mNull;
    public Text mPlayCountText,mDialogue,mCheckPointText,mTutorialText;
    public CutScenePoint mCutScenePoint;
    public Button mCutSceneSkipButton;

    public string[] mSentences;
    private int mIndex;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (TitleController.Instance.mLanguage == 0)
            {
                mCheckPointText.text = "*체크포인트가 갱신되었습니다!";
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                mCheckPointText.text = "*Checkpoints updated!";
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

    public void ShowDialogue(string text)
    {
        TextBoxCheck();
        mDialogue.text = text;
        mDialogueImage.gameObject.SetActive(true);
    }

    public IEnumerator ShowDialogueTimer(string text,float time)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        mDialogue.text = "";
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
            yield return null;
        }
    }

    public void HideDialogue()
    {
        StopCoroutine(ShowDialogueTimer("",0f));
        mDialogueImage.gameObject.SetActive(false);
    }
}
