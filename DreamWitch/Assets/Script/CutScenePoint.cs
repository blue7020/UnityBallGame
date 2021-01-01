﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenePoint : MonoBehaviour
{
    public bool mTrigger,IsSkip;
    public int mID;
    private bool mMoveTrigger;

    public IEnumerator CutScene0()
    {
        mTrigger = true;
        float time = 0.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        Player.Instance.mSpeed -= 1.5f;
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(0, 2f));
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        time = 1.5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        mMoveTrigger = false;
        time = 2f;
        delay = new WaitForSeconds(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(1, 2f));
        time = 3f;
        delay = new WaitForSeconds(time);
        Player.Instance.mSpeed += 1.5f;
        yield return delay;
        UIController.Instance.ShowTutorial();
        EndCutScene();
    }
    public IEnumerator MovePlayer()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        while (mMoveTrigger)
        {
            Player.Instance.Moving(1);
            if (!mMoveTrigger)
            {
                break;
            }
            yield return delay;
        }
        Player.Instance.mRB2D.velocity = new Vector2(Player.Instance.mRB2D.velocity.x, Player.Instance.mRB2D.velocity.y);
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
    }


    public IEnumerator CutScene1()
    {
        float time = 1f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        StartCoroutine(UIController.Instance.ShowDialogueTimer(2, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        StartCoroutine(UIController.Instance.ShowDialogueTimer(3, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        StartCoroutine(UIController.Instance.ShowDialogueTimer(4, 2f));
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        EndCutScene();
    }

    public IEnumerator CutScene2()
    {
        float time = 3f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(5, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(6, 2f));
        time = 2.5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        EndCutScene();
    }

    public IEnumerator CutScene3()
    {
        float time = 3f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(7, 2f));
        time = 3.5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(8, 2f));
        time = 3.5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(9, 2f));
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        EndCutScene();
    }

    public IEnumerator CutScene4()
    {
        float time = 2f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        UIController.Instance.HideTutorial();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        Player.Instance.ShowAction(0);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Player.Instance.ShowAction(1);
        StartCoroutine(UIController.Instance.ShowDialogueTimer(10, 2f));
        time = 1.5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = true;
        SoundController.Instance.SESound(6);
        CutSceneController.Instance.CutSceneCamera();
        time = 4f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = false;
        CutSceneController.Instance.ChangeMainCamera();
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        StartCoroutine(UIController.Instance.ShowDialogueTimer(11, 3f));
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        EndCutScene();
    }

    public IEnumerator CutScene5()
    {
        float time = 3f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        Player.Instance.ShowAction(4);
        StartCoroutine(UIController.Instance.ShowDialogueTimer(12, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(13, 2f));
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        EndCutScene();
    }

    public IEnumerator CutScene6()
    {
        float time = 3f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(14, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(15, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(16, 2f));
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        EndCutScene();
    }

    public IEnumerator CutScene7()
    {
        float time = 1.5f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        CutSceneController.Instance.FadeOut();
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Player.Instance.mNowItem.gameObject.SetActive(true);
        Player.Instance.mNowItem.transform.SetParent(null);
        Player.Instance.mNowItem.transform.position = new Vector3(Player.Instance.transform.position.x+0.5f, 5.26f,0);
        Player.Instance.mNowItem.mRenderer.sortingOrder = 6;
        CutSceneController.Instance.FadeIn();
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.ShowAction(0);
        StartCoroutine(UIController.Instance.ShowDialogueTimer(17, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(18, 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        Player.Instance.ShowAction(1);
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        CutSceneController.Instance.ShowCutSceneImage(0);
        time = 1f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = true;
        SoundController.Instance.SESound(6);
        StartCoroutine(CutSceneController.Instance.FadeinCutSceneImage(1));
        time = 5.5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        CutSceneController.Instance.CloseCutSceneImage();
        SoundController.Instance.mBGM.mute = false;
        StartCoroutine(UIController.Instance.ShowDialogueTimer(19, 3f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        mID = 8;
        PlayCutScene();
    }

    public IEnumerator CutScene8()
    {
        float time = 1f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        SoundController.Instance.BGMFadeOut(3f);
        TitleController.Instance.isShowTitle = true;
        yield return delay;
        CutSceneController.Instance.FadeOut();
        TitleController.Instance.TutorialClear = true;
        time = 4f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SceneManager.LoadScene(0);
    }

    public void PlayCutScene()
    {
        UIController.Instance.TextBoxCheck();
        UIController.Instance.mCutScenePoint = this;
        if (IsSkip)
        {
            UIController.Instance.mCutSceneSkipButton.gameObject.SetActive(true);
        }
        switch (mID)
        {
            case 0:
                mTrigger = true;
                StartCoroutine(CutScene0());
                break;
            case 1:
                mTrigger = true;
                StartCoroutine(CutScene1());
                break;
            case 2:
                mTrigger = true;
                StartCoroutine(CutScene2());
                break;
            case 3:
                mTrigger = true;
                StartCoroutine(CutScene3());
                break;
            case 4:
                mTrigger = true;
                StartCoroutine(CutScene4());
                break;
            case 5:
                mTrigger = true;
                StartCoroutine(CutScene5());
                break;
            case 6:
                mTrigger = true;
                StartCoroutine(CutScene6());
                break;
            case 7:
                if (Player.Instance.mNowItemID==0)
                {
                    mTrigger = true;
                    StartCoroutine(CutScene7());
                }
                break;
            case 8:
                mTrigger = true;
                StartCoroutine(CutScene8());
                break;
            default:
                break;
        }
    }

    public void StopCutScene(int id)
    {
        switch (id)
        {
            case 0:
                StopCoroutine(CutScene0());
                break;
            case 1:
                StopCoroutine(CutScene1());
                break;
            case 2:
                StopCoroutine(CutScene2());
                break;
            case 3:
                StopCoroutine(CutScene3());
                break;
            case 4:
                StopCoroutine(CutScene4());
                break;
            case 5:
                StopCoroutine(CutScene5());
                break;
            case 6:
                StopCoroutine(CutScene6());
                break;
            case 7:
                StopCoroutine(CutScene7());
                break;
            case 8:
                StopCoroutine(CutScene8());
                break;
            default:
                break;
        }
        UIController.Instance.HideDialogue();
        UIController.Instance.mCutScenePoint = null;
        EndCutScene();
    }

    public void EndCutScene()
    {
        Player.Instance.isCutScene = false;
        UIController.Instance.mCutSceneSkipButton.gameObject.SetActive(false);
        UIController.Instance.HideDialogue();
        GameController.Instance.ShowUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !mTrigger)
        {
            PlayCutScene();
        }
    }
}
