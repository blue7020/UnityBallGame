using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyCamera : MonoBehaviour
{
    public static MainLobbyCamera Instance;
    public GameObject mCamera;
    public MainLobbyPlayer mPlayerObj;
    private Vector3 mOffset;
    public bool PlayerSpawn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerSpawn = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CameraSetting(MainLobbyPlayer mPlayer)
    {
        PlayerSpawn = true;
        mPlayerObj =mPlayer;// .find 사용 금지 / FindGameObjectsWithTag는 어레이를 찾으니까 헷갈리면 안된다.
        mOffset = transform.position - mPlayerObj.transform.position; //카메라의 위치 설정
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerSpawn==true)
        {
            transform.position = mPlayerObj.transform.position + mOffset;
        }
    }

}
