using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas pauseMenu;
    public Canvas hud;

    public void HideHUD()
    {
        hud.gameObject.SetActive(false);
    }
    
    public void Play()
    {
        GameManager.instance.inGame = true;
        hud.gameObject.SetActive(true);
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
    }
    
    public void Restart()
    {
        //Game restart fuction
        Resume();
    }

   
}
