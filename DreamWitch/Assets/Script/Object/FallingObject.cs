using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingObject : MonoBehaviour
{

    public Rigidbody2D mRB2D;
    public Vector3 mDir;

    public void Falling()
    {
        mRB2D.DOMove(mDir,0.75f);
    }
}
