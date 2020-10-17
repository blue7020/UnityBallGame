using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public Enemy mEnemy;
    public float mDamage;
    public EnemyAttackArea mAttackArea;
    public int Count;
    public int SkillCount;
    private int SkillRand;

    public bool Skilltrigger, Skilltrigger2;
    public Transform[] BulletStarter;
    public GameObject[] SkillObj;
    public GameObject mBarrier,BulletTrash;
    public int BoltCount;//현재까지 소환한 볼트의 개수
    public EnemyObjAttackArea mEnemyObj;//에너미의 무기 등의 판정

    private void Start()
    {
        if (GameController.Instance.IsTutorial==false)
        {
            BulletTrash = Instantiate(GameController.Instance.BulletTrash, mEnemy.CurrentRoom.transform);
            Skilltrigger = false;
            Skilltrigger2 = false;
            SkillCount = 0;
        }
    }

    public void IceBarrier()
    {
        mBarrier = Instantiate(SkillObj[0], mEnemy.transform);
        mEnemy.HasBarrier = true;
    }

    public void RefreashBullet()
    {
        if (BoltCount >49)
        {
            Destroy(BulletTrash);
            BulletTrash = Instantiate(GameController.Instance.BulletTrash, mEnemy.CurrentRoom.transform);
        }
    }

    public void Skill()
    {
        Count = 0;
        if (mEnemy.mID < 22)
        {
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
                case 4://Tomocto
                    StartCoroutine(Tomocto());
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
                    StartCoroutine(Flied1());
                    break;
                case 12://Burger-Pac
                    CabbageBoomerang();
                    break;
                case 13://Portatargo
                    Potatargot();
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
                    break;
                case 19://chocoshell
                    ChocoShellIn();
                    break;
                case 20://mrs. cake
                    StartCoroutine(Misscake());
                    break;
                case 21://spirit of oven
                    Oven();
                    break;
                default:
                    Debug.LogError("wrong Enemy ID");
                    break;
            }
        }
        else if (mEnemy.mID >= 22)
        {
            switch (mEnemy.mID)
            {
                case 22://Wasabi
                    break;
                case 23://Sushinobi
                    Sushinobi();
                    break;
                case 24://Rolling Sushi
                    StartCoroutine(RollingSushi());
                    break;
                case 25://TakoYaki
                    break;
                case 26://YakiStove
                    YakiStove();
                    break;
                case 27://Ebimaid
                    Ebimaid();
                    break;
                case 28://Kramen
                    Kramen();
                    break;
                case 29://Ice
                    break;
                case 30://CoolTomato
                    StartCoroutine(CoolTomato());
                    break;
                case 31://IceWing
                    IceWing();
                    break;
                case 32://Pizzring
                    StartCoroutine(PizzShot());
                    break;
                case 33://PhantomPizz
                    PhantomPizz();
                    break;
                case 34://Nuggetoth
                    StartCoroutine(Nuggetoth());
                    break;
                case 35://Tunight
                    Tunight();
                    break;
                case 36://Bagoyle
                    Bagoyle();
                    break;
                case 37://Zombie Toast
                    break;
                case 38://Bread Crust
                    break;
                case 39://Sandwich Fanatic
                    StartCoroutine(SandwichFanatic());
                    break;
                case 40://Tomster
                    Tomster();
                    break;
                case 41://Pantaurus
                    PanTaurus();
                    break;
                case 42://SandWitch
                    SandWitch();
                    break;
                case 43://Scarecrow_Melee
                    ScareCrow();
                    break;
                case 44://Scarecrow_Range
                    ScareCrow2();
                    break;
                case 45://CursedPowder
                    StartCoroutine(CursedPowder());
                    break;
                default:
                    Debug.LogError("wrong Enemy ID");
                    break;
            }
        }
    }

    public void DieSkill()
    {
        Count = 0;
        if (mEnemy.mID < 22)
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
                case 4://Tomocto
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
        else if (mEnemy.mID >= 22)
        {
            switch (mEnemy.mID)
            {
                case 22://Wasabi
                    Wasabi();
                    break;
                case 23://Sushinobi
                    break;
                case 24://Rolling Sushi
                    break;
                case 25://TakoYaki
                    break;
                case 26://YakiStove
                    break;
                case 27://Ebimaid
                    break;
                case 28://Kramen
                    break;
                case 29://Ice
                    Ice();
                    break;
                case 30://CoolTomato
                    CoolTomato2();
                    break;
                case 31://IceWing
                    break;
                case 32://Pizzring
                    break;
                case 33://PhantomPizz
                    break;
                case 34://Nuggetoth
                    break;
                case 35://Tunight
                    break;
                case 36://Bagoyle
                    break;
                case 37://Zombie Toast
                    ZombieToast();
                    break;
                case 38://Bread Crust
                    break;
                case 39://Sandwich Fanatic
                    break;
                case 40://Tomster
                    break;
                case 41://Pantaurus
                    break;
                case 42://SandWitch
                    break;
                case 43://Scarecrow_Melee
                    break;
                case 44://Scarecrow_Range
                    break;
                case 45://CursedPowder
                    break;
                default:
                    Debug.LogError("wrong Enemy ID");
                    break;
            }
        }
    }

    public void ResetDir(int ID, int bulletDir = 0)
    {
        Bullet bolt = BulletPool.Instance.GetFromPool(ID);
        if (BulletTrash!=null)
        {
            bolt.transform.SetParent(BulletTrash.transform);
        }
        bolt.mEnemy = mEnemy;
        bolt.transform.position = mEnemy.transform.position;
        switch (bulletDir)
        {
            case 0:
                Vector3 Pos = Player.Instance.transform.position;
                Vector3 dir = Pos - transform.position;
                bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
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
        mEnemy.mRB2D.velocity = Vector3.zero;
        mEnemy.IsTraking = false;
        yield return delay;
        mEnemy.IsTraking = true;
    }

    public IEnumerator MoldKingAttack()//id = 2
    {
        WaitForSeconds cool = new WaitForSeconds(0.3f);
        Count = 0;
        StartCoroutine(MoveDelay(0.5f));
        mEnemy.mRB2D.velocity = Vector3.zero;
        while (Count < 3)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            ResetDir(1);
            Count++;
            yield return cool;
        }
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

    private IEnumerator CursedPowder()//id=45
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP)
        {
            for (int i = 1; i < 5; i++)
            {
                ResetDir(3, i);
            }
        }
        ResetDir(3);
        ResetDir(3);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }


    private IEnumerator Flied1()//id = 11
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        mEnemy.IsTraking = false;
        mEnemy.SpeedAmount += 0.5f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.SpeedAmount -= 0.5f;
        mEnemy.IsTraking = true;
    }

    private void Flied2()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(4, i);
        }
    }

    private void Potatargot()//id = 13
    {
        Count = 0;
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            if (Skilltrigger == false)
            {
                mEnemy.mStats.AtkSpd *= 3f;
                Skilltrigger = true;
            }
            mEnemy.Nodamage = true;
            StartCoroutine(ShellIn());
        }
        if (!Skilltrigger2)
        {
            StartCoroutine(PotatoShoting());
        }

    }

    private IEnumerator PotatoShoting()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            Invoke("PotatoShot", 0.1f);
            if (Count >= 2)
            {
                break;
            }
            yield return delay;
        }
    }
    private IEnumerator ShellIn()//13
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        Skilltrigger2 = true;
        StartCoroutine(MoveDelay(4f));
        Count = 0;
        while (true)
        {
            if (Count > 39)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                mEnemy.Nodamage = false;
                Skilltrigger2 = false;
                break;
            }
            Invoke("PotatoRay", 0.1f);
            yield return delay;
        }
    }

    private void PotatoRay()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(5);
        bolt.mEnemy = mEnemy;
        bolt.transform.SetParent(BulletTrash.transform);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }

    private void CabbageBoomerang()//id =12
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            Bullet bolt = BulletPool.Instance.GetFromPool(6);
            bolt.mEnemy = mEnemy;
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

    private void PotatoShot()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(5);
        bolt.mEnemy = mEnemy;
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
                StartCoroutine(MoveDelay(0.4f));
                for (int i = 0; i < 2; i++)
                {
                    Enemy enemy = EnemyPool.Instance.GetFromPool(0);//햄버그 소환
                    enemy.mStats.Gold = 0;
                    enemy.mMaxHP -= 2; enemy.mCurrentHP -= 2;
                }
                StartCoroutine(ColaBoom());
                Skilltrigger = true;
            }
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
            if (Count >= 39)
            {
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
        Vector3 dirR = Pos - transform.position; Vector3 dirL = Pos - transform.position * -1;
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
        Bullet bolt1 = BulletPool.Instance.GetFromPool(bulletIndex); Bullet bolt2 = BulletPool.Instance.GetFromPool(bulletIndex);
        bolt1.mEnemy = mEnemy; bolt2.mEnemy = mEnemy;
        bolt1.transform.SetParent(BulletTrash.transform); bolt2.transform.SetParent(BulletTrash.transform);
        bolt1.transform.localPosition = BulletStarter[0].transform.position; bolt2.transform.localPosition = BulletStarter[1].transform.position;
        bolt1.mRB2D.velocity = dirR.normalized * bolt1.mSpeed; bolt2.mRB2D.velocity = -dirL.normalized * bolt2.mSpeed;
        Count++;
    }

    private IEnumerator ColaBoom()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        while (true)
        {
            if (Count > 10)
            {
                break;
            }
            else
            {
                int Xpos = Random.Range(-5, 6);
                int Ypos = Random.Range(-5, 6);
                Vector3 Pos = new Vector3(Xpos, Ypos, 0);
                Bullet bolt = BulletPool.Instance.GetFromPool(7);
                bolt.mEnemy = mEnemy;
                bolt.transform.localPosition = Pos;
                Count++;
                yield return delay;
            }
        }

    }

    private void CreamSlime()//15
    {
        GameObject Cream = Instantiate(SkillObj[0], Player.Instance.CurrentRoom.transform);
        Cream.transform.position = mEnemy.transform.position;
    }

    private IEnumerator Cakon()//16
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        if (mEnemy.mTarget != null)
        {
            mEnemy.mStats.Gold = 0;
            Bullet bolt = BulletPool.Instance.GetFromPool(11);
            bolt.mEnemy = mEnemy;
            bolt.transform.position = mEnemy.transform.position;
            mEnemy.Hit(mEnemy.mMaxHP,1,false);
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
        bolt.mEnemy = mEnemy;
        bolt.transform.SetParent(BulletTrash.transform);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }


    private void ChocoShellIn()//19
    {
        Count = 0;
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            if (Skilltrigger == false)
            {
                mEnemy.mStats.AtkSpd *= 2;
                for (int i = 0; i < 2; i++)
                {
                    Enemy enemy = EnemyPool.Instance.GetFromPool(1);//크림슬라임 소환
                    enemy.transform.position = mEnemy.transform.position;
                    enemy.mStats.Gold = 0;
                    enemy.mMaxHP -= 3; enemy.mCurrentHP -= 3;
                }
                Skilltrigger = true;
            }
            mEnemy.Nodamage = true;
            StartCoroutine(Shellin2());
        }
        StartCoroutine(ChocoBooming());

    }

    private IEnumerator ChocoBooming()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            Invoke("ChocoBoom", 0.1f);
            if (Count >= 8)
            {
                break;
            }
            yield return delay;
        }
    }
    private IEnumerator Shellin2()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        StartCoroutine(MoveDelay(4f));
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        Count = 0;
        while (true)
        {
            if (Count > 39)
            {
                mEnemy.Nodamage = false;
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            Invoke("ChocoShot", 0.1f);
            yield return delay;
        }
    }

    private void ChocoBoom()
    {
        int Xpos = Random.Range(-6, 7);
        int Ypos = Random.Range(-6, 7);
        Vector3 Pos = new Vector3(Xpos, Ypos, 0);
        Bullet bolt = BulletPool.Instance.GetFromPool(13);
        bolt.mEnemy = mEnemy;
        bolt.transform.position = mEnemy.transform.position + Pos;
        Count++;
    }
    private void ChocoShot()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(14, i);
        }
        Count++;
    }

    private IEnumerator Misscake()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(2f));
        while (true)
        {
            if (Count >= 5)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
                {
                    Enemy enemy = EnemyPool.Instance.GetFromPool(2);//케이콘 소환
                    enemy.transform.position = mEnemy.transform.position;
                    enemy.mStats.Gold = 0;
                }
                break;
            }
            Invoke("BerryBoom", 0.1f);
            yield return delay;
        }
    }
    private void BerryBoom()
    {
        Bullet bolt = BulletPool.Instance.GetFromPool(15);
        bolt.mEnemy = mEnemy;
        bolt.transform.SetParent(BulletTrash.transform);
        bolt.transform.position = Player.Instance.transform.position;
        Count++;
    }

    private void Oven()
    {
        Vector3 point = new Vector3(0, 3, 0);
        FirePillar();
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            if (Skilltrigger == false)
            {
                mEnemy.mStats.AtkSpd += 2f;
                Skilltrigger = true;
            }
            mEnemy.Nodamage = true;
            StartCoroutine(OvenIn());
        }
        if (Skilltrigger2 == true)
        {
            Enemy enemy = EnemyPool.Instance.GetFromPool(3);//화염의손 소환
            enemy.mMaxHP = 6; enemy.mCurrentHP = 6;
            enemy.transform.position = mEnemy.transform.position + point;
        }
    }
    private IEnumerator OvenIn()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        Count = 0;
        Skilltrigger2 = true;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(4.5f));
        while (true)
        {
            if (Count >= 40)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                Skilltrigger2 = false;
                mEnemy.Nodamage = false;
                break;
            }
            Invoke("FireRay", 0.1f);
            yield return delay;
        }
    }
    private void FirePillar()
    {
        for (int i = 1; i < 5; i++)
        {
            int Xpos = Random.Range(-6, 7);
            int Ypos = Random.Range(-6, 7);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            GameObject pillar = Instantiate(SkillObj[0], Player.Instance.CurrentRoom.transform);
            pillar.transform.position = mEnemy.transform.position + Pos;
            Count++;
        }
        Count++;
    }
    private void FireRay()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(16);
        bolt.mEnemy = mEnemy;
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }

    //===================================ID >=22
    private void Wasabi()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(17, i);
        }
    }

    private void Sushinobi()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(18);
        bolt.mEnemy = mEnemy;
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
    }

    private IEnumerator RollingSushi()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        mEnemy.IsTraking = false;
        mEnemy.SpeedAmount += 2f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.SpeedAmount -= 2f;
        mEnemy.IsTraking = true;
        StartCoroutine(MoveDelay(0.5f));
    }

    private void YakiStove()
    {
        Count = 0;
        StartCoroutine(BallShot());
        if (Skilltrigger == false && mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            Skilltrigger = true;
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Phase, true);
            StartCoroutine(MoveDelay(1f));
            Vector3 Pos = Vector3.zero;
            for (int i = 0; i < 4; i++)
            {
                Enemy enemy = EnemyPool.Instance.GetFromPool(4);//타코야키 소환
                switch (i)
                {
                    case 0:
                        Pos = new Vector3(1, 0, 0);
                        break;
                    case 1:
                        Pos = new Vector3(0, 1, 0);
                        break;
                    case 2:
                        Pos = new Vector3(-1, 0, 0);
                        break;
                    case 3:
                        Pos = new Vector3(0, -1, 0);
                        break;
                }
                enemy.transform.position = mEnemy.transform.position + Pos;
            }
            mEnemy.mCurrentHP += 10;
            mEnemy.mHPBar.SetGauge(mEnemy.mCurrentHP, mEnemy.mMaxHP);
        }
    }
    private IEnumerator BallShot()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while (true)
        {
            if (Count > 3)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            StartCoroutine(TakoyakiBall());
        }
        yield return delay;
    }
    private IEnumerator TakoyakiBall()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        for (int i = 0; i < 3; i++)
        {
            Vector3 Pos = Player.Instance.transform.position;
            Vector3 dir = Pos - transform.position;
            Bullet bolt = BulletPool.Instance.GetFromPool(19);
            bolt.mEnemy = mEnemy;
            bolt.transform.SetParent(BulletTrash.transform);
            bolt.transform.localPosition = mEnemy.transform.position;
            bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
            Count++;
            yield return delay;
        }
    }

    private void Ebimaid()
    {
        StartCoroutine(ShrimpShot());
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            WaterPillar();
        }
    }
    private IEnumerator ShrimpShot()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        Count = 0;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(2f));
        while (true)
        {
            if (Count >= 3)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            Shrimp();
            yield return delay;
        }
    }
    private void Shrimp()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(20);
        bolt.mEnemy = mEnemy;
        bolt.transform.SetParent(BulletTrash.transform);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }
    private void WaterPillar()
    {
        for (int i = 1; i < 5; i++)
        {
            int Xpos = Random.Range(-6, 7);
            int Ypos = Random.Range(-6, 7);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            GameObject pillar = Instantiate(SkillObj[0], Player.Instance.CurrentRoom.transform);
            pillar.transform.position = mEnemy.transform.position + Pos;
            Count++;
        }
    }

    private void Kramen()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            StartCoroutine(MeatStockRay());
        }
        Tentacle();
    }

    private IEnumerator MeatStockRay()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        Count = 0;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(4.3f));
        while (true)
        {
            if (Count >= 40)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            MeatStock();
            yield return delay;
        }
    }
    private void MeatStock()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(21);
        bolt.mEnemy = mEnemy;
        bolt.transform.SetParent(BulletTrash.transform);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }
    private void Tentacle()
    {
        for (int i = 1; i < 10; i++)
        {
            int Xpos = Random.Range(-7, 8);
            int Ypos = Random.Range(-7, 8);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            GameObject tentacle = Instantiate(SkillObj[0], BulletTrash.transform);
            tentacle.transform.position = mEnemy.transform.position + Pos;
        }
    }


    private void Ice()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(22, i);
        }
    }

    private IEnumerator CoolTomato()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2 && Skilltrigger == true)
        {
            Skilltrigger = false;
            for (int i = 0; i < 4; i++)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
                ResetDir(22, i + 1);
            }
        }
        yield return delay;
        Skilltrigger = true;
    }
    private void CoolTomato2()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(22, i);
        }
    }


    private void IceWing()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            StartCoroutine(MoveDelay(0.5f));
            IceWing2();
        }
        else
        {
            mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
            StartCoroutine(IceWing1());
        }
    }
    private IEnumerator IceWing1()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        yield return delay;
        mEnemy.IsTraking = false;
        mEnemy.SpeedAmount += 1.5f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.SpeedAmount -= 1.5f;
        mEnemy.IsTraking = true;
    }
    private void IceWing2()
    {
        for (int i = 1; i < 9; i++)
        {
            ResetDir(22, i);
        }
    }

    private void PhantomPizz()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            IceWing2();
            if (Skilltrigger == false)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Phase, true);
                Skilltrigger = true;
                StartCoroutine(MoveDelay(1f));
                Vector3 Pos = Vector3.zero;
                for (int i = 0; i < 5; i++)
                {
                    Enemy enemy = EnemyPool.Instance.GetFromPool(5);//피즈링 소환
                    switch (i)
                    {
                        case 0:
                            Pos = new Vector3(1, 0, 0);
                            break;
                        case 1:
                            Pos = new Vector3(1, 1, 0);
                            break;
                        case 2:
                            Pos = new Vector3(-1, 1, 0);
                            break;
                        case 3:
                            Pos = new Vector3(0, -1, 0);
                            break;
                        case 4:
                            Pos = new Vector3(0, -1, 0);
                            break;
                    }
                    enemy.transform.position = mEnemy.transform.position + Pos;
                }
            }
        }
        StartCoroutine(PizzShot());
    }
    private IEnumerator PizzShot()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(0.8f));
        while (true)
        {
            if (Count >= 2)
            {
                mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
                break;
            }
            else
            {
                Pizz();
                yield return delay;
            }
        }
    }
    private void Pizz()
    {
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        Bullet bolt = BulletPool.Instance.GetFromPool(23);
        bolt.mEnemy = mEnemy;
        bolt.transform.SetParent(BulletTrash.transform);
        bolt.transform.localPosition = mEnemy.transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
        Count++;
    }

    private IEnumerator Nuggetoth()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        mEnemy.IsTraking = false;
        IceBoom();
        yield return delay;
        mEnemy.SpeedAmount += 3f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        yield return delay;
        mEnemy.SpeedAmount -= 3f;
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            IceBoom();
        }
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.IsTraking = true;
    }

    private void IceBoom()
    {
        for (int i = 1; i < 8; i++)
        {
            int Xpos = Random.Range(-6, 7);
            int Ypos = Random.Range(-6, 7);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            Bullet bolt = BulletPool.Instance.GetFromPool(24);
            bolt.mEnemy = mEnemy;
            bolt.transform.SetParent(BulletTrash.transform);
            bolt.transform.localPosition = mEnemy.transform.position + Pos;
        }
    }

    private void Tunight()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            if (Skilltrigger == false)
            {
                StopCoroutine(Tunight0());
                mEnemy.mStats.AtkSpd += 0.5f;
                IceBarrier();
                Skilltrigger = true;
            }
            StartCoroutine(Tunight1());
        }
        else
        {
            StartCoroutine(Tunight0());
        }
    }
    private IEnumerator Tunight0()
    {
        WaitForSeconds delay = new WaitForSeconds(1.7f);
        StartCoroutine(MoveDelay(2f));
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        yield return delay;
        IceBoom();
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }
    private IEnumerator Tunight1()
    {
        WaitForSeconds delay = new WaitForSeconds(1.7f);
        StartCoroutine(MoveDelay(2f));
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        yield return delay;
        mEnemy.SpeedAmount += 1f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        IceBoom();
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.SpeedAmount -= 1f;
    }


    private void Bagoyle()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            Bagoyle1();
        }
        else
        {
            StartCoroutine(Bagoyle0());
        }
    }
    private IEnumerator Bagoyle0()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        yield return delay;
        mEnemy.IsTraking = false;
        mEnemy.SpeedAmount += 1.5f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        yield return delay;
        mEnemy.SpeedAmount -= 1.5f;
        mEnemy.IsTraking = true;
    }
    private void Bagoyle1()
    {
        for (int i = 1; i < 9; i++)
        {
            ResetDir(25, i);
        }
    }

    private void ZombieToast()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(26, i);
        }
    }

    private IEnumerator SandwichFanatic()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(0.7f));
        if (Skilltrigger == false && mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            Skilltrigger = true;
            Vector3 Pos = Vector3.zero;
            for (int i = 0; i < 2; i++)
            {
                Enemy enemy = EnemyPool.Instance.GetFromPool(6);//빵모서리 소환
                switch (i)
                {
                    case 0:
                        Pos = new Vector3(1.5f, 0, 0);
                        break;
                    case 1:
                        Pos = new Vector3(-1.5f, 0, 0);
                        break;
                }
                enemy.transform.position = mEnemy.transform.position + Pos;
            }
        }
        ToastBoom(8);
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }

    private void ToastBoom(int count)
    {
        for (int i = 1; i < count; i++)
        {
            int Xpos = Random.Range(-6, 7);
            int Ypos = Random.Range(-6, 7);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            Bullet toast = BulletPool.Instance.GetFromPool(27);
            toast.transform.SetParent(BulletTrash.transform);
            toast.mEnemy = mEnemy;
            toast.transform.position = mEnemy.transform.position + Pos;
        }
    }

    private void Tomster()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            if (Skilltrigger == false)
            {
                Skilltrigger = true;
                Vector3 Pos = Vector3.zero;
                for (int i = 0; i < 2; i++)
                {
                    Enemy enemy = EnemyPool.Instance.GetFromPool(7);//좀비소트스 소환
                    switch (i)
                    {
                        case 0:
                            Pos = new Vector3(1.5f, 0, 0);
                            break;
                        case 1:
                            Pos = new Vector3(-1.5f, 0, 0);
                            break;
                    }
                    enemy.mStats.Gold = 0;
                    enemy.mCurrentHP -= 6;
                    enemy.transform.position = mEnemy.transform.position + Pos;
                }
            }
        }
        StartCoroutine(Tomster1());
    }
    private IEnumerator Tomster1()
    {
        WaitForSeconds delay = new WaitForSeconds(1.7f);
        StartCoroutine(MoveDelay(2f));
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            ToastShot();
        }
        yield return delay;
        mEnemy.SpeedAmount += 2f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        ToastShot();
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.SpeedAmount -= 2f;
    }
    private void ToastShot()
    {
        for (int i = 1; i < 5; i++)
        {
            ResetDir(28, i);
        }
    }


    private void PanTaurus()
    {
        if (mEnemy.mCurrentHP <= mEnemy.mMaxHP / 2)
        {
            if (Skilltrigger == false)
            {
                mEnemy.mStats.AtkSpd += 0.5f;
                Skilltrigger = true;
            }
            ToastBoom(10);
        }
        else
        {
            StartCoroutine(PanTaurus1());
        }
    }
    private IEnumerator PanTaurus1()
    {
        WaitForSeconds delay = new WaitForSeconds(1.7f);
        StartCoroutine(MoveDelay(2f));
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        mEnemy.SpeedAmount += 1f;
        Vector3 dir = Player.Instance.transform.position - transform.position;
        mEnemy.mRB2D.velocity = dir.normalized * (mEnemy.mStats.Spd * (1 + mEnemy.SpeedAmount));
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        mEnemy.SpeedAmount -= 1f;
    }

    private void SandWitch()
    {
        if (SkillCount >= 3)
        {
            StartCoroutine(Sandwitch1());
        }
        else
        {
            ToothPickFall();
            SkillCount++;
        }
    }
    private void ToothPickFall()
    {
        for (int i = 1; i < 8; i++)
        {
            int Xpos = Random.Range(-6, 7);
            int Ypos = Random.Range(-6, 7);
            Vector3 Pos = new Vector3(Xpos, Ypos, 0);
            Bullet toast = BulletPool.Instance.GetFromPool(29);
            toast.mEnemy = mEnemy;
            toast.transform.SetParent(BulletTrash.transform);
            toast.transform.position = mEnemy.transform.position + Pos;
        }
    }
    private IEnumerator Sandwitch1()
    {
        WaitForSeconds delay = new WaitForSeconds(2.5f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        for (int i = 0; i < 2; i++)
        {
            GameObject bread = Instantiate(SkillObj[i], Player.Instance.CurrentRoom.transform);
        }
        StartCoroutine(MoveDelay(2.4f));
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
        SkillCount = 0;
    }


    private void ScareCrow()
    {
        if (mEnemy.mCurrentHP >= mEnemy.mMaxHP)
        {
            mEnemy.mCurrentHP = mEnemy.mMaxHP;
        }
        else
        {
            mEnemy.mCurrentHP += 100;
        }
    }
    private void ScareCrow2()
    {
        Bullet bolt = BulletPool.Instance.GetFromPool(8);
        bolt.mEnemy = mEnemy;
        bolt.mDamage = 8.5f;
        bolt.transform.position = mEnemy.transform.position;
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        bolt.mRB2D.velocity = dir.normalized * bolt.mSpeed;
    }

    private IEnumerator Tomocto()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, true);
        StartCoroutine(MoveDelay(0.8f));
        if (mEnemy.mCurrentHP<=mEnemy.mMaxHP)
        {
            AngerTomato2();
        }
        StartCoroutine(TomatoFall());
        yield return delay;
        mEnemy.mAnim.SetBool(AnimHash.Enemy_Attack, false);
    }
    private IEnumerator TomatoFall()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        int count = 0;
        while (true)
        {
            if (count>=12)
            {
                break;
            }
            else
            {
                int Xpos = Random.Range(-6, 7);
                int Ypos = Random.Range(-6, 7);
                Vector3 Pos = new Vector3(Xpos, Ypos, 0);
                Bullet tomato = BulletPool.Instance.GetFromPool(30);
                tomato.mEnemy = mEnemy;
                tomato.transform.SetParent(BulletTrash.transform);
                tomato.transform.position = mEnemy.transform.position + Pos;
                count++;
            }
            yield return delay;
        }
    }
}
