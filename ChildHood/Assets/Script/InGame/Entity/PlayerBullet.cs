using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    public float mSpeed;
    public Rigidbody2D mRB2D;
    private Enemy Target;

    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(Guide());
    }

    private void Update()
    {
        if (Target!=null)
        {
            Vector2 dir = Target.transform.position - transform.position;
            mRB2D.velocity = dir.normalized * mSpeed;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    private IEnumerator Guide()
    {
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);//0.3초마다 타겟의 좌표 찾기
        while (true)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Enemy");//태그가 중복되면 가장 가까운 태그를 지목한다.
            if (obj != null)//obj가 있을때만 작동
            {
                Target = obj.GetComponent<Enemy>();
                Vector3 pos = Target.transform.position;
                Vector3 direction = pos - transform.position;//방향벡터 = 목적지 - 나
                direction = direction.normalized;
                mRB2D.velocity = direction * mSpeed;
                transform.LookAt(pos);//해당 방향으로 향한다.
            }

            yield return pointThree; }
    }


    public void ResetDir()
    {
        mRB2D.velocity = transform.forward * mSpeed;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0 && Target != null)
            {
                float rand = UnityEngine.Random.Range(0, 1f);
                if (rand <= Player.Instance.Stats.Crit / 100)
                {
                    Target.Hit(Player.Instance.Stats.Atk * (1 + (Player.Instance.Stats.CritDamage / 100)));

                }
                else
                {
                    Target.Hit(Player.Instance.Stats.Atk);
                }
            }
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            gameObject.SetActive(false);
        }
    }
}
