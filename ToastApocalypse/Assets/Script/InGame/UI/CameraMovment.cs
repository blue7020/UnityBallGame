using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{

    public static CameraMovment Instance;
    public GameObject mPlayerObj;
    public Camera mCamera;
    private Vector3 mOffset;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
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

    // Start is called before the first frame update
    void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerSetting(GameObject player)
    {
        mPlayerObj = player;
        mOffset = transform.position - mPlayerObj.transform.position; //카메라의 위치 설정
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mPlayerObj!=null)
        {
            transform.position = mPlayerObj.transform.position + mOffset;
        }
    }

}
