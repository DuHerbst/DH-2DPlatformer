using UnityEngine;

public class PauseUI : MonoBehaviour
{

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        
    }
    
    
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
    
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    
    
}
