using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    //Public
    public LayerMask elementToTchek;
    public bool isPassed;
    
    
    //Private S
    [SerializeField] private GameObject _PSWhenCompleted;
    [SerializeField] private int _ID;
    
    //Private
    private SphereCollider _sphereCollider;
    
    // Start is called before the first frame update
    void Awake()
    {
        CheckCapsuleColliderAttached();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((elementToTchek.value & (1 << other.gameObject.layer)) != 0 && !isPassed)
        {
            isPassed = true;
            IncrementValueToReachVictory();
            EnableParticulesFeedback();
            Debug.Log(gameObject.name + " is passed and complete");
        }
    }


    void CheckCapsuleColliderAttached()
    {
        if (!_sphereCollider)
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }
        Debug.Log("Capsule Collider is ready for Checkpoints : " + gameObject.name + 
                  "ID :" + _sphereCollider.GetInstanceID());
    }

    void IncrementValueToReachVictory()
    {
        CheckPointManager.Instance.checkPointPassed++;
        CheckPointManager.Instance.lastCheckPointPassed = _ID;
        //Debug.Log(gameObject.name + "has incremented Value");
    }

    void EnableParticulesFeedback()
    {
        if (!_PSWhenCompleted)
        {
            Debug.Log("Particule System Not Set");
            return;
        }
        _PSWhenCompleted.SetActive(true);
    }
}
