using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader : MonoBehaviour
{

    Material mMat;
    public Color ChangeColor;

    private void Start()
    {
        //먼저 마테리얼을 꽂아주고 순차적으로 해야한다. 안그러면 동시에 적용된다.
        mMat = GetComponent<Renderer>().material;
        mMat.SetColor("_Color",ChangeColor);
    }
}
