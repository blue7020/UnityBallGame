using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScreen : MonoBehaviour
{
    public static DontDestroyScreen Instance;
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
        DontDestroyOnLoad(gameObject);
    }
}
