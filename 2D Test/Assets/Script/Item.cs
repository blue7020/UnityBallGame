using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private bool mToggle = true;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
    }

    public void IsItemShow()
    {
        gameObject.SetActive(true);
        mAnim.SetBool(AnimHash.Show, mToggle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Potion!");
            gameObject.SetActive(false);
        }
    }
}
