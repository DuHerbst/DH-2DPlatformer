using UnityEngine;

public class CoinScripts : MonoBehaviour, ICollectible
{
    public void OnCollect()
    {
        Debug.Log("Coin Picked up"); // Log message for collecting a coin
        Destroy(gameObject); // Remove the collectible from the scene
    }
}
