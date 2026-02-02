using UnityEngine;

public class ClimbableWalls : MonoBehaviour
{
    private PlayerController _playerController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController = other.GetComponent<PlayerController>();
            _playerController.canClimb = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_playerController != null)
        {
            _playerController.canClimb = false;
        }
    }
    
}
