using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Action OnUpdate;
    
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

    public void SetInGame(bool state) => inGame = state;
    private void Update()
    {
        FlightController._playerController.Base.Start.started += context =>
        {
            if (!isPlaying && inGame)
            {
                Play();
            }
        };
        
        FlightController._playerController.Base.Pause.started += ctx => uiManager.Pause();
        
        OnUpdate?.Invoke();
    }

    private void Play()
    {
        isPlaying = true;
        uiManager.awaitToStartTxt.gameObject.SetActive(false);
        GhostRecording.Instance.StartRecording();
        timer.StartChrono();
    }

    public void RespawnPlayer()
    {
        Respawn.Instance.SpawnPlayer();
        
        uiManager.awaitToStartTxt.gameObject.SetActive(true);

        inGame = true;
        uiManager.Play();
    }
    
    public void Restart()
    {
        Respawn.Instance.SetRespawnPoint(originPoint.position, originPoint.rotation.eulerAngles);
        Respawn.Instance.SpawnPlayer();
        
        CheckPointManager.Instance.ResetCheckPoints();
        timer.ResetChrono();
        
        inGame = true;
        uiManager.Play();
    }

    public void Win()
    {
        isPlaying = false;
        inGame = false;
        uiManager.HideHUD();
        timer.StopChrono();

        uiManager.ResultGameUI();
    }
}
