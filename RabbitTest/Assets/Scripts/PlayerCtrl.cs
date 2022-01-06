using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{

    public Transform[] tile;        // 프리팹 
    public Transform[] item;
    public Transform[] bird;
    public Transform gold;



    public Transform startPos;
    public Transform endPos;

    public SpriteRenderer mRenderer;
    public Transform spPoint;
    Transform newTile;
    float maxY = 0;

    int speedSide = 11;             // 좌우 이동 속도 
    public int speedJump = 16;             // 점프 속도 
    int gravity = 20;               // 추락 속도 

    public Vector3 moveDir = Vector3.zero;
    Vector3 ItemLastSpawnPos = Vector3.zero;
    Vector3 GoldLastSpawnPos = Vector3.zero;

    public bool isDead = false;
    public bool isJump = false;
    public bool isNodamage = false;
    public Animator anim,spriteAnim;


    public float[] MovingTileSpawnRate;
    public float[] BreakTileSpawnRate;
    public float[] MobSpawnRate;
    public float[] ItemSpawnRate;
    public float GoldSpawnRate;

    void Start()
    {
        // 모바일 단말기 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        spPoint = GameObject.Find("spPoint").transform;

        // Tile 만들기 
        float x = Random.Range(-5f, 5f) * 0.5f;
        Vector3 pos = new Vector3(x, spPoint.position.y, 0.3f);
        float TileCode = Random.Range(0, 1f);
        if (TileCode <= MovingTileSpawnRate[GameController.Instance.mStage])
        {
            pos = new Vector3(0, spPoint.position.y, 0.3f);
            newTile = Instantiate(tile[1], pos, Quaternion.identity) as Transform;
        }
        else
        {
            newTile = Instantiate(tile[0], pos, Quaternion.identity) as Transform;
        }
        spriteAnim.Play("Normal");
        PlayerController.Instance.mReviveTile.gameObject.SetActive(false);
    }

    //-------------------
    // 게임 루프
    //-------------------
    void Update()
    {
        if (isDead) return;
        if (!UIController.Instance.pause)
        {
            JumpPlayer();          // Player 점프
            MovePlayer();          // Player 이동 
            MoveCamera();          // 카메라 이동 
            UIController.Instance.ScoreRefresh();
        }
    }


    //-------------------
    // Player 점프
    //-------------------
    void JumpPlayer()
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Tile"));

        if (hit&& !isJump)
        {
            moveDir.y = speedJump;
            SoundController.Instance.SESound(0);
        }
    }

    //-------------------
    //  Player 이동
    //-------------------
    void MovePlayer()
    {
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);

        if (view.y < -50)
        {   // 화면 아래를 벗어나면  
            isDead = true;      // 게임 오버 
            GameOver();
            return;
        }

        moveDir.x = 0;      // Player의 좌우 이동 방향

        // Mobile 처리 
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // 중력 가속도 센서 읽기 
            float x = Input.acceleration.x;

            // 왼쪽으로 기울였나?
            if (x < 0 && view.x > 35)
            {
                mRenderer.flipX = false;
                moveDir.x = (2 * x) * speedSide;
            }
            if (x > 0 && view.x < Screen.width - 35)
            {
                mRenderer.flipX = true;
                moveDir.x = (2 * x) * speedSide;
            }

        }
        else
        {   // Keyboard 읽기 
            float key = Input.GetAxis("Horizontal");
            if ((key < 0 && view.x > 35))
            {
                mRenderer.flipX = false;
                moveDir.x = key * speedSide;
            }
            if ((key > 0 && view.x < Screen.width - 35))
            {
                mRenderer.flipX = true;
                moveDir.x = key * speedSide;
            }
        }

        // 매 프레임마다 점프 속도 감소
        moveDir.y -= gravity * Time.deltaTime;
        transform.Translate(moveDir * Time.smoothDeltaTime);


        //애니메이션 설정
        if (moveDir.y > 0)
        {
            isJump = true;
            anim.Play("PlayerJump");
        }
        else
        {
            isJump = false;
            anim.Play("PlayerIdle");
        }
    }

    void MoveCamera()
    {
        // Player 최대 높이 구하기 
        if (transform.position.y > maxY)
        {
            maxY = transform.position.y;

            // 카메라 위치 이동 
            Camera.main.transform.position = new Vector3(0, maxY - 2f, -10);
            int point = (int)maxY;
            GameController.Instance.mHeight = point;
            GameController.Instance.AddScore(point/3);
        }
        MakeItem();
        MakeTile();

    }

    void MakeTile()
    {
        //가장 최근의 tile과 spPoint와의 거리 구하기
        if (spPoint.position.y - newTile.position.y >= 4)
        {
            float x = Random.Range(-5f, 5f) * 0.5f;
            Vector3 pos = new Vector3(x, spPoint.position.y, 0.3f);
            float TileCode = Random.Range(0, 1f);
            if (TileCode <= MovingTileSpawnRate[GameController.Instance.mStage])
            {
                pos = new Vector3(0, spPoint.position.y, 0.3f);
                newTile = Instantiate(tile[1], pos, Quaternion.identity) as Transform;
            }
            else
            {
                TileCode = Random.Range(0, 1f);
                if (TileCode <= BreakTileSpawnRate[GameController.Instance.mStage])
                {
                    newTile = Instantiate(tile[2], pos, Quaternion.identity) as Transform;
                }
                else
                {
                    newTile = Instantiate(tile[0], pos, Quaternion.identity) as Transform;
                }
            }
        }
    }

    void MakeItem()
    {
        if (Random.Range(1, 1000) < 990) return;

        // 오브젝트 표시 위치 
        Vector3 pos = Vector3.zero;

        if (Random.Range(0, 1f) < MobSpawnRate[GameController.Instance.mStage])
        {
            pos.y = maxY + Random.Range(0, 5f);
            // 몬스터 만들기 
            if (Random.Range(0, 1f) <= 0.5f)
            {
                pos.x = -7f;
                Transform obj = Instantiate(bird[GameController.Instance.mStage], pos, Quaternion.identity);
                obj.GetComponent<BirdCtrl>().isRight = true;
            }
            else
            {
                pos.x = 7f;
                Transform obj = Instantiate(bird[GameController.Instance.mStage], pos, Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if (Random.Range(0, 1f) < ItemSpawnRate[GameController.Instance.mStage])
        {
            for (int i = 1; i < item.Length; i++)
            {
                // 화면의 선물 수를 2개 이내로 제한 
                int n1 = GameObject.FindGameObjectsWithTag("Item1").Length;
                int n2 = GameObject.FindGameObjectsWithTag("Item2").Length;
                int n3 = GameObject.FindGameObjectsWithTag("Item3").Length;

                if (n1 + n2 + n3 >= 2) return;

                //Item 만들기 
                while (true)
                {
                    pos.x = Random.Range(-2.5f, 2.5f) * 0.5f;
                    pos.y = Random.Range(2, 8);
                    if (pos != ItemLastSpawnPos)
                    {
                        ItemLastSpawnPos = pos;
                        break;
                    }
                }

                int n = Random.Range(0, 3);
                Transform obj = Instantiate(item[n], transform.position + pos, Quaternion.identity) as Transform;
            }
        }
        if (Random.Range(0,1f)<GoldSpawnRate)
        {
            for (int i = 1; i < 2; i++)
            {
                // 화면의 선물 수를 2개 이내로 제한 
                int n1 = GameObject.FindGameObjectsWithTag("Item4").Length;
                int n2 = GameObject.FindGameObjectsWithTag("Item4").Length;
                int n3 = GameObject.FindGameObjectsWithTag("Item4").Length;

                if (n1 + n2 + n3 >= 2) return;
                //Gold 만들기 
                while (true)
                {
                    pos.x = Random.Range(-2f, 2f) * 0.5f;
                    pos.y = Random.Range(2, 8);
                    if (pos != GoldLastSpawnPos)
                    {
                        GoldLastSpawnPos = pos;
                        break;
                    }
                }
                Transform obj = Instantiate(gold, transform.position + pos, Quaternion.identity) as Transform;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.transform.tag)
        {
            case "Item1":
                // 선물 제거 
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Item2":
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Item3":
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Item4":
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Bird":
                //몹 충돌 처리 
                if (!isNodamage)
                {
                    coll.transform.SendMessage("DropBird", SendMessageOptions.DontRequireReceiver);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        switch (coll.transform.tag)
        {
            case "Tile":
                if (!isJump)
                {
                    coll.transform.SendMessage("BreakPlatform", SendMessageOptions.DontRequireReceiver);
                }
                break;
        }
    }

    //게임 오버 설정
    void GameOver()
    {
        SoundController.Instance.SESound(5);
        Time.timeScale = 0;
        SaveDataController.Instance.DeadAds = true;
        UIController.Instance.ShowReviveWindow();
    }

    public IEnumerator ShowReviveTile()
    {
        WaitForSeconds time = new WaitForSeconds(3.5f);
        isNodamage = true;
        spriteAnim.Play("Blink");
        speedJump +=6;
        PlayerController.Instance.mReviveTile.gameObject.SetActive(true);
        yield return time;
        speedJump =16;
        spriteAnim.Play("Normal");
        isNodamage = false;
        PlayerController.Instance.mReviveTile.gameObject.SetActive(false);
    }
}
