using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecording : MonoBehaviour
{
    private List<Vector3> positions = new();
    private List<Quaternion> rotations = new();
    [SerializeField] private GameObject player;
    public float timeBetweenPositionsInSeconds = 0.1f;
    [SerializeField] private string ghostName = "DefaultName";
    [SerializeField] private string ghostSavePath = "Assets/ScriptsDamien/Ghosts/";
    Timer _timer;
    Respawn respawn;
    CameraScript cameraScript;
    public static GhostRecording Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _timer = Timer.Instance;
        respawn = Respawn.Instance;
        cameraScript = CameraScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void StartRecording()
    {
        StartCoroutine(Record());
        _timer.StartChrono();
        respawn.SpawnPlayer();
        cameraScript.SetPlayer(player);
    }

    private IEnumerator Record()
    {
        while (true)
        {
            positions.Add(player.transform.position);
            rotations.Add(player.transform.rotation);
            yield return new WaitForSeconds(timeBetweenPositionsInSeconds);
        }
    }
    
    public void StopRecording()
    {
        StopAllCoroutines();
        _timer.StopChrono();
        SaveRecording();
    }

    private void SaveRecording()
    {
        string path = ghostSavePath + ghostName + ".json";
        Ghost ghost = new Ghost(ghostName, positions, rotations);
        string json = JsonUtility.ToJson(ghost);
        // json = json.Replace("},{", "},\n{");
        // json = json.Replace(":[{", ":[\n{");
        // json = json.Replace("}]", "}\n]");
        System.IO.File.WriteAllText(path, json);
    }
}

internal class Ghost
{
    public string ghostName;
    public List<Vector3> positions;
    public List<Quaternion> rotations;
    public Ghost(string ghostName, List<Vector3> positions, List<Quaternion> rotations)
    {
        this.ghostName = ghostName;
        this.positions = positions;
        this.rotations = rotations;
    }
}
