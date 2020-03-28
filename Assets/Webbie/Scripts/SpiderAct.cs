using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAct : MonoBehaviour
{
    public GameObject gui;
    private GUI_Controller guiCont;

    private Color colWhite = Color.white;
    private Color colTransp = new Color(1,1,1,.2f);
    private Color colNear = new Color(1, 1, 1, .5f);

    // Start is called before the first frame update
    void Start()
    {
        guiCont = gui.GetComponent<GUI_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Finish")
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = colNear;
        }
    }

}
