using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DetectSpaceBar : MonoBehaviour
{
    public string MainScene;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) SpaceHit();
        if (Input.GetKeyDown(KeyCode.S)) SourceHit();
        if (Input.GetKeyDown(KeyCode.T)) TwitterHit();
        if (Input.GetKeyDown(KeyCode.I)) InstagramHit();
    }

    public void SpaceHit()
    {
        SceneManager.LoadScene(MainScene, LoadSceneMode.Single);
    }

    public void SourceHit()
    {
        Application.OpenURL("https://github.com/DanielGooding/The_Study_Of_Dune");
    }
    public void TwitterHit()
    {
        Application.OpenURL("https://twitter.com/OneStarOneHeart");
    }
    public void InstagramHit()
    {
        Application.OpenURL("https://www.instagram.com/onestar0neheart");
    }
}
