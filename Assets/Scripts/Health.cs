using UnityEngine;

public class Health : MonoBehaviour, ICollectible
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectHealthSound;
    
    public void OnCollect()
    {
        
        //audioSource.PlayOneShot(collectHealthSound); // Play sound effect for collecting a coin
        Debug.Log("Health Pack Picked up"); // Log message for collecting a health pack
        //PlayerController.Instance.Heal(1); // Heal the player by 1
        
        
        if (CompareTag("HealthPack"))
        {
            CollectibleTracker.OnCollected?.Invoke(gameObject.tag); // The collectible has been collected
        }
        
        Destroy(gameObject); // Remove the collectible from the scene
        
    }
    
}
