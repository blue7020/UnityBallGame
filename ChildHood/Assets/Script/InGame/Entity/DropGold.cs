using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{

    public static DropGold Instance;

    [SerializeField]
    public SpriteRenderer mRenderer;
    [SerializeField]
    public Sprite[] mSprites;

    public bool DropEnd;
    public int GoldStack =0;//골드 중첩

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
        mRenderer.sprite = mSprites[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //골드가 겹치면 골드 스택 증가
        //TODO 골드 스택이 증가하면 쌓인 골드 둘을 비교한 후 더 작은 쪽이 큰 쪽한테 골드를 넘겨주고 gameobject 비활성화
        if (other.gameObject.GetComponent<Player>())
        {
            Player.Instance.mInfoArr[Player.Instance.mID].Gold += Enemy.Instance.mInfoArr[Enemy.Instance.mID].Gold;
            UIController.Instance.ShowGold();
            gameObject.SetActive(false);
        }
        
    }
}
