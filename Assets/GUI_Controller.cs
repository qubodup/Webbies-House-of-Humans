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
    public SpiderGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        HouseFill.fillAmount = 0;
        //SetEnergyValue(1);
        WebColor.gameObject.SetActive(false);
        WebPosition.localPosition = new Vector3(WebPosition.localPosition.x, -755 + ( (755 + 150) * barTreshold), 0);
    }

    private void Update()
    {
        if (HouseFill.fillAmount>0.99f)
        {

            gameManager.GameWin();
        }
    }

    public void SetCompletionValue(float value)
    {
        HouseFill.fillAmount = value;
    }
    public void IncreaseCompletionValue(float value)
    {
        HouseFill.fillAmount += value;
    }

    // 0 to 1
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
