using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadTime = 2f;
    [SerializeField] float exitSlow = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
            StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = exitSlow;
        yield return new WaitForSecondsRealtime(loadTime);
        Time.timeScale = 1f;
        Destroy(FindObjectOfType<ScenePersist>().gameObject);
        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel + 1);
    }
}
