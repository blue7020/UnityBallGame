using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera mMainCamera;
    [SerializeField]
    private EffectPool mEffectPool;

    // Start is called before the first frame update
    void Start()
    {
        mMainCamera = Camera.main;
    }

    private Ray GenerateRay(Vector3 screenPos)
    {
        screenPos.z = mMainCamera.nearClipPlane;
        Vector3 origin = mMainCamera.ScreenToWorldPoint(screenPos);
        screenPos.z = mMainCamera.farClipPlane;
        Vector3 dest = mMainCamera.ScreenToWorldPoint(screenPos);

        return new Ray(origin, dest - origin);
    }

    public bool CheckTouch(out Vector3 vec)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(i);//input.GetTouch를 한번에 부르기 위해 Touch 변수에 저장
                if (touch.phase == TouchPhase.Began)//begen(누르는 순간), ended(떼는 순간), moved만 사용
                {
                    Ray ray = GenerateRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (gameObject == hit.collider.gameObject)
                        {
                            vec = hit.point;
                            return true;
                        }
                    }
                }
            }
        }
        vec = Vector3.zero;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//0이 왼쪽, 1이 오른쪽 버튼, 2가 휠
        {
            Ray ray = GenerateRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (gameObject == hit.collider.gameObject)
                {
                    Timer effect = mEffectPool.GetFromPool();
                    effect.transform.position = hit.point;
                    GameController.Instance.Touch();
                }
            }
        }
        Vector3 pos;
        if (CheckTouch(out pos))
        {
            Timer effect = mEffectPool.GetFromPool();
            gameObject.transform.position = pos;
            GameController.Instance.Touch();
        }
    }
}
