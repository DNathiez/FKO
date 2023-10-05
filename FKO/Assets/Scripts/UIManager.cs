using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UI_CheckpointCounter checkpointCounter;
    
    public Canvas mainMenu;
    public Canvas pauseMenu;
    public Canvas hud;
    public Canvas deathMenu;
    
    public Button pauseFirstSelectedButton;
    public Button deathFirstSelectedButton;
    public TMP_Text awaitToStartTxt;
    
    public TMP_Text deathTimeText;
    
    public void HideHUD()
    {
        hud.gameObject.SetActive(false);
    }
    
    public void Play()
    {
        hud.gameObject.SetActive(true);
        awaitToStartTxt.gameObject.SetActive(true);
        
        mainMenu.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(false);
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
        deathMenu.gameObject.SetActive(false);
    }

    public void GameOverUI()
    {
        hud.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);

        deathTimeText.text = GameManager.instance.timer.GetTime();
        
        EventSystem.current.SetSelectedGameObject(deathFirstSelectedButton.gameObject);
    }
    
    public void ResultGameUI()
    {
        
    }
   
}
