using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private Vector3 rotationRespawnPoint;
    [SerializeField] private Material playerMat;
    [SerializeField] private float incrementValue = 1;
    public static Respawn Instance;
    
    private CameraScript cameraScript;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        if (!cameraScript)
        {
            cameraScript = CameraScript.Instance;
        }
    }
    
    public void SetRespawnPoint(Vector3 newRespawnPoint, Vector3 newRotationRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        rotationRespawnPoint = newRotationRespawnPoint;
    }
    
    public void SpawnPlayer()
    {
        player.transform.position = respawnPoint;
        player.SetActive(true);
        player.transform.rotation = Quaternion.Euler(rotationRespawnPoint);
        cameraScript.SetCameraPos(player.transform.position);
        player.GetComponent<FlightController>().ResetSpeed();

        value = -1.5f;
        isAppearing = true;
    }

    private void Update()
    {
        if (isAppearing)
        {
            AppearSpaceship();
        }
    }

    private bool isAppearing;
    private float value;
    private void AppearSpaceship()
    {
        value = Mathf.Lerp(value, 0, Time.deltaTime * incrementValue);
        playerMat.SetFloat("_Position", value);
        
        if (value >= 0)
        {
            isAppearing = false;
        }
    }
}
