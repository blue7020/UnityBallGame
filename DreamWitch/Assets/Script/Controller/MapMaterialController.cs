using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaterialController : MonoBehaviour
{
    public CutScenePoint[] mCutsceneArr;
    public Enemy[] mEnemyArr;
    public CheckPoint[] mCheckPointArr;
    public bool isShowStartCutScene;

    public bool[] ChapterArr;
    public Transform[] mStartPointArr;

    public CutScenePoint mStartCutScene;

    public List<CollectionObject> CollectionList;

    public GameObject mBoss;

    private void Awake()
    {
        CollectionList = new List<CollectionObject>();
        GameController.Instance.ChapterChange(TitleController.Instance.NowChapterCode);
    }

    public void StartCutScene()
    {
        if (isShowStartCutScene)
        {
            switch (TitleController.Instance.NowStage)
            {
                case 0:
                    isShowStartCutScene = false;
                    mStartCutScene.PlayCutScene();
                    break;
                case 1:
                    isShowStartCutScene = false;
                    if (ChapterArr[0]==true)
                    {
                        mStartCutScene.PlayCutScene();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void RefreshMap()
    {
        RefreshCollection();
        ReviveEnemy();
        ResetCheckPoint();
    }

    public void RefreshCollection()
    {
        if (CollectionList.Count>0)
        {
            for (int i = 0; i < CollectionList.Count; i++)
            {
                CollectionList[i].Refresh();
            }
        }
    }

    public void ReviveEnemy()
    {
        if (mEnemyArr.Length > 0)
        {
            for (int i = 0; i < mEnemyArr.Length; i++)
            {
                mEnemyArr[i].Revive();
            }
        }
    }

    public void ResetCheckPoint()
    {
        if (mCheckPointArr.Length > 0)
        {
            for (int i = 0; i < mCheckPointArr.Length; i++)
            {
                mCheckPointArr[i].ResetCheckPoint();
            }
        }
    }
}
