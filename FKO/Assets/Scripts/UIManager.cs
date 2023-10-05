using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UI_CheckpointCounter checkpointCounter;
    
    public Canvas mainMenu;
    public Canvas hud;
    public TMP_Text awaitToStartTxt;

    public Canvas pauseMenu;
    public Button pauseFirstSelectedButton;
    
    public Canvas winMenu;
    public Button winMenuFirstSelectedButton;
    public TMP_Text currentTimerRun;
    
    public void HideHUD()
    {
        hud.gameObject.SetActive(false);
    }
    
    public void Play()
    {
        hud.gameObject.SetActive(true);
        awaitToStartTxt.gameObject.SetActive(true);
        
        mainMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (!GameManager.instance.isPlaying)
        {
            Resume();
            return;
        }
        GameManager.instance.SetInGame(false);
        GameManager.instance.isPlaying = false;
        Timer.Instance.StopChrono();
        
        EventSystem.current.SetSelectedGameObject(pauseFirstSelectedButton.gameObject);
        
        hud.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }
    public void Resume()
    {
        GameManager.instance.isPlaying = true;
        GameManager.instance.SetInGame(true);
        
        Timer.Instance.StartChrono();
        
        hud.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
    }

    public void GameOverUI()
    {
        hud.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(winMenuFirstSelectedButton.gameObject);
    }
    
    public void ResultGameUI()
    {
        currentTimerRun.text = GameManager.instance.timer.GetTime();
        winMenu.gameObject.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(winMenuFirstSelectedButton.gameObject);
    }
   
}
