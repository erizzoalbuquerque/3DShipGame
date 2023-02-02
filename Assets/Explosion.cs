using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _radius = 5.0f;
    [SerializeField] float _forceAtCenter = 10.0f;
    [SerializeField] float _upwardsModifier = 3f;
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

        List<Rigidbody> hitBodies = new List<Rigidbody>();
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.attachedRigidbody;

            if (rb != null)
            {
                print(rb.gameObject.name);
                if (!hitBodies.Contains(rb))
                    hitBodies.Add(rb);
            }
        }

        foreach (Rigidbody hitBody in hitBodies)
        {
            hitBody.AddExplosionForce(_forceAtCenter, explosionPos, _radius, _upwardsModifier);
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
