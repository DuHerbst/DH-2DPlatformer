using UnityEngine;
using System;

public class CollectibleController : MonoBehaviour
{
    public static event Action OnCollected;
    

    // 2D physics trigger
    private void OnTriggerEnter2D(Collider2D other)
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
