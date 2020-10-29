using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactController : InformationLoader
{
    public static ArtifactController Instance;

    public ArtifactStat[] mStatInfoArr;
    public ArtifactTextStat[] mTextInfoArr;
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
