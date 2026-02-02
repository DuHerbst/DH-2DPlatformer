using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    
    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame() // Pauses the game by setting time scale to 0
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("PauseMenu");
        Debug.Log("The game is paused and " + Time.timeScale);
    }

    public void ResumeGame() // Resumes the game by setting time scale back to 1
    {
        Time.timeScale = 1f;
        
    }
    
    public void RestartGame() // Restarts the current level
    {
        SceneManager.LoadScene("Level-1");
    }

    public void QuitGame() // quits game to main menu
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    
}
