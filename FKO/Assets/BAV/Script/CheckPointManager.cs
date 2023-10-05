using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckPointManager : MonoBehaviour
{
    //Singleton Field
    public static CheckPointManager Instance;

    public Action OnCheckpointPassed;
    public Action OnCheckpointReset;
    public Action OnCheckpointSuccess;
    
    /**
     * CheckPoints Part
     */
    //Public
    [Header("Check Points")]
    public int checkPointValueToWin;
    public List<GameObject> checkPointLevel;
    [SerializeField] private int _checkPointSizeList;

    [Header("Setup Checkpoint Renderer")] 
    public Material checkpointMaterial;
    public List<Color> checkpointColor;
    public Color checkpointColorDefault;
    public Material m_groundMaterial;

    //Private S
    //[Header("Area for SpawnPoints")]
    //[SerializeField] private Vector3 cubeCenter; 
    //[SerializeField] private Vector3 cubeSize;
    
    
    
    /**
     * Debug part
     */
    //[Header("Debug")]
    //[Header("CheckPoints")]
    //[SerializeField] private bool enableGizmos;

    public int checkPointPassed;
    public int lastCheckPointPassed;
    

    private void Awake()
    {
        //Singleton Setup
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
       
        //SetElementAtRandomPosition();
    }

    private void Start()
    {
        if (checkPointLevel.Count == 0)
        {
            foreach (Transform child in transform)
            {
                checkPointLevel.Add(child.gameObject);
                child.gameObject.GetComponent<CheckPoints>().SetId(checkPointLevel.Count);
            }
        }
        checkPointValueToWin = checkPointLevel.Count;
        SetCheckPointsColor(); //Set the color for all the material
        GroundMaterialMaster.SetValueToGroundMat(m_groundMaterial, checkPointLevel, checkpointColor);
        //Setup Checkpoint
        ResetCheckPoints();
        _checkPointSizeList = checkPointLevel.Count;
        
        OnCheckpointPassed += LookIfEnded;
        OnCheckpointPassed += UpdateCheckPointColor;
    }

    void LookIfEnded()
    {
        if (_checkPointSizeList != checkPointPassed) return;
        
        if (checkPointValueToWin == checkPointPassed)
        {
            GameManager.instance.Win();
        }
    }
    
    void SetCheckPointsColor()
    {
        if (checkPointLevel.Count != 0)
        {
            for (int i = 0; i < checkPointLevel.Count; i++)
            {
                Material checkpointMaterial = new Material(this.checkpointMaterial);
                checkpointMaterial.SetColor("_CheckpointsColor", checkpointColorDefault);
                checkPointLevel[i].GetComponent<Renderer>().material = checkpointMaterial;
                
                
                //Set Automatic the color
                //Todo : Index not set go to one don't know
                string checkpointPosPropertyName = "_CheckpointPos_" + i;
                string colorPropertyName = "_ColorCheckpoints_" + i;
                Debug.Log(colorPropertyName);
                //string isCompletedPropertyName = "_isCompleted_" + i;
                m_groundMaterial.SetVector(colorPropertyName,checkpointColorDefault);
                //m_groundMaterial.SetFloat(isCompletedPropertyName, 0);
                m_groundMaterial.SetVector(checkpointPosPropertyName, checkPointLevel[i].transform.localPosition);
            }
        }
    }

    void UpdateCheckPointColor()
    {
        for (int i = 0; i < checkPointLevel.Count; i++)
        {
            CheckPoints cp = checkPointLevel[i].GetComponent<CheckPoints>();

            if (cp.isPassed)
            {
                cp.GetComponent<Renderer>().material.SetColor("_CheckpointsColor", checkpointColor[i]);
            }
            else
            {
                cp.GetComponent<Renderer>().material.SetColor("_CheckpointsColor", checkpointColorDefault);
            }
        }
    }

    //Reset Checkpoints State
    public void ResetCheckPoints()
    {
        if (checkPointLevel != null)
        {
            foreach (GameObject checkpoint in checkPointLevel)
            {
                //Debug.Log("Reset All the CheckPoints");
                CheckPoints cp = checkpoint.GetComponent<CheckPoints>();
                cp.isPassed = false;
            }
        }

        UpdateCheckPointColor();
        checkPointPassed = 0;
        
        OnCheckpointReset?.Invoke();
    }
    
    //Set CheckPoints at Random Position
    //void SetElementAtRandomPosition()
    //{
    //    // Loop through the list of elements to spawn
    //    foreach (GameObject checkPoint in checkPointLevel)
    //    {
    //        Vector3 randomPosition = new Vector3(
    //            Random.Range(cubeCenter.x - cubeSize.x / 2, cubeCenter.x + cubeSize.x / 2),
    //            Random.Range(cubeCenter.y - cubeSize.y / 2, cubeCenter.y + cubeSize.y / 2),
    //            Random.Range(cubeCenter.z - cubeSize.z / 2, cubeCenter.z + cubeSize.z / 2)
    //        );
    //        
    //        Quaternion randomRotation = Quaternion.Euler(
    //            Random.Range((float)0, 360),
    //            Random.Range(0, 360),
    //            Random.Range(0, 360)
    //        );
//
    //        checkPoint.gameObject.GetComponent<Transform>().position = randomPosition;
    //        checkPoint.gameObject.GetComponent<Transform>().rotation = randomRotation;
    //    }
    //}
//
    //private void OnDrawGizmos()
    //{
    //    if (enableGizmos)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawWireCube(cubeCenter, cubeSize);
    //    }
    //}
}
