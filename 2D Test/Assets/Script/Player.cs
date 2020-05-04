using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    [SerializeField]
    private float mSpeed;
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerMovement();

    }

    private void PlayerMovement()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        dir = new Vector2(hori, ver);
        dir = dir.normalized * mSpeed;
        if (hori>0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.identity;
        }
        else if (hori < 0)   
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (ver>0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else if (ver<0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }

        mRB2D.velocity = dir;

    }
}
