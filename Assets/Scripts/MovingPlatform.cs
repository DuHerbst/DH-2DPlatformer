using UnityEngine;

//Lerp needs 2 values and time

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _currentTime = 0f;
    [SerializeField] private float _speed = 1f; // every frame we will increase time by this amount
    [SerializeField] private float cycleTime = 5f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    

    // Update is called once per frame
    void Update()
    {
       _currentTime += _speed * Time.deltaTime;

       if (_currentTime >= cycleTime) _speed = -1f; // reverse direction when we reach the end
       if (_currentTime <= cycleTime) _speed = 1f; // reverse
       
       float time = _currentTime / cycleTime;
       transform.position = Vector2.Lerp(pointA.position, pointB.position, time);

    }
    
}
