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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0 && Target != null)
            {
                float rand = UnityEngine.Random.Range(0, 1f);
                if (rand <= Player.Instance.mStats.Crit / 100)
                {
                    Target.Hit(Player.Instance.mStats.Atk * (1 + (Player.Instance.mStats.CritDamage / 100)));

                }
                else
                {
                    Target.Hit(Player.Instance.mStats.Atk);
                }
            }
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Gold"))
        {
            gameObject.SetActive(false);
        }
    }
}
