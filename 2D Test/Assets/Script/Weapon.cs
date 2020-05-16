using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    //private SpriteRenderer Layer;

    [SerializeField]
    private AttackArea mAttackArea;
    private bool mAttackCooltime = false;

    // Update is called once per frame
    void Update()
    {
        if (mPlayer.hori > 0) //좌
        {
            transform.rotation = Quaternion.Euler(0, 180, 45);
        }
        else if (mPlayer.hori < 0)//우
        {
            //Layer.sortingOrder = 0;
            transform.rotation = Quaternion.Euler(0, 180, -135);
        }
        else if (mPlayer.ver > 0) //상
        {
            transform.rotation = Quaternion.Euler(0, 180, -45);
        }
        else if (mPlayer.ver < 0) //하
        {
            transform.rotation = Quaternion.Euler(0, 180, 135);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mAttackCooltime == false)
            {
                StartCoroutine("Attack");
            }

        }
        
    }

    IEnumerator Attack()
    {

        mAttackCooltime = true;
        mAttackArea.Attack();
        yield return new WaitForSeconds(mPlayer.mAttackSpeed);
        mAttackCooltime = false;
    }
}
