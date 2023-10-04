using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Quaternion playerRotation;
    [SerializeField] private AnimationCurve smoothFactor;
    [Range(0.001f,0.0250f)][SerializeField] private float smoothFactorValue;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private AnimationCurve cameralookAtOffset;
    [SerializeField] private float cameraLookAtOffsetValue;

    private float speed;
    private Vector3 speedVector;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        speed = flightController.GetSpeed();
        speed = Mathf.Clamp(speed, 0, 50);
        speed /= 50;
        smoothFactorValue = smoothFactor.Evaluate(speed);
        //Debug.Log("speed = " + speed + " smoothFactorValue = " + smoothFactorValue);
        playerRotation = player.transform.rotation;
        
        Quaternion cameraRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);
        var position = player.transform.position;
        cameraOffset.y = Mathf.Lerp(0.5f, 3, speed);
        cameraOffset.z = Mathf.Lerp(-1, -4, speed);
        speedVector = flightController.GetSpeedVector();
        transform.position = Vector3.Lerp(transform.position, position + cameraRotation * (speedVector * smoothFactorValue) + Vector3.up * cameraOffset.y + Vector3.back * cameraOffset.z, Time.deltaTime);
        transform.LookAt(position + cameraRotation * (speedVector * smoothFactorValue) + Vector3.up * cameraOffset.y);
        
        cameraLookAtOffsetValue = cameralookAtOffset.Evaluate(speed);
    }
    
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
