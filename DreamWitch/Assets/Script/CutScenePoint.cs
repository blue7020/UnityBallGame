using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenePoint : MonoBehaviour
{
    public bool mTrigger;
    public int mID;

    public IEnumerator CutScene4()
    {
        float time = 2.5f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(time);
        GameController.Instance.ShowUI();
        Player.Instance.mRB2D.velocity = Vector2.zero;
        Player.Instance.mAnim.SetFloat("xVelocity", 0);
        Player.Instance.isCutScene = true;
        Player.Instance.ShowAction(0);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        Player.Instance.ShowAction(1);
        StartCoroutine(UIController.Instance.ShowDialogueTimer("저게...뭐야...!", 2f));
        time = 2f;
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("불길해...빨리 집으로 가야겠어.", 3f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.isCutScene = false;
        GameController.Instance.ShowUI();
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("...프레스톤 씨 이곳이 어둠에 둘러쌓인다고 해도", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("마지막까지 저랑 항상 함께한다고 약속해주세요...\n너무 무서워요...", 2f,false));
        time = 3f;
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
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        CutSceneController.Instance.CloseCutSceneImage();
        SoundController.Instance.mBGM.mute = false;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("...우린...앞으로 어떻게 될까요?", 4f));
        time = 5f;
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
        time = 4f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        SceneManager.LoadScene(0);
    }

    public void PlayCutScene()
    {
        switch (mID)
        {
            case 4:
                StartCoroutine(CutScene4());
                break;
            case 7:
                if (Player.Instance.mNowItemID==0)
                {
                    StartCoroutine(CutScene7());
                }
                break;
            case 8:
                StartCoroutine(CutScene8());
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
