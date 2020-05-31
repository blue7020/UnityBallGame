using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    [SerializeField]
    private float mDamage;

    public void Skill()
    {
        switch (Enemy.Instance.mID)
        {
            case 0://Mimic
                break;
            case 1://Slime
                break;
            case 2://Ghost
                StartCoroutine(GhostAttack());
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }


    private IEnumerator GhostAttack()//id = 2
    {
        WaitForSeconds cool = new WaitForSeconds(3f);
        yield return cool;
        EnemyAttackArea.Instance.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }
}
