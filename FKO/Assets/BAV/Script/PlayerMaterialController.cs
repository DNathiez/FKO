using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialController : MonoBehaviour
{
    public static PlayerMaterialController Instance;
    
    public Material playerMat;
    
    public AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1); 
    public float transitionSpeed = 1.0f; 
    public bool reverseAnimation = false; 
    private float targetValue = 0.0f; 
    private float currentValue;
    private float timeFactor;
    
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
        targetValue = 0.0f; 
        currentValue = 0f;
    }

    public void ChangeSpawnValue(bool reverseAnimation)
    {

        Debug.Log(timeFactor);
        targetValue = reverseAnimation ? -1.5f : 0.0f;
        currentValue = Mathf.MoveTowards(currentValue, targetValue,
            animationCurve.Evaluate(currentValue / 1.5f) * timeFactor);
        playerMat.SetFloat("_Position", currentValue);
    }

    public void Update()
    {
        timeFactor = Time.deltaTime * transitionSpeed;
        ChangeSpawnValue(true);
        Debug.Log(currentValue);
    }
}
