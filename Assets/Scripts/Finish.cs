using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject textToAppear1;
    [SerializeField] private GameObject textToAppear2;
    [SerializeField] private GameObject Exit;
    public float displayTime = 3.0f;

    PlayerMovement playerMovement;

 void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        StartCoroutine(DisplayAndHideText());
        //playerMovement.isRest = true;
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
        playerMovement.isRest = true;

        Exit.gameObject.SetActive(true);
        
    }


}
