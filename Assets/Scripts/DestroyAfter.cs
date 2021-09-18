using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] private float _timer = 3f;

    void Update()
    {
        Destroy(this.gameObject, _timer);
    }
}
