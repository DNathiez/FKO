using UnityEngine;

public class FlightController : MonoBehaviour
{
   [SerializeField] private float minSpeed = 5f;
   [SerializeField] private float maxSpeed = 50f;
   [SerializeField] private float speed;
   
   [SerializeField] private float acceleration = 1;
   [SerializeField] private float deceleration = 1;
   [SerializeField] private float hardDecelerationFactor = 2;
   private bool isAccelerating;
    
   [SerializeField] private float dampingSpeed = 3;
   [SerializeField] [Range(0, 5)] private float horizontalSensitivity = 1;

   [SerializeField] private float verticalAngleLimit = 40;
   [SerializeField] private float horizontalAngleLimit = 60;
   
   [SerializeField] private Vector2 cursorDelta;
   
   [SerializeField] private AnimationCurve horizontalAxisSensitivity;
   [SerializeField] private AnimationCurve verticalAxisSensitivity;


   public static FlightController Instance { get; private set; }

   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
      }
      else
      {
         Instance = this;
      }
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
      transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward * speed, Time.deltaTime);

      transform.eulerAngles += new Vector3(-cursorDelta.y * Time.deltaTime/0.002f, cursorDelta.x * Time.deltaTime/0.002f, 0);
      
      //TODO : Constrains the vertical angle
      transform.eulerAngles = transform.eulerAngles.x switch
      {
         > 86 and < 180 => new Vector3(86, transform.eulerAngles.y, transform.eulerAngles.z),
         < 274 and > 180 => new Vector3(274, transform.eulerAngles.y, transform.eulerAngles.z),
         _ => transform.eulerAngles
      };
       
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
         if (speed < maxSpeed)
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
      if (Input.GetAxis("Vertical") == 0)
      {
         transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z), Time.deltaTime * dampingSpeed);
      }
      if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
      {
         transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.deltaTime * dampingSpeed);
      }
      if (Input.GetAxis("Horizontal") != 0)
      {
         if (Input.GetAxis("Horizontal") > 0 && cursorDelta.x < 0)
         {
            cursorDelta.x = 0;
         }
         else if (Input.GetAxis("Horizontal") < 0 && cursorDelta.x > 0)
         {
            cursorDelta.x = 0;
         }
         cursorDelta.x += horizontalAxisSensitivity.Evaluate(Input.GetAxis("Horizontal")) * Time.deltaTime;
         cursorDelta.x = Mathf.Clamp(cursorDelta.x, -1, 1);
         transform.Rotate(0, 0, -cursorDelta.x * horizontalSensitivity * Time.deltaTime/.002f);
         //clamp the rotation to the horizontal angle limit
         if (transform.eulerAngles.z > 180)
         {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z, 360 - horizontalAngleLimit, 360));
         }
         else
         {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z, 0, horizontalAngleLimit));
         }
      }
      else
      {
         switch (cursorDelta.x)
         {
            case < 0:
               cursorDelta.x += Time.deltaTime * dampingSpeed;
               break;
            case > 0:
               cursorDelta.x -= Time.deltaTime * dampingSpeed;
               break;
         }

         if(cursorDelta.x is < 0.1f and > -0.1f) cursorDelta.x = 0;
      }
      
      if (Input.GetAxis("Vertical") != 0)
      {
         if (Input.GetAxis("Vertical") > 0 && cursorDelta.y < 0)
         {
            cursorDelta.y = 0.1f;
         }
         else if (Input.GetAxis("Vertical") < 0 && cursorDelta.y > 0)
         {
            cursorDelta.y = -0.1f;
         }
         cursorDelta.y += verticalAxisSensitivity.Evaluate(Input.GetAxis("Vertical")) * Time.deltaTime;
         cursorDelta.y = Mathf.Clamp(cursorDelta.y, -1, 1);
      }
      else
      {
         switch (cursorDelta.y)
         {
            case < 0:
               cursorDelta.y += Time.deltaTime * dampingSpeed;
               break;
            case > 0:
               cursorDelta.y -= Time.deltaTime * dampingSpeed;
               break;
         }

         if(cursorDelta.y is < 0.1f and > -0.1f) cursorDelta.y = 0;
      }
   }

   public void ResetSpeed()
   {
      speed = minSpeed;
   }
   
   public float GetSpeed()
   {
      return speed;
   }
   
   public Vector3 GetSpeedVector()
   {
      return transform.forward * speed;
   }

   public Vector2 GetCursorDelta()
   {
      return cursorDelta;
   }
}
