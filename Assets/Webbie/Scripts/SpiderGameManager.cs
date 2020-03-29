using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGameManager : MonoBehaviour
{
    public GameObject[] DeactivateOnWin;
    public GameObject[] ActivateOnWin;
    public GameObject[] ActivateOnLose;

    public bool debug = true;

    public void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.F12))
        {
            GameWin();
        }
        if (debug && Input.GetKeyDown(KeyCode.F11))
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        foreach (GameObject obj in DeactivateOnWin)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in ActivateOnLose)
        {
            obj.SetActive(true);
        }
    }

    public void GameWin()
    {
        foreach (GameObject obj in DeactivateOnWin)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in ActivateOnWin)
        {
            obj.SetActive(true);
        }
    }
}
