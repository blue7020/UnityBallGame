using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private Sprite[] mSprites;

    private double mCurrentProgress;
    private double mMaxProgress;

    private double mTouchPower;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //TODO Load Save Data
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Touch()
    {
        mCurrentProgress += mTouchPower;
        //터치효과 적용 <- 원석의 작업 진행도 증가
        float progress = (float)(mCurrentProgress / mMaxProgress);//이미지의 fillamount가 있기 때문에 float을 사용한다
        Debug.Log(progress);
        //작업 진행도에 따라 원석의 이미지 교체
        //게이지바에 진행도 표시
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
