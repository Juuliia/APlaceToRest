using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    GameSession gameSession;

    void Start()
    {
        gameSession =  FindObjectOfType<GameSession>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
        StartCoroutine(LoadNextLevel());
        }
        
    } IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Application.Quit();
    }

}
