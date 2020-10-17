using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desolving : MonoBehaviour
{
    public Material mat;

    private float DesolveAmount;
    private float DesolveSpeed=1;
    public bool IsDesolving;

    private void Update()
    {
        if (IsDesolving)
        {
            DesolveAmount = Mathf.Clamp01(DesolveAmount + DesolveSpeed*Time.deltaTime);
            mat.SetFloat("_Desolve",DesolveAmount);
        }
        else
        {
            DesolveAmount = Mathf.Clamp01(DesolveAmount - DesolveSpeed * Time.deltaTime);
            mat.SetFloat("_Desolve", DesolveAmount);
        }
    }

    public void FadeIn()
    {
        IsDesolving = false;
    }

    public void FadeOut()
    {
        IsDesolving = true;
    }
}
