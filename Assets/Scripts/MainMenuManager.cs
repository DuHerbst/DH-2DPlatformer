using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame() // When the Start button is pressed changes scene to the game scene
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level-1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player has quit the game ");
    }
    
}
