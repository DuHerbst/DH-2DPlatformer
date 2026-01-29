using UnityEngine;

public class CoinScripts : MonoBehaviour, ICollectible // call the interface ICollectible script name after a comma - this means that this item can be collected
{
    // AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectCoinSound;
   
    public void OnCollect()
    {
        audioSource.PlayOneShot(collectCoinSound); // Play sound effect for collecting a coin
        Debug.Log("Coin Picked up"); // Log message for collecting a coin
        Destroy(gameObject); // Remove the collectible from the scene
    }
}
