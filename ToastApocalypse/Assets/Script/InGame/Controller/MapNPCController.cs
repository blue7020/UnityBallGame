using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNPCController : MonoBehaviour
{
    public static MapNPCController Instance;

    public MapNPC[] mNPCArr;
    public List<MapNPC> mNotOpenNPCList;
    public float NPCSpawnrate;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mNotOpenNPCList = new List<MapNPC>();
            if (mNPCArr.Length>5)
            {
                for (int i = 4; i < mNPCArr.Length; i++)
                {
                    if (GameSetting.Instance.NPCOpen[i] == false)
                    {
                        mNotOpenNPCList.Add(mNPCArr[i]);
                    }
                }
            }
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NPCSpawn()
    {
        if (GameController.Instance.StageLevel==3)
        {
            float Spawnrate = Random.Range(0, 1f);
            if (NPCSpawnrate>Spawnrate)
            {
                if (mNotOpenNPCList.Count > 0)
                {
                    int rand = Random.Range(4, mNotOpenNPCList.Count);
                    Instantiate(mNPCArr[rand], Player.Instance.CurrentRoom.mNPCSpawnPos);
                }
            }
        }
    }

    public void MainNPCSpawn()
    {
        if (GameController.Instance.StageLevel == 5)
        {
            switch (GameSetting.Instance.NowStage)
            {
                case 1:
                    break;
                case 2:
                    Instantiate(mNPCArr[2], Player.Instance.CurrentRoom.mNPCSpawnPos);
                    Debug.Log("소환");
                    break;
                case 3:
                    Instantiate(mNPCArr[3], Player.Instance.CurrentRoom.mNPCSpawnPos);
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    break;
            }
        }
    }
}
