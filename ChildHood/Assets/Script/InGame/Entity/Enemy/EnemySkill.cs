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
    [SerializeField]
    private EnemyPool mEnemyPool;

    public bool Skilltrigger;
    public int Skilltick;

    public void Skill()
    {
        Skilltick = 0;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
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
            case 4://KingSlime
                KingSlime();
                break;
            case 5://PotatoGolem
                PotatoGolem();
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }

    public IEnumerator MoldKingAttack()//id = 2
    {
        WaitForSeconds cool = new WaitForSeconds(0.1f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        ResetDir(0);
        yield return cool;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }

    private void MoldlingAttack()//id = 3
    {
        ResetDir(0);
    }
    public void ResetDir(int ID)
    {
        Bullet bolt = mBulletPool.GetFromPool(ID);
        bolt.transform.position = mBulletPos.position;
        if (bolt.Type == eBulletType.homing)
        {
            StartCoroutine(bolt.MoveToPlayer());
        }
        else if (bolt.Type == eBulletType.normal)
        {
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        }
    }

    private void KingSlime()
    {
        if (mEnemy.mCurrentHP>5)
        {
            mEnemy.mCurrentHP -= 2;
            Enemy mSpawnEnemy = mEnemyPool.GetFromPool();
            mSpawnEnemy.transform.position = mBulletPos.position;
        }
        
    }

    private void PotatoGolem()
    {
        Skilltrigger = true;
        if (Skilltick==0)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            StartCoroutine(MoveToPlayerGolem());
        }
        else
        {
            Skilltick++;
        }
        
    }

    public IEnumerator MoveToPlayerGolem()
    {
        if (Skilltrigger==true)
        {
            if (Skilltick<60)
            {
                //구를 때 충돌 시 1초간 기절
                WaitForSeconds one = new WaitForSeconds(0.1f);
                Skilltrigger = false;
                Vector3 Pos = Player.Instance.transform.position;
                Vector3 dir = Pos - transform.position;
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
                mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mInfoArr[mEnemy.mID].Spd * 2);
                Skilltick++;
                yield return one;
            }
            else if(Skilltick>=60)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                Skilltick = -7;
            }
            
        }
        
    }
}
