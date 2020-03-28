using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Controller : MonoBehaviour
{
    public Image HouseFill;
    public Image BarFill;
    public Image WebColor;
    public Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        HouseFill.fillAmount = 0;
        BarFill.fillAmount = 1;
        WebColor.enabled = false;
    }


    public void SetCompletionValue(float value)
    {
        HouseFill.fillAmount = value;
    }



    public void SetEnergyValue(float value)
    {
        BarFill.fillAmount = value;
    }

    public void EnableDisableWeb(bool value)
    {
        WebColor.enabled = value;
    }

    public void NegativeFeedback()
    {

        if (Input.GetMouseButtonDown(1))
        {
            myAnimator.SetTrigger("TryWeb");
        }
    }
        
}
