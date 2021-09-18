using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //later look at gun is equiped and change damage based on that.

    [SerializeField] private GameObject _bloodSpatterPreFab;
    [SerializeField] private LayerMask _enemy;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 centre = new Vector3(0.5f, 0.5f, 0f);
        Ray rayOrigin = Camera.main.ViewportPointToRay(centre);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin ,out hit,Mathf.Infinity, _enemy.value))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                //instantate blood splater
                //at the position ray cast hit
                // rotate towards the hit normal position (serface normal)

              //  Instantiate(_bloodSpatterPreFab, hit.point,Quaternion.Euler( hit.normal.x, hit.normal.y, hit.normal.z));
                Instantiate(_bloodSpatterPreFab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));

                target.Damage(10);
            }
            Debug.Log( "Omg, you killed " + hit.transform.name);
        }
    }
}
