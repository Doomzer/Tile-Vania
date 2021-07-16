using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    int score = 0;

    private void Awake()
    {
        int sessionNum = FindObjectsOfType<GameSession>().Length;
        if(sessionNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
