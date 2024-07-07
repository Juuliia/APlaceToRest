using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
     Animator myAnimator;
     [SerializeField] private GameObject objectToAppear;
     [Header("SoundEffect")]
    [SerializeField] AudioClip soundEffectClip;
    [SerializeField] [Range(0f, 1f)] float soundgVolume = 1f;
     private bool hasPlayedAudio = false;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        myAnimator.SetTrigger("isOpen");
        if(soundEffectClip != null && !hasPlayedAudio)
    {
        AudioSource.PlayClipAtPoint(soundEffectClip, 
        Camera.main.transform.position, soundgVolume);
        hasPlayedAudio = true;
    }
        objectToAppear.SetActive(true);
        StartCoroutine(HideObject());
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(5f);
        objectToAppear.SetActive(false);
    }
}

