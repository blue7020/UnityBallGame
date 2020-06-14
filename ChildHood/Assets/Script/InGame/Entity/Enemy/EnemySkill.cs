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
    [SerializeField]
    public BulletPool mBulletPool;
    [SerializeField]
    private Transform mBulletPos;

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
        ResetDir(0);
        yield return Cool;
    }

    private void MoldlingAttack()//id = 3
    {
        ResetDir(0);
    }

    public void ResetDir(int ID)
    {
        Bullet bolt = mBulletPool.GetFromPool(ID);
        bolt.transform.position = mBulletPos.position;
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - bolt.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
    }
}
