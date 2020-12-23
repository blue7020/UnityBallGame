using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ePortalType
{
    Stage,
    level
}

public class Portal : MonoBehaviour
{

    public static Portal Instance;
    public ePortalType Type;

    public Animator mAnim;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mAnim = GetComponent<Animator>();
            UIController.Instance.mPortalButton.onClick.RemoveAllListeners();
            UIController.Instance.mPortalButton.onClick.AddListener(() => { Nextroom(); });
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowPortal()
    {
        gameObject.SetActive(true);
    }

    public void PortalAnim()
    {
        mAnim.SetBool(AnimHash.PortalSpawn,true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIController.Instance.mPortalButton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIController.Instance.mPortalButton.gameObject.SetActive(false);
        }
    }

    public void Nextroom()
    {
        SoundController.Instance.SESound(21);
        GameController.Instance.StageLevel++;
        if(GameSetting.Instance.ChallengeMode){
            GameController.Instance.SyrupInStage += 35;
        }
        Player.Instance.ResetBuff();
        Player.Instance.Nodamage = false;
        UIController.Instance.mPortalButton.gameObject.SetActive(false);
        for (int i=0; i<BuffEffectController.Instance.EffectList.Count;i++)
        {
            BuffEffectController.Instance.EffectList[i].gameObject.SetActive(false);
        }
        if (GameController.Instance.StageLevel == GameSetting.STAGELEVEL_COUNT)
        {
            UIController.Instance.StartCoroutine(UIController.Instance.SceneMoveShadow());
            SceneManager.LoadScene(3);
            UIController.Instance.StartCoroutine(UIController.Instance.ShowLevel());
            Player.Instance.transform.position = new Vector2(0, -10.5f);
        }
        else if (GameController.Instance.StageLevel> GameSetting.STAGELEVEL_COUNT)
        {
            GameController.Instance.SetRewardMaterial(GameSetting.Instance.NowStage);
            if (GameSetting.Instance.ChallengeMode)
            {
                if (GameController.Instance.StageLevel <= 6)
                {
                    if (GameSetting.Instance.NowStage==6)
                    {
                        UIController.Instance.ShowClearTextInMode();
                    }
                    else
                    {
                        BuffSelectController.Instance.SetBuff();
                        BuffSelectController.Instance.mWindow.gameObject.SetActive(true);
                    }
                }
                else
                {
                    GameController.Instance.StageLevel -= 1;
                    UIController.Instance.ShowClearTextInMode();
                }
            }
            else
            {
                UIController.Instance.ShowClearText();
            }
        }
        else if (GameController.Instance.StageLevel < GameSetting.STAGELEVEL_COUNT)
        {
            UIController.Instance.StartCoroutine(UIController.Instance.SceneMoveShadow());
            SceneManager.LoadScene(2);
            Player.Instance.transform.position = new Vector2(0, 0);
            UIController.Instance.StartCoroutine(UIController.Instance.ShowLevel());
            GameController.Instance.SetActiveArtifacts();
            GameController.Instance.SetWeapon();
        }
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();
    }
}
