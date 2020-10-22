using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{

    public static HitAnimation Instance;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            mHitAnim.SetBool(AnimHash.HitAnim, false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Animator mHitAnim;
}
