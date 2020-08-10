using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public SpriteRenderer mRenderer;
    public SpriteRenderer mRenderer_Top;

    public Color color = Color.white;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            color.a = 0.5f;
            mRenderer.color =color;
            mRenderer_Top.color = color;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            mRenderer_Top.color = Color.white;
            mRenderer.color = Color.white;
        }
    }
}
