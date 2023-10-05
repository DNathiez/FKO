using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.SetActive(false);
    }

    private void Die()
    {
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.transform.position = player.transform.position;
        particleSystem.SetActive(true);
        player.SetActive(false);
        
        GameManager.instance.timer.StopChrono();

        player.GetComponent<FlightController>().ResetSpeed();
        
        GhostRecording.Instance.StopRecording();
        
        GameManager.instance.inGame = false;
        GameManager.instance.isPlaying = false;
        GameManager.instance.uiManager.GameOverUI();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Die();
        }
    }
}
