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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //컷씬에서 이제 게임이 시작될 때 1.5초 후 움직이게 함으로써 플레이어가 안전하게
        //탑을 내려갈 수 있게 배려 / 현재는 테스트를 위해 바로 움직임
        isMoving = true;
        Moving();
    }

    public void Moving()
    {
        if (isMoving)
        {
            mWhite.SetActive(false);
            StartCoroutine(Dash());
        }
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
