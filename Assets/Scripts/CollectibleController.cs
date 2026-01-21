using UnityEngine;
using System;

public class CollectibleController : MonoBehaviour
{

    // COLLECTIBLE TYPES
    public enum CollectibleType
    {
        Coin,
        PowerUp,
        HealthPack,
    }

    [SerializeField] private CollectibleType collectibleType = CollectibleType.Coin; // default type is Coin
    public static event Action OnCollected; // triggered when the collectible is collected


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (other.CompareTag("Player"))
        {
            CollectCoin();
            // CollectibleTracker.HandleCollectibleCollected(collectibleType);
        }

    }
    
    private void CollectCoin()
    {
        if (CompareTag("Coin"))
        {
            OnCollected?.Invoke(); // The collectible has been collected
            Debug.Log("Collectible (Coin) picked up!"); // Log message for collecting a coin
            Destroy(gameObject); // Remove the collectible from the scene
        }
    }
    
}
