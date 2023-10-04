using UnityEngine;

public class FlightController : MonoBehaviour
{
   [SerializeField] private float minSpeed = 5f;
   [SerializeField] private float speed;
   
   [SerializeField] private float acceleration = 1;
   [SerializeField] private float deceleration = 1;
   [SerializeField] private float hardDecelerationFactor = 2;
   private bool isAccelerating;
    
   [SerializeField] private float dampingSpeed = 3;
   [SerializeField] [Range(0, 5)] private float horizontalSensitivity = 1;

   [SerializeField] private float verticalAngleLimit = 40;
   
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
      if (Input.GetButtonDown("Fire1") && GameManager.instance.inGame && !GameManager.instance.isPlaying)
      {
         GameManager.instance.isPlaying = true;
         GameManager.instance.uiManager.HideHUD();
      }
      
      if(!GameManager.instance.isPlaying) return;
      
      CalculateCursorDelta();
      Move();
   }

   private void Move()
   {
      transform.position += transform.forward * (speed * Time.deltaTime);
      transform.eulerAngles += new Vector3(-cursorDelta.y, cursorDelta.x, 0);
      
      //TODO : Constrains the vertical angle
       
      Accelerate();
      if(Input.GetButtonDown("Fire2")) Decelerate();
   }
    

   private void Accelerate()
   {
      if (Input.GetButtonDown("Fire1"))
      {
         isAccelerating = true;
      }

      if (Input.GetButtonUp("Fire1"))
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
         if (cursorDelta.x < 0)
         {
            cursorDelta.x += Time.deltaTime * dampingSpeed;
         }
         else if (cursorDelta.x > 0)
         {
            cursorDelta.x -= Time.deltaTime * dampingSpeed;
         }
         
         if(cursorDelta.x < 0.1f && cursorDelta.x > -0.1f) cursorDelta.x = 0;
      }
      
      if (Input.GetAxis("Vertical") != 0)
      {
         cursorDelta.y += Input.GetAxis("Vertical") * dampingSpeed * Time.deltaTime;
         cursorDelta.y = Mathf.Clamp(cursorDelta.y, -1, 1);
      }
      else
      {
         if (cursorDelta.y < 0)
         {
            cursorDelta.y += Time.deltaTime * dampingSpeed;
         }
         else if (cursorDelta.y > 0)
         {
            cursorDelta.y -= Time.deltaTime * dampingSpeed;
         }
         
         if(cursorDelta.y < 0.1f && cursorDelta.y > -0.1f) cursorDelta.y = 0;

      }
   }
}
