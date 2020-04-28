using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    [SerializeField]
    private float mSpeed;
    Vector2 Pos;

    // Start is called before the first frame update
    void Start()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        Pos= transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            mAnim.SetBool(AnimHash.Walk, true);
            Pos.x += mSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Pos.y += mSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        }
        else if (Input.GetKey(KeyCode.D))   
        {
            transform.rotation = Quaternion.identity;
            mAnim.SetBool(AnimHash.Walk, true);
            Pos.x += mSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Pos.y += mSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        }
        else if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S))
        {
            mAnim.SetBool(AnimHash.Walk, true);
            Pos.x += mSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Pos.y += mSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        }
        else
        {
            mAnim.SetBool(AnimHash.Walk, false);
        }
        transform.position = Pos;

    }
}
