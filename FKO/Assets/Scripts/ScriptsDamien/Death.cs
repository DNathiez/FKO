using System.Threading.Tasks;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private GameObject player;
    
    private CameraScript cameraScript;
    private void Awake()
    {
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.SetActive(false);
    }
    
    private void Start()
    {
        if (!cameraScript)
        {
            cameraScript = CameraScript.Instance;
        }
    }

    private async void Die()
    {
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.transform.position = player.transform.position;
        particleSystem.SetActive(true);
        player.SetActive(false);
        
        cameraScript.SetStartCamera(false);
        
        GameManager.instance.uiManager.HideHUD();
        GameManager.instance.isPlaying = false;
        GameManager.instance.inGame = false;
        GameManager.instance.timer.StopChrono();
        GhostRecording.Instance.StopRecording();

        await Task.Delay((int)(particleSystem.GetComponent<ParticleSystem>().main.duration * 1000));
        particleSystem.SetActive(false);

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
