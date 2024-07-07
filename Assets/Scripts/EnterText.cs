using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterText : MonoBehaviour
{
    [SerializeField] private GameObject textToAppear1;
    [SerializeField] private GameObject textToAppear2;
    public float displayTime = 3.0f;

 void Start()
    {
        StartCoroutine(DisplayAndHideText());
    }

    IEnumerator DisplayAndHideText()
    {
        textToAppear1.gameObject.SetActive(true);
       
        yield return new WaitForSeconds(displayTime);
        
        textToAppear1.gameObject.SetActive(false);

//

//

        textToAppear2.gameObject.SetActive(true);
       
        yield return new WaitForSeconds(displayTime);
       
        textToAppear2.gameObject.SetActive(false);

    }
}
