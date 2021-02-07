using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueSystem : InformationLoader
{
    public static DialogueSystem Instance;


    public DialogueText[] mDialogueTextArr;
    public GameObject mNextPoint; 
    public List<string> mTextList;
    public int NowIndex,EndIndex;
    public bool isChatDelay,isDialogue,EndDialogue;

    public CutScenePoint mCutScenePoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            NowIndex = -1;
            EndIndex = -1;
            LoadJson(out mDialogueTextArr, Paths.DIALOGUE_TEXT);
            mTextList = new List<string>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ChatDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        if (EndDialogue)
        {
            DialogueCheck();
        }
        else
        {
            mNextPoint.SetActive(false);
            isChatDelay = true;
            ShowDialogue(NowIndex);
            mCutScenePoint.SetAction(NowIndex);
            if (NowIndex < EndIndex)
            {
                NowIndex++;
            }
            else
            {
                EndDialogue = true;
            }
        }
        yield return delay;
    }
    public IEnumerator ChatDelay2(float time =1f)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
        mNextPoint.SetActive(true);
        isChatDelay = false;
    }


    public void DialogueCheck()
    {
        if (EndDialogue)
        {
            NowIndex = -1;
            mCutScenePoint.SetAction(NowIndex);
        }
    }

    public void EndCutScene()
    {
        NowIndex = -1;
        EndIndex = -1;
        EndDialogue = false;
        isDialogue = false;
        mCutScenePoint.EndCutScene();
    }

    public void DialogueSetting(int now,int end)
    {
        NowIndex = now;
        EndIndex = end;
        isDialogue = true;
        StartCoroutine(ChatDelay());
    }

    public void ShowDialogue(int id)
    {
        UIController.Instance.mDialogueFaceImage.sprite = UIController.Instance.mFaceSprite[mDialogueTextArr[id].FaceCode];
        UIController.Instance.mDialogue.text = "";
        string text = "";
        if (TitleController.Instance.mLanguage == 0)
        {
            text = mDialogueTextArr[id].text_kor;
        }
        else if (TitleController.Instance.mLanguage == 1)
        {
            text = mDialogueTextArr[id].text_eng;
        }
        UIController.Instance.mDialogue.DOText(text,0.8f);
        //StartCoroutine(TypingTextToTextBox(text));
        UIController.Instance.mDialogueImage.gameObject.SetActive(true);
    }
    private IEnumerator TypingTextToTextBox(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            UIController.Instance.mDialogue.text += letter;
            yield return null;
        }
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (!isChatDelay)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine(ChatDelay());
                }
            }
            if (mCutScenePoint != null && mCutScenePoint.IsSkip)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    EndCutScene();
                }
            }
        }
    }
}
