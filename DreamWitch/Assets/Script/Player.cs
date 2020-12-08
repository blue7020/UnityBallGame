using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public LayerMask mGoundLayer;
    const float GROUND_CHECK_RADIUS = 0.01f;

    public Rigidbody2D mRB2D;
    public Animator mAnim;
    public SpriteRenderer mRenderer;
    public Transform mGroundChecker, mHoldZone, mBulletStart,Map;
    public Vector2 CheckPointPos;
    public HoldingItem mHold, mNowHold;
    public PlayerBolt mBolt;

    public Sprite[] mActionArr;
    public GameObject mAction;

    public float mMaxHP, mCurrentHP;
    public float mSpeed;
    public float mJumpForce;

    public bool isJump, isGround, isNoDamage,isCooltime,isHold;
    private float Hori;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mCurrentHP = mMaxHP;
            CheckPointPos = GameController.Instance.mStartPoint.transform.position + new Vector3(0, 2f, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Hori = Input.GetAxis("Horizontal");
        GroundCheck();
        Moving(Hori);
        Jump(isJump);
    }

    private void Update()
    {

        mAnim.SetFloat("yVelocity", mRB2D.velocity.y);
        if (Input.GetButton("Jump"))
        {
            isJump = true;
            mAnim.SetBool(AnimHash.Jump, true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
        }

        if (Input.GetKey(KeyCode.S)&&!isCooltime)//공격
        {
            if (GameController.Instance.Pause==false)
            {
                StartCoroutine(AttackCooltime());
            }
        }
    }

    public IEnumerator AttackCooltime()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        SoundController.Instance.SESound(2);
        PlayerBolt bolt = Instantiate(mBolt);
        bolt.transform.position = transform.position;
        bolt.transform.rotation = mRenderer.gameObject.transform.rotation;
        bolt.mRB2D.AddForce(mBulletStart.right * bolt.mSpeed, ForceMode2D.Impulse);
        isCooltime = true;
        yield return delay;
        isCooltime = false;
    }

    public void Jump(bool jumpFlag)
    {
        if (isGround && jumpFlag)
        {
            jumpFlag = false;
            SoundController.Instance.SESound(13);
            mRB2D.AddForce(new Vector2(0f, mJumpForce));
        }
    }
    private void Moving(float dir)
    {
        Vector3 move = new Vector3(dir, 0f, 0f);
        transform.position += move * Time.deltaTime * mSpeed;
        mAnim.SetFloat("xVelocity", dir);
        if (dir > 0)//좌
        {
            mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }
        if (dir < 0)//우
        {
            mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        }
    }

    public void GroundCheck()
    {
        isGround = false;
        Collider2D[] Coll2D = Physics2D.OverlapCircleAll(mGroundChecker.position, GROUND_CHECK_RADIUS, mGoundLayer);
        if (Coll2D.Length > 0)
        {
            isGround = true;
            mRB2D.velocity = Vector2.zero;
        }
        mAnim.SetBool(AnimHash.Jump, !isGround);
    }

    public void GetItem(HoldingItem obj)//아이템 획득
    {
        //인벤토리 구현
        //mNowHold = obj;
        //mNowHold.transform.SetParent(mHoldZone);
        //mNowHold.transform.position = Vector3.zero;
        //isHold = true;
    }

    //TODO 아이템 홀드

    public void Damage(float damage)
    {
        if (!isNoDamage)
        {
            mCurrentHP -= damage;
            GameController.Instance.Damege();
            StartCoroutine(DamageAnimation());
        }
        if (mCurrentHP<1)
        {
            GameController.Instance.GameOver();
        }
    }
    public IEnumerator DamageAnimation()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        isNoDamage = true;
        mRenderer.color = new Vector4(1,1,1,0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        yield return delay;
        mRenderer.color = new Vector4(1, 1, 1, 0.2f);
        yield return delay;
        mRenderer.color = Color.white;
        isNoDamage = false;
    }

    public void FallingDamage()
    {
        mCurrentHP -= 1;
        GameController.Instance.Damege();
        StartCoroutine(DamageAnimation());
        if (mCurrentHP > 0)
        {
            transform.position = CheckPointPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (mRB2D.velocity.y < 0 && transform.position.y > enemy.mHead.transform.position.y)//몬스터보다 높은 위치에 있을 때
            {
                enemy.Damage(2);
            }
            else
            {
                if (enemy.isDeath == false)//아니라면 플레이어가 데미지 받음
                {
                    Damage(enemy.mAtk);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MoveEnd"))
        {
            ShowAction(0);
            StartCoroutine(End());
        }
    }

    public IEnumerator End()
    {
        float time = 2.5f;
        WaitForSeconds delay = new WaitForSeconds(time);
        yield return delay;
        SoundController.Instance.mBGM.volume = 0;
        mRenderer.gameObject.transform.rotation = Quaternion.Euler(new Vector2(0, 180f));
        SoundController.Instance.SESound(6);
        ShowAction(1);
        time = 1f;
        delay = new WaitForSeconds(time);
        yield return delay;
        CutSceneController.Instance.CutSceneCamera();
        time = 5f;
        delay = new WaitForSeconds(time);
        yield return delay;
        CutSceneController.Instance.ChangeMainCamera();
        //time = 1f;
        //delay = new WaitForSeconds(time);
        //yield return delay;
        //CutSceneController.Instance.FadeOut();
    }

    public void ShowAction(int id)
    {
        switch (id)
        {
            case 0://생각
                SoundController.Instance.SESound(10);
                mAction.GetComponent<SpriteRenderer>().sprite = mActionArr[0];
                break;
            case 1://놀람
                SoundController.Instance.SESound(15);
                mAction.GetComponent<SpriteRenderer>().sprite = mActionArr[1];
                break;
            case 2://웃음
                SoundController.Instance.SESound(11);
                mAction.GetComponent<SpriteRenderer>().sprite = mActionArr[2];
                break;
            case 3://하트
                SoundController.Instance.SESound(12);
                mAction.GetComponent<SpriteRenderer>().sprite = mActionArr[3];
                break;
            case 4://물음표
                SoundController.Instance.SESound(14);
                mAction.GetComponent<SpriteRenderer>().sprite = mActionArr[4];
                break;
        }
        StartCoroutine(ActiveAction());
    }
    private IEnumerator ActiveAction()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);
        mAction.SetActive(true);
        yield return delay;
        mAction.SetActive(false);
    }
}
