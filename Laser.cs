using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float laserDistance=10f;
    [SerializeField] LayerMask ignoreMask;

    public event EventHandler OnHitTarget;

    public RaycastHit raycastHit;
    public Ray ray;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        ray=new(transform.position,transform.forward);

        if(Physics.Raycast(ray, out raycastHit,laserDistance,~ignoreMask))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, raycastHit.point);

            if(raycastHit.collider.TryGetComponent(out PlayerController player))
            {
                player.HitByRay();
                OnHitTarget?.Invoke(this,EventArgs.Empty);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position+transform.forward*laserDistance);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, ray.direction * laserDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(raycastHit.point, 0.23f);
    }
}
