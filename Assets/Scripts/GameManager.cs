using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GAME STATES

    public GameObject pauseMenu;
    public bool isPaused = false;
    public bool IsPaused => isPaused;
    public bool isGameOver = false;
    public bool isLevelComplete = false;

    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // private void Update()
    // {
    //         if (isPaused)
    //         {
    //             ResumeGame();
    //         }
    //
    //         else
    //         {
    //             PauseGame();
    //         }
    //     
    // }


    public void PauseGame() // Pauses the game by setting time scale to 0
    {
        isPaused = true;
        Time.timeScale = 0f;
        Debug.Log("PauseGame called. pauseMenu is null? " + (pauseMenu == null));

        pauseMenu.SetActive(true);
        
    }
    
    public void TogglePause() // Toggles the pause state of the game
    {
        Debug.Log("TogglePause in Game Manager called. isPaused: " + isPaused);
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame() // Resumes the game by setting time scale back to 1
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        Debug.Log("The game is resumed and " + Time.timeScale);
        
    }
    
    public void RestartGame() // Restarts the current level
    {
        Time.timeScale = 1f; // Resets the time before loading the game again
        Debug.Log("Restarting the game...");
        SceneManager.LoadScene("Level-1");
    }

    public void QuitGame() // quits game to main menu
    {
        Debug.Log("Quit Game button has been pressed");
       // SceneManager.LoadScene("MainMenu"); // need to create the main menu scene
    }
    
}
