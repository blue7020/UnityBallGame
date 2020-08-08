using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public static BuffController Instance;

    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RemoveNurf()
    {
        //for (int i = 0; i < Player.Instance.PlayerBuffList.Count; i++)
        //{
        //    if (Player.Instance.PlayerBuffList[i].eType ==eBuffType.Nurf)
        //    {
        //        Player.Instance.PlayerBuffList.RemoveAt(i);
        //    }
        //}
    }
}
