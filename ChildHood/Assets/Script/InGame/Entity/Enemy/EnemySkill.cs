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
        mAttackArea.mAnim.SetTrigger(AnimHash.Enemy_Attack);
        BulletPool.Instance.GetFromPool(0);
    }
    private void MoldlingAttack()//id = 3
    {
        mAttackArea.mAnim.SetTrigger(AnimHash.Enemy_Attack);
    }
}
