using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public List<string> mTextList;
    public int NowIndex,EndIndex;

    public bool isChatDelay,isDialogue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mTextList = new List<string>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ChatDelay()
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.2f);
        isChatDelay = true;
        NowIndex++;
        yield return delay;
        DialogueCheck();
        isChatDelay = false;
    }

    public void DialogueCheck()
    {
        if (NowIndex== EndIndex)
        {
            UIController.Instance.mDialogueImage.gameObject.SetActive(false);
            NowIndex = -1;
            EndIndex = -1;
            isDialogue = false;
        }
    }

    public void DialogueSetting(int now,int end)
    {
        NowIndex = now;
        EndIndex = end;
        isDialogue = true;
    }

    private void FixedUpdate()
    {
        if (!isChatDelay&& isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ChatDelay());
            }
        }
    }
}
