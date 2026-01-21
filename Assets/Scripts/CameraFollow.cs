using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector2 offset; // Offset from the player (x,y)
    [SerializeField] private float dampening = 0.15f; // How fast the camera is catching up to the player (smooth time)
    public Transform player; // The player transform to follow
    
    private Vector3 _velocity = Vector3.zero; // Velocity reference variable for SmoothDamp

    private void LateUpdate()
    {
        if (player == null) return;

        
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); // Create a vector 3 for the player position with offset
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, dampening); // Smoothly move the camera towards the target position
        
        /// Other options for camera follow to consider:
        /// Straight snap to player position
        /// Showing full map to complete
        /// Smoother follow like hollow knight (i think this one is similar to smooth damp)
    }
}
