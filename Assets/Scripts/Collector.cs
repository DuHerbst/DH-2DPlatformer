using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        ICollectible otherCollectible = other.GetComponent<ICollectible>();

        if (otherCollectible != null)
        {
            otherCollectible.OnCollect();
        }
    }
}
