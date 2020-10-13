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
            for (int i = 5; i < mNPCArr.Length; i++)
            {
                if (SaveDataController.Instance.mUser.NPCOpen[i] == false)
                {
                    mNotOpenNPCList.Add(mNPCArr[i]);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NPCSpawn()
    {
        if (GameController.Instance.StageLevel==2)
        {
            float Spawnrate = Random.Range(0, 1f);
            if (NPCSpawnrate>=Spawnrate)
            {
                if (mNotOpenNPCList.Count > 0)
                {
                    int rand = Random.Range(0, mNotOpenNPCList.Count);
                    Instantiate(mNotOpenNPCList[rand], Player.Instance.CurrentRoom.mNPCSpawnPos);
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
