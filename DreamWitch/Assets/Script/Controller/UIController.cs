﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Image mPlayCountSceen,mItemImage,mItemBoxImage,mDialogueImage,mDialogueFaceImage,mBlackScrean, mTextBoxImage;
    public Sprite mNull;
    public Sprite[] mFaceSprite;
    public Text mPlayCountText,mDialogue,mCheckPointText,mTutorialText,mSkipText,mNextDialogueText,mTextBoxText,mCloseText;
    public bool isShowTextBox;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            if (TitleController.Instance.mLanguage == 0)
            {
                mCheckPointText.text = "*체크포인트가 갱신되었습니다!";
                mTutorialText.text = "이동: WASD / 점프: 스페이스바\n마법: Q / 줍기: F / 사용: E\n메뉴: ESC";
                mSkipText.text = "건너뛰기: C";
                mCloseText.text = "닫기: Z";
                mNextDialogueText.text= "다음: Z";
            }
            else if (TitleController.Instance.mLanguage == 1)
            {
                mCheckPointText.text = "*Checkpoints updated!";
                mTutorialText.text = "Move: WASD / Jump: Space Bar\nMagic: Q / Pick: F / Use: E\nMenu: ESC";
                mSkipText.text = "Skip: C";
                mCloseText.text = "Close: Z";
                mNextDialogueText.text = "Next: Z";
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
        MapMaterialController.Instance.StartCutScene();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&& isShowTextBox)
        {
            Player.Instance.isCutScene = false;
            mTextBoxImage.gameObject.SetActive(false);
        }
    }
}
