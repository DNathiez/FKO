using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[ExecuteAlways]
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]
    float radius;
 
    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
        Shader.SetGlobalFloat("_Radius", radius);
    }
}