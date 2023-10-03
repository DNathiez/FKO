using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostReplay : MonoBehaviour
{
    [SerializeField] private TextAsset textFile;
    private List<Vector3> positions = new();
    private List<Quaternion> rotations = new();
    
    [SerializeField] private GameObject ghost;

    private float lerpSpeed;
    
    private GhostRecording ghostRecording;
    // Start is called before the first frame update
    void Start()
    {
        lerpSpeed = GhostRecording.Instance.timeBetweenPositionsInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadRecording()
    {
        string[] lines = textFile.text.Split('\n');
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            positions.Add(new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2])));
            rotations.Add(new Quaternion(float.Parse(values[3]), float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6])));
        }
        
        ghost.transform.position = positions[0];
        ghost.transform.rotation = rotations[0];
    }
    
    public void StartReplay()
    {
        StartCoroutine(Replay());
    }

    private IEnumerator Replay()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            transform.position = Vector3.Lerp(transform.position, positions[i], lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotations[i], lerpSpeed);
            yield return new WaitForSeconds(lerpSpeed);
        }
    }
}
