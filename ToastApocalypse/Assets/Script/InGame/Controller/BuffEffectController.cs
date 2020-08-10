using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffectController : MonoBehaviour
{
    public static BuffEffectController Instance;

    public Sprite[] mSprite;

    public SkillEffect mEffect;
    public List<SkillEffect> EffectList;

    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
