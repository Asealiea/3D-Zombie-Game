using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{

    private EnemyAI _ai;

    private void Start()
    {
        _ai = GetComponentInParent<EnemyAI>();
        if (_ai == null) Debug.LogError(transform.name + ":Parent: EnemyAi is null");
       
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ai.Attack(other.transform);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ai.PlayerRunning();
        }
    }
}
