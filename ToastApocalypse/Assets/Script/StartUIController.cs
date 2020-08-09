using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUIController : MonoBehaviour
{
    public static StartUIController Instance;

    public Image BrandLogo;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            StartCoroutine(ShowLogo());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ShowLogo()
    {
        WaitForSeconds delay = new WaitForSeconds(1.6f);
        yield return delay;
        BrandLogo.gameObject.SetActive(true);
        SceneManager.LoadScene(1);

    }
}
