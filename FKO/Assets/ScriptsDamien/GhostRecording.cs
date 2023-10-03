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
        string path = ghostSavePath + ghostName + ".txt";
        string[] lines = new string[positions.Count];
        for (int i = 0; i < positions.Count; i++)
        {
            lines[i] = $"{positions[i].x},{positions[i].y},{positions[i].z},{rotations[i].x},{rotations[i].y},{rotations[i].z},{rotations[i].w}";
        }
        System.IO.File.WriteAllLines(path, lines);
        Debug.Log("Saved recording to " + path);
    }
}
