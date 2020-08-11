using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactController : InformationLoader
{
    public static ArtifactController Instance;

    public ArtifactStat[] mStatInfoArr;
    public ArtifactTextStat[] mTextInfoArr;

    public List<Artifacts> mPassiveArtifact;
    public List<Artifacts> mActiveArtifact;

    public ArtifactStat[] GetInfoArr()
    {
        return mStatInfoArr;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadJson(out mStatInfoArr, Path.ARTIFACT_STAT);
            LoadJson(out mTextInfoArr, Path.ARTIFACT_TEXT_STAT);
            mPassiveArtifact = new List<Artifacts>();
            mActiveArtifact = new List<Artifacts>();
            for (int i = 0; i < GameSetting.Instance.mArtifacts.Length; i++)
            {
                if (GameSetting.Instance.mArtifacts[i].eType == eArtifactType.Passive)
                {
                    mPassiveArtifact.Add(GameSetting.Instance.mArtifacts[i]);
                }
                else
                {
                    mActiveArtifact.Add(GameSetting.Instance.mArtifacts[i]);
                }
            }
        }
        else
        {
            Delete();
        }
 
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
