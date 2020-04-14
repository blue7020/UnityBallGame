using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTextureScroll : MonoBehaviour
{
    private Material mMat;
    [SerializeField]
    private float mSpeed;
    private float mOffsetPerFrame;
    private Vector2 mOffset;

    // Start is called before the first frame update
    void Start()
    {
        //이 스크립트가 붙여넣어진 렌더러를 가져오기 때문에 따로 명시를 해주지 않는다.
        Renderer renderer = GetComponent<Renderer>();//매쉬 렌더러든 뭐든 어떤 렌더러든 가져오려고
        mMat = renderer.material;
        mOffsetPerFrame = mSpeed * Time.fixedDeltaTime;
        mOffset = Vector2.zero;
    }

    private void FixedUpdate()
    {
        //Vector2 offset = mMat.GetTextureOffset("_MainTex");
        //offset.y += mOffsetPerFrame;
        mOffset.y += mOffsetPerFrame;
        mMat.SetTextureOffset("_MainTex",mOffset);
    }

    //단점 : 마테리얼을 교체할 수는 있어도 마테리얼을 수정할 수는 없음(배경을 못 바꾼다)
}
