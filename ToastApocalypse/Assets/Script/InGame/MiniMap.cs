using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance;

    public bool MinimapOn;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            MinimapOn = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (MinimapOn==true)
        {
            Vector2 Pos = Player.Instance.transform.position;
        }
    }
}
