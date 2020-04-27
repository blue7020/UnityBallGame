using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    [SerializeField]
    private float mSpeed;
    private Vector2 SetPos;

    // Start is called before the first frame update
    void Start()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        SetPos = new Vector2(0, 0);
        mRB2D.position = SetPos;

    }

    // Update is called once per frame
    void Update()
    {
        Movment();
        
    }

    private void Movment()
    {
        //이동 안하고 방향만 바꾸고 싶음
        float hori = Input.GetAxis("Horizontal");
        mAnim.SetBool(AnimHash.Walk, true);
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);//해당 스프라이트가 오른쪽을 바라보고 있기 때문에 180도
            SetPos += new Vector2(-mSpeed, 0);
            transform.position = SetPos;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.identity;//기본 Rotation 값을 받음. 따라서 0,0,0
            SetPos += new Vector2(mSpeed, 0);
            transform.position = SetPos;

        }
        mAnim.SetBool(AnimHash.Walk, false);
    }
}
