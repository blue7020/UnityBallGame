using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    [SerializeField]
    private Enemy mEnemy;
    [SerializeField]
    public float mDamage;
    [SerializeField]
    private Transform mBulletPos;
    [SerializeField]
    private BulletPool mbulletPool;
    [SerializeField]
    private EnemyAttackArea mAttackArea;

    public void Skill()
    {
        switch (mEnemy.mID)
        {
            case 0://Mimic_Wood
                break;
            case 1://Slime
                break;
            case 2://Mold_King
                MoldKingAttack();
                break;
            case 3://Moldling
                MoldlingAttack();
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }


    private void MoldKingAttack()//id = 2
    {
        Debug.Log("발사");
        mEnemy.mAnim.SetTrigger(AnimHash.Enemy_Attack);
        for (int i=0; i<4; i++)
        {
            Bullet bolt = mbulletPool.GetFromPool(0);
            bolt.transform.position = mBulletPos.position;
            bolt.transform.LookAt(Player.Instance.transform.position);
            bolt.ResetDir();
        }


    }
    private void MoldlingAttack()//id = 3
    {
        mEnemy.mAnim.SetTrigger(AnimHash.Enemy_Attack);
    }
}
