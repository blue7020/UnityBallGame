using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaterialController : MonoBehaviour
{

    public static MapMaterialController Instance;

    public CutScenePoint[] mCutsceneArr;
    public Enemy[] mEnemyArr;
    public CheckPoint[] mCheckPointArr;
    public bool isShowStartCutScene;

    public CutScenePoint mStartCutScene;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (isShowStartCutScene)
        {
            switch (TitleController.Instance.NowStage)
            {
                case 0:
                    StartCoroutine(mStartCutScene.CutScene0());
                    break;
                default:
                    break;
            }
        }
    }

    public void ReviveEnemy()
    {
        for (int i=0; i<mEnemyArr.Length;i++)
        {
            mEnemyArr[i].Revive();
        }
    }

    public void ResetCheckPoint()
    {
        for (int i = 0; i < mCheckPointArr.Length; i++)
        {
            mCheckPointArr[i].ResetCheckPoint();
        }
    }
}
