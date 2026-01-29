using UnityEngine;

public class Health : MonoBehaviour, ICollectible
{
    
    public void OnCollect()
    {
        Debug.Log("Health Pack Picked up"); // Log message for collecting a health pack
        Destroy(gameObject); // Remove the collectible from the scene
    }
    
}
