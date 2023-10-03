using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public Transform guide;
    
    private void Update()
    {
        transform.position = guide.position;
        transform.LookAt(guide.position + guide.forward);
    }
    
}
