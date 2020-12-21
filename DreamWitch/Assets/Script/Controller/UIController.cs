using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mPlayCountSceen,mItemImage,mItemBoxImage,mDialogueImage;
    public Sprite mNull;
    public Text mPlayCountText,mDialogue,mCheckPointText;
    public GameObject mCheckPointTextObj;

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
        mCheckPointTextObj.gameObject.SetActive(true);
        SoundController.Instance.SESound(5);
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

    public IEnumerator ShowDialogueTimer(string text,float time,bool fadein=true)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        if (fadein)
        {
            StartCoroutine(ShowDialogueFadeIn());
        }
        mDialogue.text = text;
        mDialogueImage.gameObject.SetActive(true);
        yield return delay;
        mDialogueImage.gameObject.SetActive(false);
    }
    public IEnumerator ShowDialogueFadeIn()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        Color color = new Color(0, 0, 0, 1 / 1f * Time.fixedDeltaTime);
        mDialogueImage.color = new Color(1,1,1,0);
        while (true)
        {
            yield return delay;
            mDialogueImage.color += color;
            if (mDialogueImage.color.a >= 1)
            {
                break;
            }
        }
    }
}
