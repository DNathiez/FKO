using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool isPlaying;
    public bool inGame;
    
    public UIManager uiManager;
    
    private void Awake()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        instance = this;
    }
    
    
}
