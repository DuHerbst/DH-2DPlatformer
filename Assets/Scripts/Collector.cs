using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<ICollectible>();

        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collected Coin");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("HealthPack"))
        {
            Debug.Log("Collected Health Pack");
            Destroy(other.gameObject);
        }
    }
}
