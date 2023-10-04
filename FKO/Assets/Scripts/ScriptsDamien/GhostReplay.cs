using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostReplay : MonoBehaviour
{
    [SerializeField] private TextAsset textFile;
    private List<Vector3> positions = new();
    private List<Quaternion> rotations = new();
    
    [SerializeField] private GameObject ghostGO;

    private float lerpSpeed;
    
    private CameraScript cameraScript;
    
    private GhostRecording ghostRecording;
    // Start is called before the first frame update
    void Start()
    {
        cameraScript = CameraScript.Instance;
        lerpSpeed = GhostRecording.Instance.timeBetweenPositionsInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadRecording()
    {
        Ghost ghost = JsonUtility.FromJson<Ghost>(textFile.text);
        positions = ghost.positions;
        rotations = ghost.rotations;
        
        ghostGO.transform.position = positions[0];
        ghostGO.transform.rotation = rotations[0];
    }
    
    public void StartReplay()
    {
        cameraScript.SetPlayer(ghostGO);
        StartCoroutine(Replay());
    }

    private IEnumerator Replay()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            ghostGO.transform.position = Vector3.Lerp(ghostGO.transform.position, positions[i], lerpSpeed);
            ghostGO.transform.rotation = Quaternion.Lerp(ghostGO.transform.rotation, rotations[i], lerpSpeed);
            yield return new WaitForSeconds(lerpSpeed);
        }
    }
}
