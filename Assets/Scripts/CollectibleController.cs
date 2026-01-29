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
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
    
        if (other.CompareTag("Player")) // other is used to check what collided with the collectible
        {
            CollectCoin(); // Call the function to collect the coin
        }
        
        ICollectible otherCollectible = other.GetComponent<ICollectible>(); // Get the ICollectible component from the other object
    
        if (otherCollectible != null) // are you a collectible?
        {
            otherCollectible.OnCollect(); // if its a collectible, call its OnCollect method
        }
        
    
    }
    
    private void CollectCoin()
    {
        
        
        CompareTag("Coin"); // Check if the collectible is a coin
        audioSource.PlayOneShot(collectCoinSound); // Play sound effect for collecting a coin
        OnCollected?.Invoke(); // The collectible has been collected
        Debug.Log("Coin Picked up"); // Log message for collecting a coin
        Destroy(gameObject); // Remove the collectible from the scene
        
        
    }
    
    private void CollectPowerUp()
    {
        // Implement power-up collection logic here
        Debug.Log("Collectible (Power-Up) picked up!");
        OnCollected?.Invoke();
        Destroy(gameObject);
    }
    
    private void CollectHealthPack()
    {
        CompareTag("HealthPack"); // Check if the collectible is a health pack
        
        // if ()
        //
        // Debug.Log("HealthUp!");
        // OnCollected?.Invoke();
        // Destroy(gameObject);
    }
    

}