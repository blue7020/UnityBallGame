using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNPCController : MonoBehaviour
{
    public GameObject[] NPCArr;

    private void Awake()
    {
        for (int i=0; i<GameSetting.Instance.NPCOpen.Length;i++)
        {
            if (GameSetting.Instance.NPCOpen[i] == false)
            {
                NPCArr[i].SetActive(false);
            }
        }
    }
}
