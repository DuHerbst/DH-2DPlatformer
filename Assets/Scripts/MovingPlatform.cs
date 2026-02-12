using UnityEngine;

//Lerp needs 2 values and time

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float direction = 1f; // every frame we will increase time by this amount
    [SerializeField] private float cycleTime = 5f;
    
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // getting the sprite renderer component from this specifc game object
    }
    
    
    // Update is called once per frame
    void Update()
    {
       currentTime += direction * Time.deltaTime; // increase time every frame

       Vector3 currentPosition = transform.position; // the current position of this game object

       // if the distance of the platform is close to point b, change the direction
       if (Vector3.Distance(currentPosition, pointB.position) < 0.1f)
       {
           direction = -1f;
       }
       // if the distance of the platform is close to point a, change the direction
       else if (Vector3.Distance(currentPosition, pointA.position) < 0.1f)
       {
           direction = 1f;
       }
       
       currentTime = Mathf.Clamp(currentTime, 0, cycleTime);
       
       float t = currentTime / cycleTime;
       transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
       _spriteRenderer.color = Color.Lerp(Color.green, Color.darkGreen, t);
       
    }
    
}
