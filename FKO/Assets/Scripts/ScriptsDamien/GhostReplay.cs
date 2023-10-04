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

    void Start()
    {
        cameraScript = CameraScript.Instance;
        lerpSpeed = GhostRecording.Instance.timeBetweenPositionsInSeconds;
    }
    
    
    public void LoadRecording()
    {
        Ghost ghost = JsonUtility.FromJson<Ghost>(textFile.text);
        positions = ghost.positions;
        rotations = ghost.rotations;
        
        ghostGO.transform.position = positions[0];
        ghostGO.transform.rotation = rotations[0];
        
        LineRenderer lineRenderer = ghostGO.AddComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
        Material redMaterial = new Material(Shader.Find("Unlit/Color"));
        redMaterial.color = Color.red;
        lineRenderer.material = redMaterial;
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
            ghostGO.transform.position = positions[i];
            ghostGO.transform.rotation = rotations[i];
            yield return new WaitForSeconds(lerpSpeed);
        }
    }
}
