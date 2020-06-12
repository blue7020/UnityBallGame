using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyWorld : MonoBehaviour
{
    public static DontDestroyWorld Instance;
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
