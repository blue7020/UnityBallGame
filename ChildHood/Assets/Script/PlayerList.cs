using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    public static PlayerList Instance;

    [SerializeField]
    public Player[] mPlayer;

    [SerializeField]
    public VirtualJoyStick stick;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        switch (GameSetting.Instance.PlayerID)
        {
            case 0:
                Player p0 = Instantiate(mPlayer[0], Vector3.zero, Quaternion.identity);
                p0.joyskick = stick;
                UIController.Instance.CharacterImage();
                break;
            case 1:
                Player p1 = Instantiate(mPlayer[1], Vector3.zero, Quaternion.identity);
                UIController.Instance.CharacterImage();
                p1.joyskick = stick;
                break;
            default:
                Debug.LogError("Wrong Player ID");
                break;
        }
    }
}
