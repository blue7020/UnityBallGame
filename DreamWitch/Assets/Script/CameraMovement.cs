using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    public GameObject mPlayerObj;
    public Camera mCamera;

    private void Awake()
    {
        if (Instance == null)
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

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mPlayerObj != null)
        {
            transform.position = new Vector3(mPlayerObj.transform.position.x, mPlayerObj.transform.position.y, -10);
        }
    }

}
