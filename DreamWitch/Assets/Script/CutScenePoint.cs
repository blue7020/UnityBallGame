using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePoint : MonoBehaviour
{
    public bool mTrigger;
    public int mID;

    public IEnumerator CutScene0()
    {
        float time = 2.5f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        Player.Instance.ShowAction(0);
        yield return delay;
        SoundController.Instance.mBGM.mute = true;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        SoundController.Instance.SESound(6);
        Player.Instance.ShowAction(1);
        time = 1f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        CutSceneController.Instance.CutSceneCamera();
        time = 5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.isCutScene = false;
        GameController.Instance.ShowUI();
        SoundController.Instance.mBGM.mute=false;
        CutSceneController.Instance.ChangeMainCamera();
    }

    public IEnumerator CutScene1()
    {
        float time = 2f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        CutSceneController.Instance.FadeOut();
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
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        Player.Instance.ShowAction(1);
        time = 2f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = true;
        SoundController.Instance.SESound(6);
        CutSceneController.Instance.ShowCutSceneImage();
        Player.Instance.mNowItem.gameObject.SetActive(false);
        Player.Instance.mNowItem.transform.SetParent(Player.Instance.mItemTransform);
        Player.Instance.mNowItem.mRenderer.sortingOrder = 2;
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SoundController.Instance.mBGM.mute = false;
        CutSceneController.Instance.CloseCutSceneImage();
        GameController.Instance.ShowUI();
        Player.Instance.isCutScene = false;
    }

    public void PlayCutScene()
    {
        switch (mID)
        {
            case 0:
                StartCoroutine(CutScene0());
                break;
            case 1:
                if (Player.Instance.mNowItemID==0)
                {
                    StartCoroutine(CutScene1());
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !mTrigger)
        {
            mTrigger = true;
            PlayCutScene();
        }
    }
}
