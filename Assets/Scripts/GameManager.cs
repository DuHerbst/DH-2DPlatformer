using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // GAME STATES

    public GameObject pauseMenu;
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isLevelComplete = false;
    
    // LEVEL TIMER
    [SerializeField] private float levelTime = 100f; 
    [SerializeField] private Image timerBarFill;
    private float _remainingTime; // try to add a _ to private variables
    
    public static GameManager Instance { get; private set; } // this is the singleton <-

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
        
        _remainingTime = levelTime;
        UpdateTimerUI();
        
    }

    private void Update()
    {
        if (isPaused || isGameOver || isLevelComplete)
        {
            return;  // The timer doesnt update during these
        }
        
        _remainingTime -= Time.deltaTime; // decrease the remaining time
        _remainingTime = Mathf.Clamp(_remainingTime, 0, levelTime);
        UpdateTimerUI();
        
        if (_remainingTime <= 0)
        {
            RestartGame();
        }
        
    }


    public void PauseGame() // Pauses the game by setting timescale to 0
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        
    }
    
    public void TogglePause() // Toggles the pause state of the game
    {
        
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame() // Resumes the game by setting timescale back to 1
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        
    }
    
    public void RestartGame() // Restarts the current level
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void QuitGame()
    { 
        Time.timeScale = 1f;
        SceneLoader.Load(SceneID.Main);
    }
    
    private void UpdateTimerUI()
    {
        if (timerBarFill != null)
        {
            timerBarFill.fillAmount = _remainingTime / levelTime; // Update the UI fill amount based on remaining time
            timerBarFill.color = Color.Lerp(Color.red, Color.green, _remainingTime / levelTime); // Change color from green to red based on time left
        }
    }
    
}
