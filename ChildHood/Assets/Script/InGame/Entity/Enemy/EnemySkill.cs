using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public Enemy mEnemy;
    public float mDamage;
    public EnemyAttackArea mAttackArea;
    int Count;

    public bool Skilltrigger;
    public int Skilltick;

    public void Skill()
    {
        Skilltick = 0;
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
            case 4://CursedPowder
                CursedPowder();
                break;
            case 5://PotatoGolem
                PotatoGolem();
                break;
            case 6://AngerTomato
                StartCoroutine(AngerTomato());
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }

    public void DieSkill()
    {
        switch (mEnemy.mID)
        {
            case 0://Mimic_Wood
                break;
            case 1://Slime
                break;
            case 2://Mold_King
                break;
            case 3://Moldling
                break;
            case 4://KingSlime
                break;
            case 5://PotatoGolem
                break;
            case 6://AngerTomato
                AngerTomato2();
                break;
            case 7://SandWitch
                break;
            default:
                Debug.LogError("wrong Enemy ID");
                break;
        }
    }

    public void ResetDir(int ID, int bulletDir=0)
    {
        Bullet bolt = BulletPool.Instance.GetFromPool(ID);
        bolt.transform.position = mEnemy.transform.position;
        switch (bulletDir)
        {
            case 0:
                if (bolt.Type == eBulletType.normal)
                {
                    Vector3 Pos = Player.Instance.transform.position;
                    Vector3 dir = Pos - transform.position;
                    bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
                }
                break;
            case 1:
                Vector3 dir1 = new Vector3(-1, 1, 0);
                bolt.mRB2D.velocity = dir1.normalized * bolt.mSpeed;
                break;
            case 2:
                Vector3 dir2 = new Vector3(1, 1, 0);
                bolt.mRB2D.velocity = dir2.normalized * bolt.mSpeed;
                break;
            case 3:
                Vector3 dir3 = new Vector3(-1, -1, 0);
                bolt.mRB2D.velocity = dir3.normalized * bolt.mSpeed;
                break;
            case 4:
                Vector3 dir4 = new Vector3(1, -1, 0);
                bolt.mRB2D.velocity = dir4.normalized * bolt.mSpeed;
                break;
            default:
                Debug.LogError("Wrong BulletID or bulletDir");
                break;
        }

    }

    public IEnumerator MoldKingAttack()//id = 2
    {
        WaitForSeconds cool = new WaitForSeconds(0.1f);
        ResetDir(1);
        yield return cool;
    }

    private void MoldlingAttack()//id = 3
    {
        ResetDir(0);
    }

    private void KingSlime()//id = 4
    {
        if (mEnemy.mCurrentHP>5)
        {
            mEnemy.mCurrentHP -= 2;
            Enemy mSpawnEnemy = EnemyPool.Instance.GetFromPool(0);
            mSpawnEnemy.transform.position = mEnemy.transform.position;
        }
        
    }

    private void PotatoGolem()//id = 5
    {
        Skilltrigger = true;
        if (Skilltick==0)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            StartCoroutine(MoveToPlayerGolem());
        }
        else
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
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
                WaitForSeconds pointOneSec = new WaitForSeconds(0.1f);
                Skilltrigger = false;
                Vector3 Pos = Player.Instance.transform.position;
                Vector3 dir = Pos - transform.position;
                mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.Stats.Spd * 2);
                Skilltick++;
                yield return pointOneSec;
            }
            else if(Skilltick>=60)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                Skilltick = -7;
            }  
        }
    }

    private IEnumerator AngerTomato()//id = 6
    {
        WaitForSeconds Cool = new WaitForSeconds(1.5f);
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2 && Skilltrigger == true)
        {
            Skilltrigger = false;
            for (int i = 0; i < 4; i++)
            {
                ResetDir(2, i + 1);
            }
        }
        yield return Cool;
        Skilltrigger = true;
    }
    private void AngerTomato2()
    {
        for (int i = 0; i < 4; i++)
        {
            ResetDir(2, i + 1);
        }
    }

    private void CursedPowder()
    {
        Count = 0;
        StartCoroutine(PowderBoom());
    }
    private IEnumerator PowderBoom()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        while (true)
        {
            mEnemy.mRB2D.velocity = Vector3.zero;
            if (Count>5)
            {
                break;
            }
            else
            {
                int Xpos = Random.Range(-7, 8);
                int Ypos = Random.Range(-7, 8);
                Vector3 Pos = new Vector3(Xpos, Ypos, 0);
                Bullet bolt = BulletPool.Instance.GetFromPool(3);
                bolt.transform.localPosition = Pos;
                Count++;
                yield return delay;
            }
        }
        
    }
}
