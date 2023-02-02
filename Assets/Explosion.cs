using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _radius = 5.0F;
    [SerializeField] float _forceAtCenter = 10.0F;
    [SerializeField] float _lifeTime = 5f;

    float _startTime;

    private void Awake()
    {
        _startTime = Time.time;
    }

    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(_forceAtCenter, explosionPos, _radius, 3.0F);
        }
    }

    private void Update()
    {
        if (Time.time - _startTime > _lifeTime)
            Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}
