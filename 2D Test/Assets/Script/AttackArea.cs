using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private bool mAttackEnd = true;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (mAttackEnd == false)
        {
            //애니메이션끝난 후 mAttackEnd는 False
            gameObject.SetActive(mAttackEnd);
            mAttackEnd = true;
        }
    }

    public void Attack()
    {
        gameObject.SetActive(mAttackEnd);
        mAnim.SetBool(AnimHash.Attack, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mob"))
        {
            Debug.Log("Attack!");
            
        }
    }
}
