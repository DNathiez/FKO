using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Button button => GetComponent<Button>();
    private TMP_Text tmpText => GetComponentInChildren<TMP_Text>();

    [SerializeField] private float bumpSpeed =5f;
    
    [SerializeField] private Color baseColor;
    [SerializeField] private float baseSize = 1f;
    
    [SerializeField] private Color selectedColor;
    [SerializeField] private float selectedSize = 1.2f;
    
    [SerializeField] private Image[] onSelectedSprites;
    [SerializeField] private bool spinMode = false;
    [SerializeField] private float turningArrowSpeed = 5f;

    public void OnSelect(BaseEventData eventData)
    {
        lookingScale = selectedSize;
        tmpText.color = selectedColor;
        foreach (var image in onSelectedSprites)
        {
           image.gameObject.SetActive(true);
        }

        if(spinMode) GameManager.instance.OnUpdate += TurningArrowEffect;
    }

    [SerializeField] private float lookingScale = 1f;
    private void Update()
    {
        Vector3 scale = transform.localScale;
        
        if (Math.Abs(scale.x - lookingScale) > 0.01f)
        {
            scale.x = Mathf.Lerp(scale.x, lookingScale, bumpSpeed * Time.deltaTime);
            scale.y = Mathf.Lerp(scale.y, lookingScale, bumpSpeed * Time.deltaTime);
            scale.z = Mathf.Lerp(scale.z, lookingScale, bumpSpeed * Time.deltaTime);
            
            transform.localScale = scale;
        }
    }

    private void TurningArrowEffect()
    {
        foreach (var img in onSelectedSprites)
        {
            Vector3 scale = img.transform.localScale;
            scale.y = Mathf.Sin(Time.time * turningArrowSpeed);
            img.transform.localScale = scale;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        lookingScale = baseSize;
        
        tmpText.color = baseColor;
        foreach (var image in onSelectedSprites)
        {
            image.gameObject.SetActive(false);
        }
        
        if(spinMode) GameManager.instance.OnUpdate -= TurningArrowEffect;
    }
}
