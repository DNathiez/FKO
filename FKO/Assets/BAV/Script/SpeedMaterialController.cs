using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMaterialController : MonoBehaviour
{
    public Material matSpeed;
    public bool enabledRenderer;
    public bool debugValue;
    
    [Header("BlurControls")] 
    public float blurPower = 0.5f;
    public float blurPowerEffect = 0.5f;
    
    [Header("Blur Oscillator")]
    public float minValue = 0.35f;
    public float maxValue = 0.75f;
    public float oscillationSpeed = 1f;
    
    [Header("LinesControls")] 
    public float maskSizeLines = 0.1f;
    public float centerMaskEdge = 0.65f;
    public float minCenterMaskEdge = 1.0f;
    public float maxCenterMaskEdge = 0.40f;
    public Vector2 speedLineCenter = new Vector2(0.5f, 0.5f);

    public float lineCount  = 20f;
    public float minLineCount = 20f;
    public float maxLineCount = 60f;
    public float frameCount = 24f; 
    
    //Private float value
    private float currentLineCount      = 0f;
    private float currentBlurPower      = 0f;
    public float currentMaskSizeLines  = 0f;
    private float currentCenterMaskEdge = 0f;

    private float currentTime           = 0f;
    private float timeSinValue          = 0f;
    public float currentSpeedPlayer    = 0f;

    public void Start()
    {
        currentBlurPower = blurPowerEffect;
        currentMaskSizeLines = maskSizeLines;
        currentCenterMaskEdge = centerMaskEdge;
        currentLineCount = lineCount;
    }

    public void SetValue()
    {
        if (debugValue)
        {
            matSpeed.SetFloat("_BlurPower", blurPower);
            matSpeed.SetFloat("_BlurPowerEffect", blurPowerEffect);
            matSpeed.SetFloat("_MaskSize_Lines", maskSizeLines);
            matSpeed.SetFloat("_CenterMaskEdge", centerMaskEdge);
            matSpeed.SetVector("_SpeedLineCenter", speedLineCenter);
            matSpeed.SetFloat("_LineCount", lineCount);
            matSpeed.SetFloat("_FramesAnim", frameCount);
        }
        else
        {
            matSpeed.SetFloat("_BlurPower", currentBlurPower);
            matSpeed.SetFloat("_BlurPowerEffect", blurPowerEffect);
            matSpeed.SetFloat("_MaskSize_Lines", currentMaskSizeLines);
            matSpeed.SetFloat("_CenterMaskEdge", currentCenterMaskEdge);
            matSpeed.SetVector("_SpeedLineCenter", speedLineCenter);
            matSpeed.SetFloat("_LineCount", currentLineCount);
            matSpeed.SetFloat("_FramesAnim", frameCount);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        IncrementTimeValue();
        SetValue();
        //SineAnimMenuScene();
        ChangeValuesAccordingToSpeed();
    }

    private void IncrementTimeValue()
    {
        // Incrémente le temps en fonction de la vitesse de l'oscillation
        currentTime += Time.deltaTime * oscillationSpeed;
        timeSinValue = Mathf.Sin(currentTime);
    }
    public void SineAnimMenuScene()
    {
        currentBlurPower = Mathf.Lerp(minValue, maxValue, (timeSinValue + 1f) / 2f);
        currentMaskSizeLines = Mathf.Lerp(minValue, maxValue, (timeSinValue + 1f) / 2f);
    }

    public void ChangeValuesAccordingToSpeed()
    {
        currentSpeedPlayer = FlightController.Instance.GetSpeed();
        if (currentSpeedPlayer <= 66f)
        {
            currentCenterMaskEdge = minCenterMaskEdge;
            currentLineCount = minLineCount;
        }
        else if(currentSpeedPlayer >= 3300f)
        {
            currentCenterMaskEdge = maxCenterMaskEdge;
            currentLineCount = maxLineCount;
        }
        else
        {
            currentCenterMaskEdge = minCenterMaskEdge -  (minCenterMaskEdge - maxCenterMaskEdge) * ((currentSpeedPlayer - 20.0f) / 80.0f);
            currentLineCount = minLineCount -  (minLineCount - maxLineCount) * ((currentSpeedPlayer - 20.0f) / 80.0f);
        }
    }
    

    public void AverageChangeSpeed(float speed, float minValueSpeed, float maxValueSpeed, float valueToChange, float minValueChange, float maxValueChange)
    {
        if (speed <= minValueSpeed)
        {
            valueToChange = maxValueChange;
        }
        else if(currentSpeedPlayer >= maxValueSpeed)
        {
            valueToChange =  minValueChange;
        }
        else
        {
            valueToChange = maxValueChange - (maxValueChange -  minValueChange) * ((speed - 20.0f) / 80.0f);
        }
    }
}
