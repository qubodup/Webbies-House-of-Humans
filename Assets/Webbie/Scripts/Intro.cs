using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // skip feature
    public GameObject skipText;
    private float skipTimer = 0;
    public float skipTimerLimit = 3;
    private bool skipInfoActive = false;

    // fade from black
    public GameObject fader;
    private CanvasGroup faderCG;

    // audio
    private AudioSource source;
    public AudioClip sndCutlery;
    public AudioClip sndEat;
    public AudioClip sndDrink;
    public AudioClip voice1;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;

    // cameras
    public GameObject camTable;
    public GameObject camMRS;
    public GameObject camMR;

    void Start()
    {
        source = GetComponent<AudioSource>();
        skipText.SetActive(false);
        faderCG = fader.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!skipInfoActive)
            {
                skipInfoActive = true;
                skipText.SetActive(true);
                skipTimer = skipTimerLimit;
            } else if (Input.GetKeyDown(KeyCode.Space))
            {
                //Application.Quit();
                //UnityEditor.EditorApplication.isPlaying = false;
                StartCoroutine(FadeOut());
            }
        }
        if (skipInfoActive)
        {
            skipTimer -= Time.deltaTime;
            if (skipTimer <=0)
            {
                skipText.SetActive(false);
                skipInfoActive = false;
            }
        }
    }
    IEnumerator FadeIn()
    {
        source.PlayOneShot(sndCutlery,.5f);
        while (faderCG.alpha > 0)
        {
            if (faderCG.alpha - (Time.deltaTime / 3) < 0) { faderCG.alpha = 0; }
            else
            {
                faderCG.alpha -= Time.deltaTime / 3;
            }

            yield return null;
        }
        StartCoroutine(Face1());
        yield return null;
    }
    IEnumerator FadeOut()
    {
        camTable.gameObject.SetActive(true);
        camMR.gameObject.SetActive(false);
        while (faderCG.alpha < 1)
        {
            if (faderCG.alpha + Time.deltaTime >= 1) { faderCG.alpha = 1; } else
            {
                faderCG.alpha += Time.deltaTime;
            }
            yield return null;
        }
        SceneManager.LoadScene(1);
        yield return null;
    }
    IEnumerator Face1()
    {
        camMR.gameObject.SetActive(true);
        camTable.gameObject.SetActive(false);
        source.PlayOneShot(sndEat);
        yield return new WaitForSeconds(2);
        StartCoroutine(Face2());
        yield return null;
    }
    IEnumerator Face2()
    {
        camMRS.gameObject.SetActive(true);
        camMR.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        StartCoroutine(Face3());
        yield return null;
    }
    IEnumerator Face3()
    {
        camMR.gameObject.SetActive(true);
        camMRS.gameObject.SetActive(false);
        source.PlayOneShot(sndDrink);
        yield return new WaitForSeconds(2);
        StartCoroutine(Face4());
        yield return null;
    }
    IEnumerator Face4()
    {
        camMRS.gameObject.SetActive(true);
        camMR.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        source.PlayOneShot(voice1);
        yield return new WaitForSeconds(4);
        StartCoroutine(Face5());
        yield return null;
    }
    IEnumerator Face5()
    {
        camMR.gameObject.SetActive(true);
        camMRS.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        source.PlayOneShot(voice2);
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Face6());
        yield return null;
    }
    IEnumerator Face6()
    {
        camMRS.gameObject.SetActive(true);
        camMR.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        source.PlayOneShot(voice3);
        yield return new WaitForSeconds(2);
        StartCoroutine(Face7());
        yield return null;
    }
    IEnumerator Face7()
    {
        camMR.gameObject.SetActive(true);
        camMRS.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        source.PlayOneShot(voice4);
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOut());
        yield return null;
    }
}
