using System.Collections;
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
        yield return delay;
        mMoveTrigger = true;
        StartCoroutine(MovePlayer());
        StartCoroutine(UIController.Instance.ShowDialogueTimer("후, 오늘 산책길도 얼마 안 남았네.", 3f));
        time = 1.3f;
        delay = new WaitForSeconds(time);
        yield return delay;
        mMoveTrigger = false;
        time = 2f;
        delay = new WaitForSeconds(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("저런 언덕쯤은 거뜬하지!", 2f));
        time = 3f;
        delay = new WaitForSeconds(time);
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
                Player.Instance.mRB2D.velocity = Vector2.zero;
                Player.Instance.mAnim.SetFloat("xVelocity", 0);
                break;
            }
            yield return delay;
        }
        Player.Instance.mRB2D.velocity = Vector2.zero;
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("등불을 밝힐때면 무슨 일이 일어나곤 해.", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        StartCoroutine(UIController.Instance.ShowDialogueTimer("저 위로 올라가려면 등불을 밝히는 게 좋을거야.", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        Player.Instance.mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        StartCoroutine(UIController.Instance.ShowDialogueTimer("Q 키로 마법을 사용할 수 있어.\n그거면 점화할 수 있을거야!", 2f));
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("망각의 꽃은 항상 편안한 기분이 들게끔 도와줘.\n고통, 슬픔, 외로움...같은 것에서 말이야.", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("...자꾸 혼잣말을 하게 되는데,\n내가 정신 나간 건 아니겠지?! 꽃이 필요해!", 2f));
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("저 검은 녀석들, 얼마 전부터 나타나기\n시작했는데...생명체일까?", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("나 외의 생물을 보는 건 처음인데...\n암튼 다가가면 물려고 했었지!", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        StartCoroutine(UIController.Instance.ShowDialogueTimer("밟아주거나 마법으로 손 좀 봐줘야겠어!", 2f));
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("우왓, 저런 애들은 처음보는데...!", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("암튼 쟤네가 쏘는 것에 맞으면\n좋을 것 같진 않네..", 2f));
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("다녀왔어요, 프레스톤 씨!\n오늘은 산책하는데 검은 녀석들이\n더 늘어난 것 같아서 걱정되요...", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("이러다가 쟤네들이 우리 집까지\n오면 어떡하죠...!", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("아, 섬 전경 보는 걸 잊었다구요?\n네, 빨리 보러가요!", 2f));
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
        StartCoroutine(UIController.Instance.ShowDialogueTimer("...프레스톤 씨, 이곳이 어둠으로\n둘러쌓인다고 해도", 2f));
        time = 3f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("마지막까지 저랑 항상 함께한다고 약속해주세요...너무 무서워요...", 2f,false));
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
        CutSceneController.Instance.ShowCutSceneImage(0);
        time = 5f;
        delay = new WaitForSecondsRealtime(time);
        yield return delay;
        CutSceneController.Instance.CloseCutSceneImage();
        SoundController.Instance.mBGM.mute = false;
        StartCoroutine(UIController.Instance.ShowDialogueTimer("...우린...앞으로 어떻게 될까요?", 3f));
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
