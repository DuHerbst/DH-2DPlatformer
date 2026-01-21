using UnityEngine;
using System;

public class CollectibleController : MonoBehaviour
{
    public static event Action OnCollected; // when the collectible is collected...

    private void OnTriggerEnter2D(Collider2D other) // when the player collides with the collectible
    {
        if (other != null && other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        OnCollected?.Invoke(); // The collectible has been collected
        Debug.Log("Collectible picked up!");
        Destroy(gameObject);
    }
    
}
