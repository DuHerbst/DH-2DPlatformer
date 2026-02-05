using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // refers to the parent object of the trigger
            Debug.Log("Player entered the trigger area.");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); // the parent of the player is now null so it can get off the platform trigger
            Debug.Log("Player exited the trigger area.");
        }
    }
    
}
