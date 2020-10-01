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
        Player.Instance.ResetBuff();
        Player.Instance.Nodamage = false;
        UIController.Instance.mPortalButton.gameObject.SetActive(false);
        for (int i=0; i<BuffEffectController.Instance.EffectList.Count;i++)
        {
            BuffEffectController.Instance.EffectList[i].gameObject.SetActive(false);
        }
        if (GameController.Instance.StageLevel == GameSetting.STAGELEVEL_COUNT)
        {
            SceneManager.LoadScene(3);
            Player.Instance.transform.position = new Vector2(0, -10.5f);
        }
        else if (GameController.Instance.StageLevel> GameSetting.STAGELEVEL_COUNT)
        {
            UIController.Instance.ShowClearText();
        }
        else if (GameController.Instance.StageLevel < GameSetting.STAGELEVEL_COUNT)
        {
            SceneManager.LoadScene(2);
            Player.Instance.transform.position = new Vector2(0, 0);
            UIController.Instance.StartCoroutine(UIController.Instance.ShowLevel());
            WeaponController.Instance.mWeapons = new List<Weapon>();
            for (int i=0;i<GameSetting.Instance.mWeapons.Length; i++)
            {
                WeaponController.Instance.mWeapons.Add(GameSetting.Instance.mWeapons[i]);
            }
            MapNPCController.Instance.NPCSpawn();
        }
        UIController.Instance.ShowHP();
        UIController.Instance.ShowGold();


    }
}
