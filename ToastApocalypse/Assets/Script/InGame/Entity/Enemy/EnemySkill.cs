using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public Enemy mEnemy;
    public float mDamage;
    public EnemyAttackArea mAttackArea;
    private int Count;
    private float BackupSpeed;
    private int SkillRand;

    public bool Skilltrigger;
    public Transform[] BulletStarter;
    public GameObject[] SkillObj;

    public void Skill()
    {
        Count = 0;
        switch (mEnemy.mID)
        {
            case 0://Mimic_Wood
                break;
            case 1://Slime_Butter
                break;
            case 2://Moldling
                MoldlingAttack();
                break;
            case 3://Mold_King
                StartCoroutine(MoldKingAttack());
                break;
            case 4://CursedPowder
                StartCoroutine(CursedPowder());
                break;
            case 5://PotatoGolem
                StartCoroutine(PotatoGolem());
                break;
            case 6://AngerTomato
                StartCoroutine(AngerTomato());
                break;
            case 7://Mimic_Silver
                break;
            case 8://Mimic_Gold
                break;
            case 9://Ketchup_Slime
                break;
            case 10://Hambug
                break;
            case 11://Flied
                Flied1();
                break;
            case 12://Burger-Pac
                CabbageBoomerang();
                break;
            case 13://Portatargo
                Potatargo();
                break;
            case 14://Dispenster
                ColaRay();
                break;
            case 15://creamSlime
                break;
            case 16://Cakon
                StartCoroutine(Cakon());
                break;
            case 17://creambun
                StartCoroutine(Creambun());
                break;
            case 18://fire hand
                StartCoroutine(FireHand());
                break;
            case 19://chocoshell
                StartCoroutine(ChocoShellIn());
                break;
            case 20://mrs. cake
                break;
            case 21://spirit of oven
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
            case 7://Mimic_Silver
                break;
            case 8://Mimic_Gold
                break;
            case 9://Ketchup_Slime
                AngerTomato2();
                break;
            case 10://Hambug
                break;
            case 11://Flied
                Flied2();
                break;
            case 12://Burger-Pac
                CabbageBoomerang();
                break;
            case 13://Portatargo
                break;
            case 14://Dispenster
                break;
            case 15://creamSlime
                CreamSlime();
                break;
            case 16://Cakon
                break;
            case 17://creambun
                break;
            case 18://fire hand
                break;
            case 19://chocoshell
                break;
            case 20://mrs. cake
                break;
            case 21://spirit of oven
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
                if (bolt.eType == eEnemyBulletType.normal)
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
            case 5:
                Vector3 dir5 = new Vector3(1, -1, 0);
                bolt.mRB2D.velocity = dir5.normalized * bolt.mSpeed;
                break;
            case 6:
                Vector3 dir6 = new Vector3(1, -1, 0);
                bolt.mRB2D.velocity = dir6.normalized * bolt.mSpeed;
                break;
            case 7:
                Vector3 dir7 = new Vector3(1, -1, 0);
                bolt.mRB2D.velocity = dir7.normalized * bolt.mSpeed;
                break;
            case 8:
                Vector3 dir8 = new Vector3(1, -1, 0);
                bolt.mRB2D.velocity = dir8.normalized * bolt.mSpeed;
                break;
            default:
                Debug.LogError("Wrong BulletID or bulletDir");
                break;
        }
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);

    }

    private IEnumerator MoveDelay(float time)
    {
        WaitForSeconds delay = new WaitForSeconds(time);
        float backup = mEnemy.mStats.Spd;
        mEnemy.mStats.Spd = 0;
        yield return delay;
        mEnemy.mStats.Spd = backup;
    }

    public IEnumerator MoldKingAttack()//id = 2
    {
        WaitForSeconds cool = new WaitForSeconds(0.3f);
        Count = 0;
        BackupSpeed = mEnemy.mStats.Spd;
        mEnemy.mStats.Spd = 0f;
        mEnemy.mRB2D.velocity = Vector3.zero;
        while (Count<3)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            ResetDir(1);
            Count++;
            yield return cool;
        }
        mEnemy.mStats.Spd = BackupSpeed;
    }

    private void MoldlingAttack()//id = 3
    {
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        ResetDir(0);
    }

    private IEnumerator PotatoGolem()//id = 5
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }

    private IEnumerator AngerTomato()//id = 6 , 9
    {
        WaitForSeconds Cool = new WaitForSeconds(1.5f);
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2 && Skilltrigger == true)
        {
            Skilltrigger = false;
            for (int i = 0; i < 4; i++)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
                ResetDir(2, i + 1);
            }
        }
        yield return Cool;
        Skilltrigger = true;
    }
    private void AngerTomato2()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(2, i);
        }
    }

    private IEnumerator CursedPowder()//id=4
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        Count = 0;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(0.11f));
        while (true)
        {
            if (Count > 10)
            {
                mEnemy.mStats.Spd = BackupSpeed;
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            Invoke("PowderBoom", 0.1f);
            yield return delay;

        }
    }
    private void PowderBoom()
    {
        int Xpos = Random.Range(-5, 6);
        int Ypos = Random.Range(-5, 6);
        Vector3 Pos = new Vector3(Xpos, Ypos, 0);
        Bullet bolt = BulletPool.Instance.GetFromPool(3);
        bolt.transform.localPosition = Pos;
        Count++;
    }

    private void Flied1()//id = 11
    {
        Count = 0;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        while (true)
        {
            Invoke("MoveFlied",0.1f);
            if (Count >= 15)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                StartCoroutine(MoveDelay(1.5f));
                break;
            }
        }
    }
    private void MoveFlied()
    {
        Vector3 Pos = mEnemy.mTarget.transform.position;
        Vector3 dir = Pos - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd*3);
        Count++;
    }

    private void Flied2()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(4, i);
        }
    }

    private void Potatargo()//id = 13
    {
        Count = 0;

        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP/2)
        {
            mEnemy.mStats.AtkSpd = mEnemy.mStats.AtkSpd *2f;
            BackupSpeed = mEnemy.mStats.Spd;
            mEnemy.mStats.Spd = 0f;
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            mEnemy.Nodamage = true;
            StartCoroutine(ShellIn());
        }
        while (true)
        {
            Invoke("PotatoShot", 0.3f);
            if (Count <= 4)
            {
                break;
            }
        }
    }

    private void CabbageBoomerang()//id =12
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            Bullet bolt = BulletPool.Instance.GetFromPool(6);
            bolt.transform.localPosition = mEnemy.transform.position;
            bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        }
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            XShot();
        }
    }

    private void XShot()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(5, i);
        }
    }

    private IEnumerator ShellIn()//13
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        StartCoroutine(MoveDelay(5.1f));
        Count = 0;
        while (true)
        {
            if (Count >= 50)
            {
                mEnemy.mStats.Spd = BackupSpeed;
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                mEnemy.Nodamage = false;
                break;
            }
            else
            {
                Invoke("PotatoShot", 0.8f);
                yield return delay;
            }
        }
    }
    private void PotatoShot()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(5);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }


    private void ColaRay()//id=14
    {
        Count = 0;
        SkillRand = Random.Range(0, 3);
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            if (Skilltrigger == false)
            {
                Count = 0;
                StartCoroutine(MoveDelay(0.2f));
                StartCoroutine(ColaBoom());
                for (int i = 0; i < 2; i++)
                {
                    Enemy enemy = EnemyPool.Instance.GetFromPool(0);//햄버그 소환
                    enemy.mStats.Gold = 0;
                    enemy.mMaxHP -= 2; enemy.mCurrentHP -= 2;
                }
                StartCoroutine(ColaBoom());
                Skilltrigger = true;
            }
            BackupSpeed = mEnemy.mStats.Spd;
            mEnemy.mStats.Spd = 0f;
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        }
            StartCoroutine(DrinkRay());
    }

    private IEnumerator DrinkRay()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        StartCoroutine(MoveDelay(4.1f));
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        while (true)
        {
            Invoke("DrinkShot", 0.1f);
            if (Count >= 40)
            {
                mEnemy.mStats.Spd = BackupSpeed;
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            yield return delay;

        }
    }
    private void DrinkShot()
    {
        int bulletIndex = 0;
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dirR = Pos - transform.position;
        Vector3 dirL = Pos - transform.position*-1;
        switch (SkillRand)
        {
            case 0://cola
                bulletIndex = 8;
                break;
            case 1://sprite
                bulletIndex = 9;
                break;
            case 2://fanta
                bulletIndex = 10;
                break;
        }
        Bullet bolt1 = BulletPool.Instance.GetFromPool(bulletIndex);  Bullet bolt2 = BulletPool.Instance.GetFromPool(bulletIndex);
        bolt1.transform.localPosition = BulletStarter[0].transform.position;  bolt2.transform.localPosition = BulletStarter[1].transform.position;
        bolt1.mRB2D.velocity = dirR.normalized * bolt1.mSpeed;  bolt2.mRB2D.velocity = -dirL.normalized * bolt2.mSpeed;
        Count++;
    }

    private IEnumerator ColaBoom()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        while (true)
        {
            if (Count > 10)
            {
                mEnemy.mStats.Spd = BackupSpeed;
                break;
            }
            else
            {
                int Xpos = Random.Range(-5, 6);
                int Ypos = Random.Range(-5, 6);
                Vector3 Pos = new Vector3(Xpos, Ypos, 0);
                Bullet bolt = BulletPool.Instance.GetFromPool(7);
                bolt.transform.localPosition = Pos;
                Count++;
                yield return delay;
            }
        }

    }

    private void CreamSlime()//15
    {
        GameObject Cream= Instantiate(SkillObj[0],Player.Instance.CurrentRoom.transform);
        Cream.transform.position = mEnemy.transform.position;
    }

    private IEnumerator Cakon()//16
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        if (mEnemy.mTarget!=null)
        {
            mEnemy.mStats.Gold = 0;
            Bullet bolt= BulletPool.Instance.GetFromPool(11);
            bolt.transform.position = mEnemy.transform.position;
            mEnemy.Hit(mEnemy.mMaxHP);
        }

    }

    private IEnumerator Creambun()//17
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        Count = 0;
        StartCoroutine(MoveDelay(2.1f));
        while (true)
        {
            if (Count >= 20)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            CreamRay();
            yield return delay;
        }
    }

    private void CreamRay()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(12);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }

    private IEnumerator FireHand()//18
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        mEnemy.mStats.Spd += 3;
        Count = 0;
        while (true)
        {
            if (Count >= 20)
            {
                mEnemy.mStats.Spd -= 3;
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            Invoke("Point", 0.1f);
            yield return delay;
        }
    }
    private void Point()
    {
        Count++;
    }


    private IEnumerator ChocoShellIn()//19
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        Count = 0;
        StartCoroutine(MoveDelay(2.6f));
        while (true)
        {
            if (mEnemy.mCurrentHP<=mEnemy.mMaxHP/2)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
                mEnemy.Nodamage = true;
                if (Skilltrigger == false)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Enemy enemy = EnemyPool.Instance.GetFromPool(1);//크림슬라임 소환
                        enemy.mStats.Gold = 0;
                        enemy.mMaxHP -= 2; enemy.mCurrentHP -= 2;
                    }
                    Skilltrigger = true;
                }
                if (Count >= 10)
                {
                    mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                    mEnemy.Nodamage = false;
                    break;
                }
                else
                {
                    Invoke("CreamShot", 0.1f);
                    yield return delay;
                    Invoke("ChocoBoom", 1f);
                }
            }
            else
            {
                ChocoBoom();
                break;
            }
        }
    }
    private void ChocoBoom()
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("sdsa");
            int Xpos = Random.Range(-5, 6);
            int Ypos = Random.Range(-5, 6);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            Bullet bolt = BulletPool.Instance.GetFromPool(13);
            bolt.transform.localPosition = Pos;
        }
    }
    private void CreamShot()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(12, i);
        }
        Count++;
    }
}
