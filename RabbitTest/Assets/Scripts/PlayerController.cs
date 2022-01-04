using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public PlayerCtrl[] mPlayerList;
    public Vector3 StartPos;
    public TilCtrl mReviveTile;
    public PlayerCtrl mPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mPlayer=Instantiate(mPlayerList[SaveDataController.Instance.mCharacterID], StartPos, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
