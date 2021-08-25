using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{

    public Transform[] tile;        // 프리팹 
    public Transform[] item;
    public Transform[] bird;


    public Transform startPos;
    public Transform endPos;

    public SpriteRenderer mRenderer;
    Transform spPoint;
    Transform newTile;
    float maxY = 0;
    bool isJump = false;

    int speedSide = 10;             // 좌우 이동 속도 
    int speedJump = 16;             // 점프 속도 
    int gravity = 25;               // 추락 속도 

    Vector3 moveDir = Vector3.zero;

    bool isDead = false;
    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();

        // 모바일 단말기 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        spPoint = GameObject.Find("spPoint").transform;

        // Tile 만들기 
        newTile = Instantiate(tile[0], spPoint.position, spPoint.rotation) as Transform;
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
            MakeItem();
            GameController.Instance.mHeight = (int)maxY;
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

        if (hit)
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
            if (x < -0.2f && view.x > 35)
            {
                mRenderer.flipX = false;
                moveDir.x = 2 * x * speedSide;
            }
            if (x > 0.2f && view.x < Screen.width - 35)
            {
                mRenderer.flipX = true;
                moveDir.x = 2 * x * speedSide;
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
            anim.Play("PlayerJump");
        }
        else
        {
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
            GameController.Instance.AddScore((int)maxY/2);
        }

        //가장 최근의 tile과 spPoint와의 거리 구하기
        if (spPoint.position.y - newTile.position.y >= 4)
        {
            float x = Random.Range(-5f, 5f) * 0.5f;
            Vector3 pos = new Vector3(x, spPoint.position.y, 0.3f);
            float TileCode = Random.Range(0, 1f);
            if (TileCode<=0.25f)
            {
                pos = new Vector3(0, spPoint.position.y, 0.3f);
                newTile = Instantiate(tile[1], pos, Quaternion.identity) as Transform;
            }
            else
            {
                newTile = Instantiate(tile[0], pos, Quaternion.identity) as Transform;
            }

            //나뭇가지의 회전방향 설정
            int mx = (Random.Range(0, 2) == 0) ? -1 : 1;
            int my = (Random.Range(0, 2) == 0) ? -1 : 1;
            newTile.GetComponent<SpriteRenderer>().material.mainTextureScale = new Vector2(mx, my);
        }
    }
    void MakeItem()
    {
        if (Random.Range(1, 1000) < 990) return;

        // 오브젝트 표시 위치 
        Vector3 pos = Vector3.zero;

        if (Random.Range(0, 100) < 50)
        {
            pos.y = maxY + Random.Range(0, 5f);
            // 참새 만들기 
            if (Random.Range(0,1f)<=0.5f)
            {
                pos.x = -7f;
                Instantiate(bird[GameController.Instance.mStage], pos, Quaternion.identity);
            }
            else
            {
                pos.x = 7f;
                Transform obj = Instantiate(bird[GameController.Instance.mStage], pos, Quaternion.identity);
                obj.rotation = Quaternion.Euler(180f, 0, 180f);
            }
        }
        else
        {
            for (int i = 1; i < item.Length; i++)
            {
                // 화면의 선물 수를 2개 이내로 제한 
                int n1 = GameObject.FindGameObjectsWithTag("Item1").Length;
                int n2 = GameObject.FindGameObjectsWithTag("Item2").Length;
                int n3 = GameObject.FindGameObjectsWithTag("Item3").Length;

                if (n1 + n2 + n3 >= 2) return;

                //Item 만들기 
                pos.x = Random.Range(-2f, 2f);
                pos.y = Random.Range(2f, 8f);

                int n = Random.Range(0, 3);
                Transform obj = Instantiate(item[n], transform.position + pos, Quaternion.identity) as Transform;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.transform.tag)
        {
            case "Item1":
                SoundController.Instance.SESound(1);
                // 선물 제거 
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Item2":
                SoundController.Instance.SESound(1);
                // 선물 제거 
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Item3":
                SoundController.Instance.SESound(1);
                // 선물 제거 
                coll.transform.SendMessage("DisplayScore");
                break;

            case "Bird":
                if (coll.transform.eulerAngles.z != 0) return;

                SoundController.Instance.SESound(2);
                // 참새 추락 처리 
                coll.transform.SendMessage("DropBird", SendMessageOptions.DontRequireReceiver);
                break;
        }
    }

    //게임 오버 설정
    void GameOver()
    {
        SaveDataController.Instance.DeadAds = true;
        SaveDataController.Instance.mUser.HighScore= GameController.Instance.mHighScore;
        SaveDataController.Instance.Save();
        SceneManager.LoadScene(0);
    }

}
