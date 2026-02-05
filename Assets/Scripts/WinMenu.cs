using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level-1");
        Debug.Log("Loading: " + SceneManager.GetActiveScene().name);
    }
    
}
