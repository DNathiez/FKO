using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas pauseMenu;
    public Canvas hud;
    
    public void Play()
    {
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
