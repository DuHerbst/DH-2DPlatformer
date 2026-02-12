using UnityEngine;

public class CoinScripts : MonoBehaviour, ICollectible // call the interface ICollectible script name after a comma - this means that this item can be collected
{
    // AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectCoinSound;
   
    public void OnCollect()
    {

        if (CompareTag("Coin"))
        {
            CollectibleTracker.OnCollected?.Invoke(gameObject.tag); // The collectible has been collected
        }
        
        Destroy(gameObject); // Remove the collectible from the scene
        
        audioSource.PlayOneShot(collectCoinSound); // 
        
    }
}
