using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject mChatacterObj;
    private Character mControlTarget;

    private void Start()
    {
        mControlTarget = mChatacterObj.GetComponent<Character>();
        mControlTarget.Attack();
        mControlTarget.Hit(100);
    }

    private void Update()
    {
        
    }
}
