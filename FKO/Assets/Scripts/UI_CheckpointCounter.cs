using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CheckpointCounter : MonoBehaviour
{
    [SerializeField] private GameObject checkpointSpritePrefab;
    private List<GameObject> checkpointSpriteList;

    public Color passedColor;
    public Color baseColor;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        checkpointSpriteList = new List<GameObject>();
        
        foreach (var cp in CheckPointManager.Instance.checkPointLevel)
        {
            checkpointSpriteList.Add(Instantiate(checkpointSpritePrefab, transform));
        }

        CheckPointManager.Instance.OnCheckpointReset += UpdateCheckpointSprite;
        CheckPointManager.Instance.OnCheckpointPassed += UpdateCheckpointSprite;
        
        UpdateCheckpointSprite();
    }
    
    public void UpdateCheckpointSprite()
    {
        for (int i = 0; i < CheckPointManager.Instance.checkPointLevel.Count; i++)
        {
            Image cpImage = checkpointSpriteList[i].GetComponent<Image>();
            
            if (CheckPointManager.Instance.checkPointLevel[i].GetComponent<CheckPoints>().isPassed)
            {
                cpImage.color = CheckPointManager.Instance.checkpointColor[i];
            }
            else
            {
                cpImage.color = baseColor;
            }
        }
    }
}
