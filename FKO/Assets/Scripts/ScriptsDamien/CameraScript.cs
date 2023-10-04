using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Quaternion playerRotation;
    [Range(0.001f, 0.1f)] [SerializeField] private float smoothFactor = 0.5f;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraLookAtOffset;
    
    private Vector3 goalPosition;
    
    public static CameraScript Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerRotation = player.transform.rotation;
        Quaternion cameraRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);
        var position = player.transform.position;
        goalPosition = position + cameraRotation * cameraOffset;
        transform.position = Vector3.Lerp(transform.position, goalPosition, smoothFactor);
        
        //system for a lookat that looks at the player but with a bit of lag
        if (cameraLookAtOffset > 0)
        {
            Vector3 lookAtPosition = position + cameraRotation * Vector3.forward * cameraLookAtOffset;
            transform.LookAt(lookAtPosition);
        }
        else
        {
            transform.LookAt(position);
        }
    }
    
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
