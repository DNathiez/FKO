using System.Threading.Tasks;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private GameObject player;
    private void Awake()
    {
       if(particleSystem) particleSystem.SetActive(false);
    }

    private async void Die()
    {
        if (particleSystem)
        {
            particleSystem.transform.position = player.transform.position;
            particleSystem.SetActive(true);
        }
        
        player.SetActive(false);
        
        GameManager.instance.uiManager.HideHUD();
        GameManager.instance.isPlaying = false;
        GameManager.instance.inGame = false;
        GameManager.instance.timer.StopChrono();
        GhostRecording.Instance.StopRecording();

        await Task.Delay((int)(particleSystem.GetComponent<ParticleSystem>().main.duration * 1000));
        
        if (particleSystem)
        {
            particleSystem.SetActive(false);
        }

        player.GetComponent<FlightController>().ResetSpeed();
        
        GameManager.instance.RespawnPlayer();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Die();
        }
    }
}
