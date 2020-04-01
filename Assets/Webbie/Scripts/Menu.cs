using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // quit feature
    public GameObject quitText;
    private float quitTimer = 0;
    public float quitTimerLimit = 3;
    private bool quitInfoActive = false;
    void Start()
    {
        quitText.SetActive(false);
    }

    void Update()
    {
        if (!quitInfoActive && Input.GetKeyDown(KeyCode.Escape))
        {
            quitInfoActive = true;
            quitText.SetActive(true);
            quitTimer = quitTimerLimit;
        }
        if (!quitInfoActive) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
        }
        quitTimer -= Time.deltaTime;
        if (quitTimer <=0)
        {
            quitText.SetActive(false);
            quitInfoActive = false;
        }
    
    }
}
