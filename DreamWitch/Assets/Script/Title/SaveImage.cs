using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveImage : MonoBehaviour
{
    public GameObject mLoadingImage;

    public void EndSaving()
    {
        mLoadingImage.SetActive(false);
    }
}
