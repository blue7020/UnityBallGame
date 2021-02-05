using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Darkness : MonoBehaviour
{
    public static Darkness Instance;

    public Rigidbody2D mRB2D;
    public SpriteRenderer mRenderer;
    public GameObject mWhite;

    public bool isMoving;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Moving()
    {
        if (isMoving)
        {
            mWhite.SetActive(false);
            StartCoroutine(Dash());
        }
    }

    public void MoveCutScene(Vector3 pos, float time)
    {
        mRB2D.DOMove(pos,time);
    }

    public IEnumerator Dash()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        isMoving = false;
        int MaxTime = 20;
        int CurrentTitme = 0;
        while (true)
        {
            if (CurrentTitme>=MaxTime)
            {
                //사운드 출력
                StartCoroutine(DashCooltime());
                break;
            }
            else
            {
                if (CurrentTitme>=MaxTime-5)
                {
                    mWhite.SetActive(true);
                }
                transform.position = new Vector3(Player.Instance.transform.position.x - 5, Player.Instance.transform.position.y, 0);

                CurrentTitme++;
            }
            yield return delay;
        }
    }

    public IEnumerator DashCooltime()
    {
        WaitForSeconds delay = new WaitForSeconds(6f);
        Vector2 pos = new Vector2(Player.Instance.transform.position.x + 30, Player.Instance.transform.position.y);
        mRB2D.DOMove(pos, 3f);
        yield return delay;
        isMoving = true;
        Moving();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Damage(1);
        }
    }
}
