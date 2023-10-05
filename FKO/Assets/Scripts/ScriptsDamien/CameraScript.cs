using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Quaternion playerRotation;
    [SerializeField] private AnimationCurve smoothFactor;
    [Range(0.001f,0.03f)][SerializeField] private float smoothFactorValue;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraLookAtOffset;

    [SerializeField] private float AnglesAccentuation;
    [SerializeField] private AnimationCurve xMultiplier;
    [SerializeField] private float xMultiplierValue;
    private float playerRotationXForEvaluation;
    
    private float speed;
    private Vector3 lastLookatAtPosition;

    
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
        lastLookatAtPosition = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        speed = flightController.GetSpeed();
        speed = Mathf.Clamp(speed, 66, 3300);
        speed = (speed - 66) / 3300;
        
        smoothFactorValue = smoothFactor.Evaluate(speed);
        //Debug.Log("speed = " + speed + " smoothFactorValue = " + smoothFactorValue);
        playerRotation = player.transform.rotation;
        Quaternion cameraRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);
        var position = player.transform.position;
        goalPosition = position + cameraRotation * cameraOffset * AnglesAccentuation;
        
        cameraLookAtOffset = playerRotation.eulerAngles.x is > 30 and < 330 ? 1 : 1;
        Vector3 lookAtPosition = position + cameraRotation * Vector3.forward * cameraLookAtOffset;
        if (cameraLookAtOffset > 0)
        {
            // transform.position = Vector3.Lerp(transform.position, goalPosition, smoothFactorValue);
            playerRotationXForEvaluation = playerRotation.eulerAngles.x > 180 ? playerRotation.eulerAngles.x - 360 : playerRotation.eulerAngles.x;
            Debug.Log("playerRotationXForEvaluation = " + playerRotationXForEvaluation);
            xMultiplierValue = xMultiplier.Evaluate(playerRotationXForEvaluation);
            smoothFactorValue *= xMultiplierValue;
            transform.position = Vector3.Lerp(transform.position, goalPosition, smoothFactorValue * Time.deltaTime);
            
        }
        else
        {
            playerRotationXForEvaluation = playerRotation.eulerAngles.x > 180 ? playerRotation.eulerAngles.x - 360 : playerRotation.eulerAngles.x;
            Debug.Log("playerRotationXForEvaluation = " + playerRotationXForEvaluation);
            xMultiplierValue = xMultiplier.Evaluate(playerRotationXForEvaluation);
            smoothFactorValue *= xMultiplierValue;
            transform.position = Vector3.Lerp(transform.position, goalPosition, smoothFactorValue);
        }
        transform.LookAt(lookAtPosition);

    }
    
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    public void SetCameraPos(Vector3 position)
    {
        transform.position = position;
    }
}