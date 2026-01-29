using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private Vector2 footstepEffectOffset; // Offset for footstep sounds
    [SerializeField] private ParticleSystem footstepEffect;
    [SerializeField] private string targetTag = "Ground"; // Tag to identify ground surfaces


    void OnCollisionEnter2D(Collision2D collision) // use this function to detect collisions between 2 colliders - use compare tag
    {
     
        if (collision.gameObject.CompareTag(targetTag)) // Check if the collided object has the target tag
        
        {
            footstepEffect.Play(); // Play footstep particle effect
            Debug.Log("hush hush hush hush");
        }
        
    }
}
