using UnityEngine;

public class FlightController : MonoBehaviour
{
   [SerializeField] private float minSpeed = 5f;
   [SerializeField] private float speed;
   [SerializeField] private float acceleration = 1;
   [SerializeField] private float deceleration = 1;
   [SerializeField] private float dampingSpeed = 3;

   [SerializeField] private float horizontalSensitivity;
   [SerializeField] private float verticalSensitivity;
   [SerializeField] private Vector2 cursorDelta;

   private void Awake()
   {
      speed = minSpeed;
   }

   private void Update()
   {
      CalculateCursorDelta();
      Move();
      Accelerate();
   }

   private void Move()
   {
      transform.position += transform.forward * (speed * Time.deltaTime);
      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-cursorDelta.y * verticalSensitivity, cursorDelta.x * horizontalSensitivity, 0));
   }

   private bool accelerating;
   private void Accelerate()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         accelerating = true;
      }

      if (Input.GetKeyUp(KeyCode.Space))
      {
         accelerating = false;
      }

      if (accelerating)
      {
         speed += acceleration * Time.deltaTime;
      }
      else
      {
         speed = Mathf.Lerp(speed, minSpeed, Time.deltaTime * deceleration);
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
