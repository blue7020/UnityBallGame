using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mDamage;
    public float mValue;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    public eEnemyBulletType eType;
    public eBulletEffect eEffectType;
    public float EffectTime;

    public Animator mAnim;
    public GameObject[] Spirte;
    public bool DamageOn;
    public Enemy mEnemy;

    private void Awake()
    {
        DamageOn = true;
    }

    private void Update()
    {
        if (eType ==eEnemyBulletType.homing)
        {
            StartCoroutine(MovetoPlayer());
        }
        
    }


    public void ShowWarning()
    {
        Spirte[1].gameObject.SetActive(false);
        Spirte[0].gameObject.SetActive(true);
    }
    public void Warning()
    {
        Spirte[0].gameObject.SetActive(false);
        Spirte[1].gameObject.SetActive(true);
    }

    public void Boom()
    {
        DamageOn = true;
        gameObject.SetActive(false);
    }

    public void Effecton()
    {
        Player.Instance.Hit(mDamage);
        if (eEffectType == eBulletEffect.stun)
        {
            if (Player.Instance.Stun == false && Player.Instance.NowDebuffArr[0] == null)
            {
                float rand = Random.Range(0,1f);
                if (rand > Player.Instance.mStats.CCReduce* (1+ Player.Instance.buffIncrease[6]))
                {
                    Player.Instance.DoEffect(5,45,EffectTime);
                }
            }
        }
        if (eEffectType == eBulletEffect.slow)
        {
            Player.Instance.DoEffect(4, 431, 3f, mValue);//몬스터나 함정에 의한 디버프는 40부터 시작
        }
        RemoveBullet();
    }

    private IEnumerator MovetoPlayer()
    {
        WaitForSeconds one = new WaitForSeconds(0.1f);
        Vector3 Pos = Player.Instance.transform.position;
        Vector3 dir = Pos - transform.position;
        mRB2D.velocity = dir.normalized * mSpeed;
        yield return one;
    }

    public void RemoveBullet()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (DamageOn == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Effecton();
            }
            if (other.gameObject.CompareTag("DestroyZone"))
            {
                RemoveBullet();
            }
            if (other.gameObject.CompareTag("Walls"))
            {
                if (eType != eEnemyBulletType.boom)
                {
                    RemoveBullet();
                }
            }
        }
    }
}
