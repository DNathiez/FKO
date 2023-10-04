using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas pauseMenu;
    public Canvas hud;
    public Canvas deathMenu;
    
    public Button deathFirstSelectedButton;
    public TMP_Text awaitToStartTxt;
    
    public TMP_Text deathTimeText;
    
    public EventSystem EventSystem;

    public void HideHUD()
    {
        hud.gameObject.SetActive(false);
    }
    
    public void Play()
    {
        hud.gameObject.SetActive(true);
        awaitToStartTxt.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        hud.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }
    
    public void Resume()
    {
        hud.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(false);
    }
    
    public void Restart()
    {
        //Game restart fuction
        Resume();
        
    }

    public void DrawGameResult()
    {
        hud.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);

        deathTimeText.text = GameManager.instance.timer.GetTime();
        
        EventSystem.SetSelectedGameObject(deathFirstSelectedButton.gameObject);
    }

   
}
