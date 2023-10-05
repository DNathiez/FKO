using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    private List<Vector3> positions = new();
    private List<Quaternion> rotations = new();
    
    [SerializeField] private GameObject ghostGO;

    private float lerpSpeed;

    public static GhostPlayer instance;
    private GhostRecording ghostRecording;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        lerpSpeed = GhostRecording.Instance.timeBetweenPositionsInSeconds;
    }
    
    public void LoadRecording()
    {
        if (!File.Exists(Application.persistentDataPath + "/Ghosts/lastGhost" + ".json"))
        {
            recordLoaded = false;
            return;
        }
        
        Ghost ghost = JsonUtility.FromJson<Ghost>(File.ReadAllText(Application.persistentDataPath +"/Ghosts/lastGhost" + ".json"));
        positions = ghost.positions;
        rotations = ghost.rotations;
        
        ghostGO.transform.position = positions[0];
        ghostGO.transform.rotation = rotations[0];
        
        LineRenderer lineRenderer = ghostGO.AddComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
        
        Material redMaterial = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.red
        };
        
        lineRenderer.material = redMaterial;

        recordLoaded = true;
    }

    private bool recordLoaded;
    
    public void StartReplay()
    {
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
