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
    Chronmètre chronomètre;
    public static GhostRecording Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        chronomètre = Chronmètre.Instance;
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void StartRecording()
    {
        StartCoroutine(Record());
        chronomètre.StartChrono();
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
        chronomètre.StopChrono();
        SaveRecording();
    }

    private void SaveRecording()
    {
        string path = ghostSavePath + ghostName + ".json";
        Ghost ghost = new Ghost(ghostName, positions, rotations);
        string json = JsonUtility.ToJson(ghost);
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
