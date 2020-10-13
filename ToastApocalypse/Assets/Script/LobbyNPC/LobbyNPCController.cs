using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNPCController : MonoBehaviour
{
    public GameObject[] NPCArr;

    private void Awake()
    {
        for (int i=0; i< SaveDataController.Instance.mUser.NPCOpen.Length;i++)
        {
            if (SaveDataController.Instance.mUser.NPCOpen[i] == false)
            {
                NPCArr[i].SetActive(false);
            }
        }
    }
}
