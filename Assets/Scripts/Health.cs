using UnityEngine;

public class Health : MonoBehaviour, ICollectible
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectHealthSound;
    
    public void OnCollect()
    {
        audioSource.PlayOneShot(collectHealthSound); // Play sound effect for collecting a coin
        Debug.Log("Health Pack Picked up"); // Log message for collecting a health pack
        Destroy(gameObject); // Remove the collectible from the scene
    }
    
}
