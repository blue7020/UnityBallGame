using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    public GoldPool mGoldPool;
    [SerializeField]
    public GaugeBarPool mGaugeBarPool;
    [SerializeField]
    public EnemyPool mEnemyPool;

    public bool pause;

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
        pause = false;
    }
    

    public void GamePause()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;
            
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
        }
    }
}
//TODO 맵 랜덤 생성 로직 구현
//TODO 몬스터 풀 만들어서 빈 방에 위치 할당
//TODO 씬 파일 만들어서 로비(캐릭터 선택까지)