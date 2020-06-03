using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    [SerializeField]
    private Enemy mEnemy;
    [SerializeField]
    private float mDamage;
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
            case 2://Ghost
                GhostAttack();
                break;
            case 3://Mimic_Gold
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }


    private void GhostAttack()//id = 2
    {
        mAttackArea.mAnim.SetTrigger(AnimHash.Enemy_Attack);
    }
}
