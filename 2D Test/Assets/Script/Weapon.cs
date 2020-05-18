using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    private SpriteRenderer mRenderer;

    [SerializeField]
    private AttackArea mAttackArea;
    private bool mAttackCooltime = false;

    private void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //블렌드 트리 애니메이션을 사용해 비슷하게 만들 수 있음
        if (mPlayer.hori > 0) //좌
        {
                mRenderer.sortingOrder = 2;
                transform.rotation = Quaternion.Euler(0, 180, 45);
            
        }
        else if (mPlayer.hori < 0)//우
        {
                mRenderer.sortingOrder = 0;
                transform.rotation = Quaternion.Euler(0, 180, -135);
        }
        else if (mPlayer.ver > 0) //상
        {
                mRenderer.sortingOrder = 0;
                transform.rotation = Quaternion.Euler(0, 180, -45);
            
        }
        else if (mPlayer.ver < 0) //하
        {
                mRenderer.sortingOrder = 2;
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
