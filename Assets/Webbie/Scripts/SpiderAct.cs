using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAct : MonoBehaviour
{
    private AudioSource source;

    public GameObject gui;
    private GUI_Controller guiCont;

    private Color colWhite = Color.white;
    private Color colTransp = new Color(1,1,1,.2f);
    private Color colNear = new Color(.1f, 1, .1f, .2f);

    private GameObject targetWeb;

    // Start is called before the first frame update
    void Start()
    {
        guiCont = gui.GetComponent<GUI_Controller>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (targetWeb)
            {
                targetWeb.GetComponent<SpriteRenderer>().color = colWhite;
                guiCont.IncreaseCompletionValue(1f / 4f);
                source.Play();
            } else
            {
                guiCont.NegativeFeedback();
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Finish" && other.gameObject.GetComponent<SpriteRenderer>().color.a != 1)
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = colNear;
            targetWeb = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish" && other.gameObject.GetComponent<SpriteRenderer>().color.a != 1)
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = colTransp;
            targetWeb = null;
        }
    }

}
