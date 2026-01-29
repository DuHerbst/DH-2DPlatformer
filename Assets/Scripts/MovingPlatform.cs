using UnityEngine;

//Lerp needs 2 values and time

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float speed = 1f; // every frame we will increase time by this amount
    [SerializeField] private float cycleTime = 5f;
    
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    // Update is called once per frame
    void Update()
    {
       currentTime += speed * Time.deltaTime; // increase time every frame

       if (currentTime > cycleTime) speed = -1f; // reverse direction when we reach the end
       if (currentTime < cycleTime) speed = 1f; // reverse

       float t = currentTime / cycleTime;
       transform.position = Vector3.Lerp(pointA.position, pointB.position, t);

    }
    
}
