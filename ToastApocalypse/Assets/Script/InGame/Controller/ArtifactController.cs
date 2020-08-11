using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactController : InformationLoader
{
    public static ArtifactController Instance;

    public ArtifactStat[] mStatInfoArr;
    public ArtifactTextStat[] mTextInfoArr;

    public List<Artifacts> mActiveArtifact;
    public List<Artifacts> mPassiveArtifact;

    public ArtifactStat[] GetInfoArr()
    {
        return mStatInfoArr;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Delete();
        }
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        LoadJson(out mStatInfoArr, Path.ARTIFACT_STAT);
        LoadJson(out mTextInfoArr, Path.ARTIFACT_TEXT_STAT);
        mPassiveArtifact = new List<Artifacts>();
        mActiveArtifact = new List<Artifacts>();
        for (int i=0; i<GameSetting.Instance.mArtifacts.Length;i++)
        {
            if (GameSetting.Instance.mArtifacts[i].mType==eArtifactType.Passive)
            {
                mPassiveArtifact.Add(GameSetting.Instance.mArtifacts[i]);
            }
            else
            {
                mActiveArtifact.Add(GameSetting.Instance.mArtifacts[i]);
            }
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
