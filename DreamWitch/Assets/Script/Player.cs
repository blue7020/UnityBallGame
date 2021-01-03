using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public LayerMask mGoundLayer,mLadderLayer;
    const float GROUND_CHECK_RADIUS = 0.01f;
    const float FRONT_CHECK_RADIUS = 0.5f;
    const float SLIDE_FACTOR = 0.2f;

    public Rigidbody2D mRB2D;
    public Animator mAnim;
    public SpriteRenderer mRenderer;
    public Vector2 CheckPointPos;

    public HoldingItem mDropItem,mNowItem;
    public int mNowItemID;
    public PlayerBolt mBolt;

    public Sprite[] mActionArr;
    public GameObject mAction;
    public Enemy mEnemy;
    public bool mTextBoxChecker;

    public Transform mItemTransform, mGroundChecker, mHoldZone, mBulletStart, mFrontCheck, Map;
    public bool isCutScene, isJump,isMultipleJump,isCoyoteJump,isGround, isNoDamage, isCooltime, isItemCooltime, isClimbing, isTouchingFront, isWallSliding, isHold;
    public float Hori, Ver;
    public float mMaxHP, mCurrentHP, mMaxStamina,mCurrentStamina,mSpeed, mJumpForce;
    public float mDistance,mCoyoteTime,mWallSlidingSpeed;

    public int mMaxmJumpToken, mJumpToken;

    public float Gravity;

    public ParticleSystem mFootStep;
    private ParticleSystem.EmissionModule mFootEmission;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mTextBoxChecker = false;
            mCurrentHP = mMaxHP;
            mCurrentStamina = mMaxStamina;
            CheckPointPos = GameController.Instance.mStartPoint.transform.position + new Vector3(0, 2f, 0);
            if (mNowItem==null)
            {
                mNowItemID = -1;
            }
            else
            {
                mNowItemID = mNowItem.mID;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Gravity = mRB2D.gravityScale;
        mFootEmission = mFootStep.emission;
    }

    private void FixedUpdate()
    {
        Hori = Input.GetAxis("Horizontal");
        GroundCheck();
        LadderCheck();
        WallCheck();
        if (!isCutScene)
        {
            Moving(Hori);
        }
    }

    private void Update()
    {
        mAnim.SetFloat("yVelocity", mRB2D.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (!isClimbing && !isCutScene)
            {
                isJump = true;
                Jump();
            }
        }//점프

        if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
            mRB2D.velocity = new Vector2(mRB2D.velocity.x, mRB2D.velocity.y * 0.5f);
        }//하강

        if (Input.GetKey(KeyCode.Q) && !isCutScene)
        {
            if (GameController.Instance.Pause == false && !isCooltime)
            {
                StartCoroutine(Attack());
            }
        }//공격

        if (Input.GetKeyDown(KeyCode.F) && !isCutScene)
        {
            if (GameController.Instance.Pause == false)
            {
                if (mDropItem != null)
                {
                    GetItem(mDropItem);
                }
            }
        }//습득

        if (Input.GetKeyDown(KeyCode.E) && !isCutScene)
        {
            if (GameController.Instance.Pause == false)
            {
                if (mNowItemID > -1)
                {
                    ItemUse();
                }
            }
        }//사용
    }
    public IEnumerator Attack()
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

    public void Jump()
    {
        float rand = Random.Range(0, 1f);
        if (isGround && mJumpToken > 0)
        {
            isMultipleJump = true;
            mJumpToken--;
            if (rand <= 0.6f)
            {
                SoundController.Instance.SESound(13);
            }
            mRB2D.velocity= Vector2.up*mJumpForce;
            mAnim.SetBool(AnimHash.Jump, true);
        }
        else
        {
            if (isCoyoteJump && mJumpToken > 0)
            {
                isMultipleJump = true;
                mJumpToken--;
                if (rand <= 0.6f)
                {
                    SoundController.Instance.SESound(13);
                }
                mRB2D.velocity = Vector2.up * mJumpForce;
                mAnim.SetBool(AnimHash.Jump, true);
            }

            if (isMultipleJump && mJumpToken > 0)
            {
                mJumpToken--;
                if (rand <= 0.6f)
                {
                    SoundController.Instance.SESound(13);
                }
                mRB2D.velocity = Vector2.up * mJumpForce;
                mAnim.SetBool(AnimHash.Jump, true);
            }
        }
    }
    public void Moving(float dir)
    {
        Vector3 move = new Vector3(dir, 0f, 0f);
        transform.position += move * Time.deltaTime * mSpeed;
        mAnim.SetFloat("xVelocity", dir);
        if (dir != 0&&isGround)
        {
            mFootEmission.rateOverTime = 25f;
        }
        else
        {
            mFootEmission.rateOverTime = 0f;
        }
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
        if (!isClimbing)
        {
            bool wasGround = isGround;
            isGround = false;
            mRB2D.velocity = new Vector2(0, mRB2D.velocity.y);
            Collider2D[] Coll2D = Physics2D.OverlapCircleAll(mGroundChecker.position, GROUND_CHECK_RADIUS, mGoundLayer);
            if (Coll2D.Length > 0)
            {
                isGround = true;
                if (!wasGround)
                {
                    mJumpToken = mMaxmJumpToken;
                    mCurrentStamina = mMaxStamina;
                }
            }
            else
            {
                if (wasGround)
                {
                    StartCoroutine(CoyoteJumpDelay());
                }
            }
            mAnim.SetBool(AnimHash.Jump, !isGround);
        }
    }//땅 체크

    public IEnumerator CoyoteJumpDelay()
    {
        isCoyoteJump = true;
        yield return new WaitForSeconds(mCoyoteTime);
        isCoyoteJump = false;
    }

    public void WallCheck()//벽 체크
    {
        isTouchingFront = Physics2D.OverlapCircle(mFrontCheck.position, FRONT_CHECK_RADIUS, mGoundLayer);
        if (isTouchingFront && Mathf.Abs(Hori) > 0 &&mRB2D.velocity.y<0&& !isGround)
        {
            if (!isWallSliding)
            {
                mJumpToken = mMaxmJumpToken;
                isMultipleJump = false;
            }
            mRB2D.velocity = new Vector2(0f, Mathf.Clamp(mRB2D.velocity.y, -mWallSlidingSpeed, float.MaxValue));
            isWallSliding = true;

            if (Input.GetButtonDown("Jump")&& isWallSliding)
            {
                if (mJumpToken > 0&& mCurrentStamina>0)
                {
                    mJumpToken--;
                    mCurrentStamina--;
                    float rand = Random.Range(0, 1f);
                    if (rand <= 0.6f)
                    {
                        SoundController.Instance.SESound(13);
                    }
                    mRB2D.velocity = Vector2.up * mJumpForce;
                    mAnim.SetBool(AnimHash.Jump, true);
                }
            }
        }
        else
        {
            isWallSliding = false;
        }
        mAnim.SetBool(AnimHash.Grab, isWallSliding);
    }
    public void LadderCheck()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, mDistance, mLadderLayer);
        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                isClimbing = false;
            }
        }
        if (isClimbing && hitInfo.collider != null)
        {
            mAnim.SetBool(AnimHash.Climb, true);
            Ver = Input.GetAxisRaw("Vertical");
            mRB2D.velocity = new Vector2(mRB2D.velocity.x, Ver * mSpeed);
            mRB2D.gravityScale = 0;
        }
        else
        {
            mAnim.SetBool(AnimHash.Climb, false);
            isClimbing = false;
            mRB2D.gravityScale = Gravity;
        }
    }//사다리 체크

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

    public void GetItem(HoldingItem obj)
    {
        if (mNowItem != null)
        {
            mNowItem.Drop();
        }
        SoundController.Instance.SESound(16);
        mNowItem = obj;
        mDropItem = null;
        mNowItem.Hold();
        mNowItemID = mNowItem.mID;
        UIController.Instance.ItemImageChange(mNowItem.mRenderer.sprite);
    }

    public void ItemUse()
    {
        mNowItem.ItemUse();
        if (mNowItem.isConsumable)
        {
            ItemDelete();
        }
    }
    public void ItemDelete()
    {
        Destroy(mNowItem.gameObject);
        mNowItem = null;
        mNowItem.mID = -1;
        UIController.Instance.ItemImageChange();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            mEnemy = other.gameObject.GetComponent<Enemy>();
            if (mRB2D.velocity.y < 0 && transform.position.y > mEnemy.mHead.transform.position.y+0.3f)//몬스터보다 높은 위치에 있을 때
            {
                mEnemy.Damage(2);
                mJumpToken = mMaxmJumpToken;
                mCurrentStamina = mMaxStamina;
            }
            else
            {
                DamageCount();//아니라면 플레이어가 데미지 받음
            }
        }
    }

    private void DamageCount()
    {
        if (mEnemy.isCollide)
        {
            if (mEnemy.isDeath == false)
            {
                Damage(mEnemy.mAtk);
            }
            Invoke("DamageCount", 0.7f);
        }
        else
        {
            CancelInvoke();
        }
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
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(2f);
        mAction.SetActive(true);
        yield return delay;
        mAction.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TextBoxTop"))
        {
            mTextBoxChecker = true;
        }
        if (other.gameObject.CompareTag("TextBoxDown"))
        {
            mTextBoxChecker = false;
        }
    }
}
