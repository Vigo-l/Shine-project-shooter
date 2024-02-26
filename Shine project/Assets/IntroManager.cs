using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject WhiteScreen;
    public GameObject menu;

    private void Start()
    {
        StartCoroutine("Intro");
    }

    IEnumerator Intro()
    {
        WhiteScreen.SetActive(true);
        menu.SetActive(false);
        yield return new WaitForSeconds(3);
        WhiteScreen.SetActive(false);
        menu.SetActive(true);
    }
}
