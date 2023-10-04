using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckPointManager : MonoBehaviour
{
    //Singleton Field
    private static CheckPointManager _instanceCheckPointManager;
    public static CheckPointManager Instance => _instanceCheckPointManager;

    /**
     * CheckPoints Part
     */
    //Public
    [Header("Check Points")]
    public int checkPointValueToWin;
    public List<GameObject> checkPointLevel;
    
    //Private S
    [Header("Area for SpawnPoints")]
    [SerializeField] private Vector3 cubeCenter; 
    [SerializeField] private Vector3 cubeSize;
    
    /**
     * Debug part
     */
    [Header("Debug")]
    [Header("CheckPoints")]
    [SerializeField] private bool enableGizmos;
    [SerializeField] private int _checkPointSizeList;

    public int checkPointPassed;
    public int lastCheckPointPassed;
    

    private void Awake()
    {
        //Singleton Setup
        if (_instanceCheckPointManager != null && _instanceCheckPointManager != this)
        {
            Destroy(this.gameObject);
        } else {
            _instanceCheckPointManager = this;
        }
        
        //Setup Checkpoint
        ResetCheckPoints();
        _checkPointSizeList = checkPointLevel.Count;
        //SetElementAtRandomPosition();
    }

    private void Update()
    {
        //Check the Number of Point Passed
        if (_checkPointSizeList != checkPointPassed) return;
        if (checkPointValueToWin == checkPointPassed)
        {
            Debug.Log("All points passed");
        }
    }

    //Reset Checkpoints State
    void ResetCheckPoints()
    {
        if (checkPointLevel != null)
        {
            foreach (GameObject checkpoint in checkPointLevel)
            {
                Debug.Log("Reset All the CheckPoints");
                checkpoint.GetComponent<CheckPoints>().isPassed = false;
            }
        }
    }
    
    //Set CheckPoints at Random Position
    void SetElementAtRandomPosition()
    {
        // Loop through the list of elements to spawn
        foreach (GameObject checkPoint in checkPointLevel)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(cubeCenter.x - cubeSize.x / 2, cubeCenter.x + cubeSize.x / 2),
                Random.Range(cubeCenter.y - cubeSize.y / 2, cubeCenter.y + cubeSize.y / 2),
                Random.Range(cubeCenter.z - cubeSize.z / 2, cubeCenter.z + cubeSize.z / 2)
            );
            
            Quaternion randomRotation = Quaternion.Euler(
                Random.Range((float)0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
            );

            checkPoint.gameObject.GetComponent<Transform>().position = randomPosition;
            checkPoint.gameObject.GetComponent<Transform>().rotation = randomRotation;
        }
    }

    private void OnDrawGizmos()
    {
        if (enableGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cubeCenter, cubeSize);
        }
    }
}
