using UnityEngine;
using System;

public class CollectibleController : MonoBehaviour
{
    // AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectCoinSound;

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
            CollectCoin(); // Call the function to collect the coin
        }

    }
    
    private void CollectCoin()
    {
        
        
        CompareTag("Coin"); // Check if the collectible is a coin
        audioSource.PlayOneShot(collectCoinSound); // Play sound effect for collecting a coin
        OnCollected?.Invoke(); // The collectible has been collected
        Debug.Log("Collectible (Coin) picked up!"); // Log message for collecting a coin
        Destroy(gameObject); // Remove the collectible from the scene
        
        
    }
    
    private void CollectPowerUp()
    {
        // Implement power-up collection logic here
        Debug.Log("Collectible (Power-Up) picked up!");
        OnCollected?.Invoke();
        Destroy(gameObject);
    }
    
}