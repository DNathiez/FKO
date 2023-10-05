using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GroundMaterialMaster
{
    public static void SetValueToGroundMat(Material mat,List<GameObject> checkPointList, List<Color> checkpointColorList)
    {
        Debug.Log("Hello");
        for (int i = 0; i < checkPointList.Count; i++)
        {
            string checkpointPosPropertyName = $"_CheckpointPos_{i}";
            string colorPropertyName = $"_ColorCheckpoints_{i}";
            string isCompletedPropertyName = $"_isCompleted_{i}";

            
            SetCheckpointValueToMaterial(
                mat,
                checkPointList[i].transform.position, 
                checkpointColorList[i], 
                0,  
                checkpointPosPropertyName,  
                colorPropertyName, 
                isCompletedPropertyName
                );
            Debug.Log(checkPointList[i].transform.position);
        }
    }
    

    public static void SetCheckpointValueToMaterial(Material mat, Vector3 position, Color color, float isCompleted, string posName, string colorName, string isCompletedCheck)
    {
        mat.SetVector(posName, position);
        mat.SetColor(colorName, color);
        mat.SetFloat(isCompletedCheck, isCompleted);
    }
}
