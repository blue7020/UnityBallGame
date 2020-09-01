using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour
{
    public static BlackSmith Instance;

    public Animator mAnim;

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
        if (GameSetting.Instance.NPCOpen[2]==true)
        {
            mAnim.SetBool(AnimHash.Furniture, true);
        }
    }
}
