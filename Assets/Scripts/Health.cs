using UnityEngine;

public class Health : MonoBehaviour, ICollectible
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectHealthSound;
    
    public void OnCollect()
    {
        
        if (CompareTag("HealthPack"))
        {
            CollectibleTracker.OnCollected?.Invoke(gameObject.tag); // The collectible has been collected
        }
        
        Destroy(gameObject); // Remove the collectible from the scene
        audioSource.PlayOneShot(collectHealthSound); // Play sound effect for collecting a coin
        
    }
    
}
