using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GhostRecording : MonoBehaviour
{
    private List<Vector3> positions = new();
    private List<Quaternion> rotations = new();
    [SerializeField] private GameObject player;
    public float timeBetweenPositionsInSeconds = 0.1f;
    [SerializeField] private string ghostName = "DefaultName";
    
    public static GhostRecording Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void StartRecording()
    {
        StartCoroutine(Record());
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
        SaveRecording();
    }

    private void SaveRecording()
    {
        string path = Application.persistentDataPath +"/Ghosts/lastGhost" + ".json";

        if (File.Exists(Application.persistentDataPath + "/Ghosts/lastGhost" + ".json"))
        {
            File.Delete(path);
        }
        
        Ghost ghost = new Ghost(ghostName, positions, rotations);
        string json = JsonUtility.ToJson(ghost);
        
        // json = json.Replace("},{", "},\n{");
        // json = json.Replace(":[{", ":[\n{");
        // json = json.Replace("}]", "}\n]");
        
        if (!Directory.Exists(Application.persistentDataPath +"/Ghosts"))
        {
            Directory.CreateDirectory(Application.persistentDataPath +"/Ghosts");
        }
        File.WriteAllText(path, json);
        
        Debug.Log("Ghost data has been saved.");
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
