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
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.SetActive(false);
    }
    
    public void SetRespawnPoint(Vector3 newRespawnPoint, Vector3 newRotationRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        rotationRespawnPoint = newRotationRespawnPoint;
    }
    
    public void SpawnPlayer()
    {
        if (respawnPoint == null)
        {
            Debug.Log("Respawn Point Not Set");
            return;
        }
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        if (!player)
        {
            Debug.Log("Player Not Set");
            return;
        }
        particleSystem.transform.position = player.transform.position;
        particleSystem.SetActive(false);
        particleSystem.SetActive(true);
        player.transform.position = respawnPoint;
        player.SetActive(true);
        player.transform.rotation = Quaternion.Euler(rotationRespawnPoint);
    }
}
