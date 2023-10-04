using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMaterialController : MonoBehaviour
{
    public Material matSpeed;
    public bool enabledRenderer;
    [Header("BlurControls")] 
    public float blurPower = 0.5f;
    public float blurPowerEffect = 0.5f;
    private float currentBlurPower;
    
    [Header("Blur Oscillator")]
    public float minValue = 0.35f;
    public float maxValue = 0.75f;
    public float oscillationSpeed = 1f;
    
    [Header("LinesControls")] 
    public float maskSizeLines = 0.1f;
    private float currentMaskSizeLines;
    public float centerMaskEdge = 0.65f;
    private float currentCenterMaskEdge;
    public Vector2 speedLineCenter = new Vector2(0.5f, 0.5f);

    public float lineCount = 20f;
    private float currentLineCount;
    public float frameCount = 24f; 

    public void Start()
    {
        currentBlurPower = blurPowerEffect;
        currentMaskSizeLines = maskSizeLines;
        currentCenterMaskEdge = centerMaskEdge;
        currentLineCount = lineCount;
        Debug.Log(currentBlurPower);
    }

    public void SetValue()
    {
        matSpeed.SetFloat("_BlurPower", blurPower);
        matSpeed.SetFloat("_BlurPower", currentBlurPower);
        matSpeed.SetFloat("_BlurPowerEffect", blurPowerEffect);
        matSpeed.SetFloat("_MaskSize_Lines", currentMaskSizeLines);
        matSpeed.SetFloat("_CenterMaskEdge", currentCenterMaskEdge);
        matSpeed.SetVector("_SpeedLineCenter", speedLineCenter);
        matSpeed.SetFloat("_LineCount", currentLineCount);
        matSpeed.SetFloat("_FramesAnim", frameCount);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        IncrementTimeValue();
        SetValue();
    }
    
    private float currentTime = 0f;
    private float timeSinValue = 0f;

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
}
