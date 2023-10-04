using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        if (!particleSystem)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        particleSystem.transform.position = player.transform.position;
        particleSystem.SetActive(true);
        player.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Die();
        }
    }
}
