using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterToUnicode : MonoBehaviour
{
    [SerializeField]
    private string mOrigin;

    // Start is called before the first frame update
    void Start()
    {
        char[] charArr = mOrigin.ToCharArray();//==Tostring()
        for (int i = 0; i < charArr.Length; i++)
        {
            int CharToInt = (int)charArr[i];
            Debug.LogFormat("{0}=> {1} // {2}",charArr[i], CharToInt, CharToInt.ToString("X4"));// x4는 표준 숫자 문자열 16진수
        }
           
    }
}
