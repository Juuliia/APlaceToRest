using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    [SerializeField] private GameObject objectToAppear;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
                objectToAppear.SetActive(true);
    
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
         objectToAppear.SetActive(false);
        
    }
}
