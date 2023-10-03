using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class FlightController : MonoBehaviour
{
   [SerializeField] private float minSpeed = 5f;
   [SerializeField] private float speed;
   
   [SerializeField] private float acceleration = 1;
   [SerializeField] private float deceleration = 1;
   [SerializeField] private float hardDecelerationFactor = 2;
   private bool isAccelerating;
    
   [SerializeField] private float dampingSpeed = 3;

   [SerializeField] private float horizontalSensitivity;
   [SerializeField] private float verticalSensitivity;
   
   [SerializeField] private Vector2 cursorDelta;

   private void Awake()
   {
      Initialize();
   }

   private void Initialize()
   {
      speed = minSpeed;
   }

   private void Update()
   {
      CalculateCursorDelta();
      Move();
   }

   private void Move()
   {
      transform.position += transform.forward * (speed * Time.deltaTime);
      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-cursorDelta.y * verticalSensitivity, cursorDelta.x * horizontalSensitivity, 0));
      
      Accelerate();
      if(Input.GetKeyDown(KeyCode.LeftShift)) Decelerate();
   }

   private void Accelerate()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         isAccelerating = true;
      }

      if (Input.GetKeyUp(KeyCode.Space))
      {
         isAccelerating = false;
      }

      if (isAccelerating)
      {
         speed += acceleration * Time.deltaTime;
      }
      else
      {
         if (speed > minSpeed)
         {
            speed -= deceleration * Time.deltaTime;
         }
      }
   }

   private void Decelerate()
   {
      if (speed > minSpeed)
      {
         speed -= (deceleration * hardDecelerationFactor) * Time.deltaTime;
      }   
   }

   private void CalculateCursorDelta()
   {
      if (Input.GetAxis("Horizontal") != 0)
      {
         cursorDelta.x += Input.GetAxis("Horizontal") * horizontalSensitivity * Time.deltaTime;
         cursorDelta.x = Mathf.Clamp(cursorDelta.x, -1, 1);
      }
      else
      {
         cursorDelta.x = Mathf.Lerp(cursorDelta.x, 0,  Time.deltaTime * dampingSpeed);
      }
      
      if (Input.GetAxis("Vertical") != 0)
      {
         cursorDelta.y += Input.GetAxis("Vertical") * verticalSensitivity * Time.deltaTime;
         cursorDelta.y = Mathf.Clamp(cursorDelta.y, -1, 1);
      }
      else
      {
         cursorDelta.y = Mathf.Lerp(cursorDelta.y, 0, Time.deltaTime * dampingSpeed);
      }
   }
}
