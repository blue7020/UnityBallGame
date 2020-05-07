using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mRenderer;
    [SerializeField]
    private Sprite[] mSprites;

    private float mShiftGap;

    // Start is called before the first frame update
    void Awake()
    {
        mShiftGap = 1f/mSprites.Length;
    }

    private void OnEnable()
    {
        mRenderer.sprite = mSprites[0];
    }

    public void SetProgress(float progress)
    {
        //이미지설정
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
