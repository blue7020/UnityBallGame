using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    public int mStageNum;
    public GameObject mPortal;

    public Room[] mStage;

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
            for (int i= 0; i<mStage.Length;i++)
            {
                GameSetting.Instance.NowStageRoom[i] = mStage[i];
            }
            GameSetting.Instance.Ingame = true;
            GameSetting.Instance.NowStage = mStageNum;
            SceneManager.LoadScene(3);
        }
    }
}
