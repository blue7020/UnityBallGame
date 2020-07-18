using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    public int mStageNum;
    public GameObject mPortal;

    private void Awake()
    {
        if (GameSetting.Instance.StageOpen[mStageNum-1]==false)
        {
            mPortal.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameSetting.Instance.Ingame = true;
            SceneManager.LoadScene(2);//TODO 스테이지마다 다르게
        }
    }
}
