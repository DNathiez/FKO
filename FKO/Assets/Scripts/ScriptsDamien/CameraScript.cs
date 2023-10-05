using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Quaternion playerRotation;
    [SerializeField] private AnimationCurve smoothFactor;
    [Range(0.001f,0.03f)][SerializeField] private float smoothFactorValue;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraLookAtOffset;
    [SerializeField] [Range(0.002f,0.5f)] private float followSmoothFactor;
    
    [SerializeField] private Transform cameraStartPos;

    [SerializeField] private float AnglesAccentuation;
    
    private float speed;
    
    private bool hasStarted = false;
    
    private Vector3 goalPosition;
    private FlightController flightController;
    public static CameraScript Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        if (flightController == null)
        {
            flightController = FlightController.Instance;
        }
        transform.position = cameraStartPos.position - Vector3.forward * 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        speed = flightController.GetSpeed();
        speed = Mathf.Clamp(speed, 10, 50);
        speed = (speed - 10) / 40;
        
        //Debug.Log(speed);
        smoothFactorValue = smoothFactor.Evaluate(speed);
        //Debug.Log("speed = " + speed + " smoothFactorValue = " + smoothFactorValue);
        playerRotation = player.transform.rotation;
        Quaternion cameraRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);
        var position = player.transform.position;
        goalPosition = position + cameraRotation * cameraOffset * AnglesAccentuation;
        if (hasStarted)
        {
            transform.position = Vector3.Lerp(transform.position, goalPosition, smoothFactorValue * Time.deltaTime / followSmoothFactor);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, cameraStartPos.position - Vector3.forward * 10, smoothFactorValue * Time.deltaTime / followSmoothFactor);
        }
        
        if (cameraLookAtOffset > 0)
        {
            Vector3 lookAtPosition = position + cameraRotation * Vector3.forward * cameraLookAtOffset;
            transform.LookAt(hasStarted ? lookAtPosition : cameraStartPos.position);
        }
        else
        {
            transform.LookAt(hasStarted ? position : cameraStartPos.position);
        }
    }
    
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    public void SetCameraPos(Vector3 position)
    {
        transform.position = position;
    }
    
    public void SetStartCamera(bool value)
    {
        hasStarted = value;
    }
}