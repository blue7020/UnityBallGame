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
    public ParticleSystem mParticle;

    public bool isMoving,isAttack,isHide;

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
        GameController.Instance.mDarkness = this;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (GameController.Instance.mMapMaterialController.ChapterArr[1] == false)
        {
            isHide = false;
        }
        mWhite.SetActive(false);
        mParticle.gameObject.SetActive(true);
        mRenderer.color = new Color(1, 1, 1, 1);
        gameObject.SetActive(true);
    }

    public void Moving()
    {
        if (isMoving)
        {
            Debug.Log("dash");
            mParticle.gameObject.SetActive(true);
            mRenderer.color = new Color(1, 1, 1, 1);
            isAttack = true;
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
        int MaxTime = 25;
        int CurrentTitme = 0;
        while (true)
        {
            if (CurrentTitme>=MaxTime)
            {
                SoundController.Instance.SESound(22);
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
        WaitForSeconds delay = new WaitForSeconds(3f);
        Vector2 pos = new Vector2(Player.Instance.transform.position.x + 30, Player.Instance.transform.position.y);
        mRB2D.DOMove(pos, 3f);
        yield return delay;
        AttackEnd();
        yield return delay;
        if (!isHide)
        {
            isMoving = true;
            Moving();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void AttackEnd()
    {
        mParticle.gameObject.SetActive(false);
        mWhite.SetActive(false);
        isAttack = false;
        mRenderer.color = new Color(1, 1, 1, 0);
    }

    public IEnumerator ResetPattern()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        if (GameController.Instance.mMapMaterialController.ChapterArr[1]==true)
        {
            gameObject.SetActive(false);
            yield return delay;
        }
        else
        {
            Show();
            mRB2D.DOMove(transform.position, 0.1f);
            yield return delay;
            isMoving = true;
            Moving();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& isAttack)
        {
            Player.Instance.Damage(1);
        }
    }
}
