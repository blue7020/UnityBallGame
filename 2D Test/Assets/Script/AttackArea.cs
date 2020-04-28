using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    private float mDamage;
    //List<GameObject> EnteredArr;

    public void SetDamage(float value)
    {
        mDamage = value;
        //EnteredArr = new List<GameObject>();
    }

    public void Attack()
    {
        gameObject.SetActive(true);
    }

    

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //    collision.gameObject.GetComponent<Enemy>().Hit(mDamage);
    //    }
    //}
    // Start is called before the first frame update
}
