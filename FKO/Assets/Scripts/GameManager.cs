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
        Respawn.Instance.SetRespawnPoint(originPoint.position, originPoint.rotation.eulerAngles);
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
        GhostRecording.Instance.StartRecording();
        timer.StartChrono();
    }
    
    public void Restart()
    {
        Respawn.Instance.SpawnPlayer();
        
        uiManager.awaitToStartTxt.gameObject.SetActive(true);
        timer.ResetChrono();
        inGame = true;
        
        uiManager.Restart();
    }
}
