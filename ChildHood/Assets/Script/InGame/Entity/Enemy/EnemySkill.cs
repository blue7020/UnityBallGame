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
    public BulletPool mBulletPool;
    [SerializeField]
    private Transform mBulletPos;
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
                StartCoroutine(MoldKingAttack());
                break;
            case 3://Moldling
                MoldlingAttack();
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }


    private IEnumerator MoldKingAttack()//id = 2
    {
        WaitForSeconds Cool = new WaitForSeconds(0.2f);
        Shot(0);
        yield return Cool;
    }

    private void MoldlingAttack()//id = 3
    {
        Shot(1);
    }

    public void ResetDir(int BulletID)
    {
        Bullet bolt = mBulletPool.GetFromPool(BulletID);
        bolt.transform.position = mBulletPos.position;
        bolt.transform.rotation = mBulletPos.rotation;
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - bolt.transform.position;
        dir = dir.normalized;
        bolt.mRB2D.velocity = dir * bolt.mSpeed;
    }
    public void Shot(int ID)
    {
        int BulletID = ID;
        ResetDir(BulletID);
    }
}
