using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool isPlaying;
    public bool inGame;
    
    public UIManager uiManager;
    public Timer timer;

    [SerializeField] private Transform originPoint;
    
    private void Awake()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3") && !isPlaying)
        {
           Play();
        }
    }

    public void Play()
    {
        isPlaying = true;
        uiManager.awaitToStartTxt.gameObject.SetActive(false);
        timer.StartChrono();
    }
    
    public void Restart(GameObject obj)
    {
        obj.transform.position = originPoint.position;
        uiManager.awaitToStartTxt.gameObject.SetActive(true);
        timer.ResetChrono();
        obj.SetActive(true);
        inGame = true;
        uiManager.Restart();
    }
}
