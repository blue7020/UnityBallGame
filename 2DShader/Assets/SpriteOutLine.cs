using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOutLine : MonoBehaviour
{

    public Rigidbody2D mRB2D;
    public float mSpeed = 3;
    public SpriteRenderer mRenderer;
    public Material mat;

    private void Start()
    {
        mat.SetFloat("_OutlineThickness", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(hori,ver);
        dir = dir.normalized * mSpeed;
        mRB2D.velocity = dir;

        if (Input.GetKey(KeyCode.A))
        {
            mRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mRenderer.flipX = false;
        }
    }

    public IEnumerator Outline()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        mat.SetFloat("_OutlineThickness", 2);
        yield return delay;
        mat.SetFloat("_OutlineThickness", 0);
    }
}
