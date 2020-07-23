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
            GameSetting.Instance.NowStage = mStageNum;
            switch (mStageNum)
            {
                case 1:
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    Debug.LogError("Wrong Stage Number");
                    break;

            }
        }
    }
}
