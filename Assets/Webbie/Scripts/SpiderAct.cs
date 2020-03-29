using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAct : MonoBehaviour
{
    private AudioSource source;
    public AudioClip interactSuccess;
    public AudioClip interactFail;


    //energy
    private float energy = 1;
    public float regenerationRate = .1f;
    public float regenerationRateRunning = .05f;
    public float energyPriceWeb = .7f;
    public bool running = false;

    public GameObject gui;
    private GUI_Controller guiCont;

    private Color colWhite = Color.white;
    private Color colTransp = new Color(1,1,1,.2f);
    private Color colNear = new Color(.1f, 1, .1f, .2f);

    private GameObject targetWeb;

    private SpiderMovement spiderMovemenet;

    // how many nets to build to win
    public float netsToWin = 8f;

    // Start is called before the first frame update
    void Start()
    {
        spiderMovemenet = GetComponent<SpiderMovement>();
        guiCont = gui.GetComponent<GUI_Controller>();
        source = GetComponent<AudioSource>();
        energy = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (targetWeb && energy >= energyPriceWeb && targetWeb.GetComponent<SpriteRenderer>().color.a != 1)
            {
                energy -= energyPriceWeb;
                targetWeb.GetComponent<SpriteRenderer>().color = colWhite;
                guiCont.IncreaseCompletionValue(1f / netsToWin);
                source.PlayOneShot(interactSuccess); // sound
                spiderMovemenet.AnimNet(); // animation
            }
            else if (targetWeb && targetWeb.GetComponent<SpriteRenderer>().color.a != 1)
            {
                source.PlayOneShot(interactFail); // sound
            }
            else if (energy < energyPriceWeb)
            {
                source.PlayOneShot(interactFail); // sound
                guiCont.NegativeFeedback();
            }
            else // trying to build web not near web build site
            {
                source.PlayOneShot(interactFail); // sound
            }
        }
        if (energy < 1)
        {
            energy += Time.deltaTime * (running ? regenerationRateRunning : regenerationRate);
        }
        guiCont.SetEnergyValue(energy);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Finish" && other.gameObject.GetComponent<SpriteRenderer>().color.a != 1)
        {
            targetWeb = other.gameObject;
            // highlihght web
            other.gameObject.GetComponent<SpriteRenderer>().color = colNear;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        {
            targetWeb = null;
            // remove web highlight if it's not built yet
            if (other.gameObject.GetComponent<SpriteRenderer>().color.a != 1)
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = colTransp;
            }
        }
    }

}
