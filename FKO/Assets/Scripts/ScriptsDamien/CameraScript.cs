using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Quaternion playerRotation;
    [Range(0.001f, 0.1f)] [SerializeField] private float smoothFactor = 0.5f;
    [SerializeField] private Vector3 cameraOffset;
    
    private Vector3 goalPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerRotation = player.transform.rotation;
        Quaternion cameraRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);
        var position = player.transform.position;
        goalPosition = position + cameraRotation * cameraOffset;
        transform.position = Vector3.Lerp(transform.position, goalPosition, smoothFactor);
        transform.LookAt(position);
    }
}
