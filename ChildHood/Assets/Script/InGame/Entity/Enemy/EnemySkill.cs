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

    private int RepeatControll;

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
        RepeatControll = 0;
        if (RepeatControll<4)
        {
            Debug.Log("발사");
            Invoke("Shot", 0.2f);
        }
    }
    private void MoldlingAttack()//id = 3
    {

    }

    public void ResetDir()
    {
        Bullet bolt = mbulletPool.GetFromPool(0);
        bolt.transform.position = mBulletPos.position;
        bolt.transform.rotation = mBulletPos.rotation;
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - bolt.transform.position;
        dir = dir.normalized;
        bolt.mRB2D.velocity = dir * bolt.mSpeed;
    }
    public void Shot()
    {
        ResetDir();
        RepeatControll++;
    }
}
