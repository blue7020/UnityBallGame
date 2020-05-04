using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    [SerializeField]
<<<<<<< HEAD
    private float mSpeed;
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
=======
    private AttackArea mAttackArea;
    private Vector2 dir;

    [Header("Status")]
    [SerializeField]
    public float mAtk;
    [SerializeField]
    public float mSpeed;
    [SerializeField]
    public float mMaxHP;
    [SerializeField]
    public float mAttackSpeed;//초기값은 1.2
    private bool mAttackCooltime = false;

    public float mCurrentHP;


    private void Awake()
>>>>>>> ff9f0b203ae30ad28c44b97e3eff398effea75b1
    {
        mCurrentHP = mMaxHP;
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
<<<<<<< HEAD
        

=======
>>>>>>> ff9f0b203ae30ad28c44b97e3eff398effea75b1
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mAttackCooltime==false)
            {
                mAttackArea.Attack();
                StartCoroutine("Attack");
            }
            
        }

    }

    public void Hit(float damage)
    {
        mCurrentHP -= damage;
    }

    IEnumerator Attack()
    {
<<<<<<< HEAD
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
=======
        mAttackCooltime = true;
        yield return new WaitForSeconds(mAttackSpeed);
        mAttackCooltime = false;
    }

    //테스트용
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            mCurrentHP = mMaxHP;

        }
    }

    private void PlayerMovement()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        dir = new Vector2(hori, ver);
        dir = dir.normalized * mSpeed;
        if (hori > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.identity;
        }
        else if (hori < 0)   
        {
            mAnim.SetBool(AnimHash.Walk, true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (ver > 0)
        {
            mAnim.SetBool(AnimHash.Walk, true);
        }
        else if (ver < 0)
>>>>>>> ff9f0b203ae30ad28c44b97e3eff398effea75b1
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
