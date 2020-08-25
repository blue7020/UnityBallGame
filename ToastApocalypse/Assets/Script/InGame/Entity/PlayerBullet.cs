using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int mWeaponID;
    public float mDamage;
    public ePlayerBulletType eType;
    public bool returnPlayer;
    public bool returnCheck;
    public float mSpeed;
    public Rigidbody2D mRB2D;
    private Enemy Target;
    public Vector3 mboltscale;
    private void Awake()
    {
        mboltscale = transform.localScale;
        returnPlayer = false;
        returnCheck = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Target = other.GetComponent<Enemy>();
            if (Target.mCurrentHP > 0 && Target != null)
            {
                Target.Hit(mDamage);
            }
            if (eType == ePlayerBulletType.normal)
            {
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Walls"))
        {
            if (eType == ePlayerBulletType.normal)
            {
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (returnPlayer == true && eType == ePlayerBulletType.boomerang)
            {
                returnCheck = true;
                gameObject.SetActive(false);
            }

        }
    }
}
