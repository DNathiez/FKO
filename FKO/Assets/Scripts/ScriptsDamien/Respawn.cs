using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private Vector3 rotationRespawnPoint;
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
    }
}
