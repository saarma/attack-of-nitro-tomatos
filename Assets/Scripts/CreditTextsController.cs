using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditTextsController : MonoBehaviour
{
    public bool isCreditsScrolling = false;

    public Text creditTexts;
    public Image creditTextsBackround;
    public RectTransform rectTransform;
    public float moveSpeedAmount = 10f; // speed of the credits

    // Start is called before the first frame update
    void Start()
    {
        // set hide creditTextsBackround
        creditTextsBackround.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCreditsScrolling)
        {
            rectTransform.anchoredPosition += new Vector2(0, moveSpeedAmount * Time.deltaTime);
        }

    }

    public void StartCreditTexts()
    {
        creditTextsBackround.enabled = true;
        isCreditsScrolling = true;
    }
}
