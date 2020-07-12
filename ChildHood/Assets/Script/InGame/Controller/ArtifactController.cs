using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactController : InformationLoader
{
    public static ArtifactController Instance;

    [SerializeField]
    public ArtifactStat[] mInfoArr;
    [SerializeField]
    public ArtifactTextStat[] mStatInfoArr;

    public ArtifactStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        LoadJson(out mInfoArr, Path.ARTIFACT_STAT);
        LoadJson(out mStatInfoArr, Path.ARTIFACT_TEXT_STAT);
    }
}
