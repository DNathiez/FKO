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
        
        LineRenderer lineRenderer = ghostGO.AddComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
        
        TrailRenderer trailRenderer = ghostGO.AddComponent<TrailRenderer>();
        trailRenderer.SetPositions(positions.ToArray());
        
        MeshRenderer meshRenderer = ghostGO.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = ghostGO.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.vertices = positions.ToArray();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = Color.red;
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
