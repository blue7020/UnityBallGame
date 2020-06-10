using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private eDirection Dir;
    protected Rigidbody2D mRB2D;
    private EnemySkill mEnemySkill;
    private float mDamage =1f;
    [SerializeField]
    protected float mSpeed;
    private void Awake()
    {
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        switch (Dir)
        {
            case eDirection.Up:
                transform.Translate(new Vector3(0,5,0));
                break;
            case eDirection.Right:
                transform.Translate(new Vector3(5, 0, 0));
                break;
            case eDirection.Left:
                transform.Translate(new Vector3(-5, 0, 0));
                break;
            case eDirection.Down:
                transform.Translate(new Vector3(0, -5, 0));
                break;
            default:
                Debug.LogError("Wrong Direction");
                break;
        }
    }

    public void ResetDir()
    {
        mRB2D.velocity = transform.forward * mSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent("Player"))
        {
            Player.Instance.Hit(mDamage);
        }
        if (other.gameObject.GetComponent("Walls"))
        {
            Destroy(gameObject);
        }
    }
}
