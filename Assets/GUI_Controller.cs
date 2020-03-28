using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Controller : MonoBehaviour
{
    public Image HouseFill;
    public Image BarFill;
    public Image WebColor;
    public Transform WebPosition;
    public Animator myAnimator;
    public float barTreshold = 5f;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        HouseFill.fillAmount = 0;
        //SetEnergyValue(1);
        WebColor.gameObject.SetActive(false);
        WebPosition.localPosition = new Vector3(WebPosition.localPosition.x, -755 + ( (755 + 150) * barTreshold), 0);
    }

    public void SetCompletionValue(float value)
    {
        HouseFill.fillAmount = value;
    }

    //public void Update()
    //{
    //    SetEnergyValue(BarFill.fillAmount - 0.001f);
    //}

    public void SetEnergyValue(float value)
    {
        BarFill.fillAmount = value;
        if(value >= barTreshold)
        {
            EnableDisableWeb(true);
        } else
        {
            EnableDisableWeb(false);
        }
    }

    public void EnableDisableWeb(bool value)
    {
        WebColor.gameObject.SetActive(value);
    }

    public void NegativeFeedback()
    {
        myAnimator.SetTrigger("TryWeb");
    }
        
}
