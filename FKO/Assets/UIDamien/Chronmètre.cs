using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chronmètre : MonoBehaviour
{
    [SerializeField] TMP_Text chrono;
    private int minutes;
    private int seconds;
    private int milliseconds;
    
    public static Chronmètre Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        chrono.text = "00:00:00";
    }
    
    public void UpdateChrono(float time)
    {
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;
        int milliseconds = (int) (time * 1000) % 1000;
        chrono.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
    
    public void StartChrono()
    {
        StartCoroutine(Chrono());
    }

    private IEnumerator Chrono()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            UpdateChrono(time);
            yield return null;
        }
    }
    
    public void StopChrono()
    {
        StopAllCoroutines();
    }
}
