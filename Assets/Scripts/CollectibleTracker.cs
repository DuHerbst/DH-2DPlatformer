using UnityEngine;

public class CollectibleTracker : MonoBehaviour
{
    public int CollectedCount { get; private set; }
    
    private void OnEnable()
    {
        CollectibleController.OnCollected += HandleCollectibleCollected;
    }
    
    private void OnDisable()
    {
        CollectibleController.OnCollected -= HandleCollectibleCollected;
    }
    
    public void HandleCollectibleCollected()
    {
        CollectedCount++;
        Debug.Log("Total collectibles collected: " + CollectedCount);
    }
    
}
