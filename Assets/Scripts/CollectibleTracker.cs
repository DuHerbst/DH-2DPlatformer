using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectibleTracker : MonoBehaviour
{
    [SerializeField] private int goalCollectibleCount = 30;
    private bool _hasReachedGoal = false;
    
    
    [SerializeField] private TextMeshProUGUI collectibleCountText;
    
    public static Action<string> OnCollected; // when a collectible is collected, it will show the collectible's name

    private void Awake()
    {
        UpdateCoinUI();
    }

    private int CollectedCount { get; set; }
    
    private void OnEnable() 
    {
        OnCollected += HandleCollectibleCollected;
    }
    
    private void OnDisable()
    {
        OnCollected -= HandleCollectibleCollected;
    }
    
    public void HandleCollectibleCollected(string collectibleName)
    {
        if (collectibleName != "Coin")
        {
            return;
        }
        
        CollectedCount++;
        UpdateCoinUI();
        
        //if the player has reached the goal collectible count change hasReachedGoal to true and change scene to level complete
        
        if (!_hasReachedGoal && CollectedCount >= goalCollectibleCount)
        {
            _hasReachedGoal = true;
            Time.timeScale = 1f;
            Debug.Log("20 coins collected, changing scene");
            SceneManager.LoadScene("WinScene");
        }
        
    }

    private void UpdateCoinUI()
    {
        if (collectibleCountText == null)
        {
            return;
        }
        
        collectibleCountText.text = "Coins: " + CollectedCount;
        
    }
    
}
