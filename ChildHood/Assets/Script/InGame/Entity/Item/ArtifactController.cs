using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactController : InformationLoader
{
    [SerializeField]
    private ArtifactData[] mInfoArr;
    [SerializeField]
    private Sprite[] mSpriteArr;

    private void Awake()
    {
        LoadJson(out mInfoArr, Path.ARTIFACT_STAT);
        //LoadJson(out mInfoArr, Path.ARTIFACT_TEXT_STAT);
        mSpriteArr = Resources.LoadAll<Sprite>("Artifact");

    }

    public ArtifactData GetItem(int id)
    {
        return mInfoArr[id].GetClone();
    }

    public Sprite GetItemSprite(int id)
    {
        return mSpriteArr[id];
    }
}

[System.Serializable]
public class ArtifactData
{
    public int ID;
    public eArtifactType Type;
    public string Name;
    public string Contents;

    public ArtifactData GetClone()
    {
        return MemberwiseClone() as ArtifactData;
    }

}

public enum eArtifactType
{
    Use,
    None
}