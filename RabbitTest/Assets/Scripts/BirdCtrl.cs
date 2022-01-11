using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdCtrl : MonoBehaviour {

    public Transform txtScore;  // 프리팹 
    int speed;                  // 이동 속도 
    public bool isDrop = false;	// Use this for initialization
    public float Score;
    public bool isRight;
    Animator anim;

    private void Awake()
    {
        anim = transform.GetComponent<Animator>();
    }

    private void Start()
    {
        anim.Play("BirdFly");
    }

    void Update()
    {
        speed = Random.Range(3, 7);

        float amtMove = speed * Time.smoothDeltaTime;

        if (!isDrop)
        {
            if (isRight)
            {
                transform.Translate(Vector3.right * amtMove);
            }
            else
            {
                transform.Translate(Vector3.right * -amtMove);
            }
        }

        // 화면을 벗어난 오브젝트 제거 
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);
        if (view.y < -50 || (isRight&& view.x > Screen.width + 50)|| (!isRight && view.x < Screen.width - 1000))
        {
            Destroy(gameObject);
        }

    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    void DropBird()
    {
        isDrop = true;
        int playerpoint = GameController.Instance.mScore;
        SoundController.Instance.SESound(2);
        int point = (int)(playerpoint * Score);
        if (point==0)
        {
            point = 1;
        }
        // 감점 표시 
        Transform obj = Instantiate(txtScore) as Transform;
        obj.GetComponent<Text>().text = "<color=red><size=20>-"+ point + "</size></color>";
        // World 좌표를 Viewport 좌표로 변환 
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        obj.position = transform.position;
        GameController.Instance.AddScore(-point);
        anim.Play("Death");
    }
}
