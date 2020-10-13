using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desolver : MonoBehaviour
{
    public Material material;
    public float DesoloveAmount;
    public float mDesolveSpeed;
    public bool isDesolving;

    private void Update()
    {
        if (isDesolving)
        {
            DesoloveAmount = Mathf.Clamp01(DesoloveAmount+Time.deltaTime);
            material.SetFloat("_DesolveAmount", DesoloveAmount);
        }
        else
        {
            DesoloveAmount = Mathf.Clamp01(DesoloveAmount - Time.deltaTime);
            material.SetFloat("_DesolveAmount", DesoloveAmount);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isDesolving = true;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            isDesolving = false;
        }
    }

    public void StartDesolve(float desolveSpeed)
    {
        isDesolving = true;
        mDesolveSpeed = desolveSpeed;
    }
    public void StopDeslove(float desolveSpeed)
    {
        isDesolving = false;
        mDesolveSpeed = desolveSpeed;
    }
}
