using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _radius = 5.0f;
    [SerializeField] float _forceAtCenter = 10.0f;
    [SerializeField] float _upwardsModifier = 3f;
    [SerializeField] float _lifeTime = 5f;

    [SerializeField] float _startSize = 0f;
    [SerializeField] float _endingSize = 5f;
    [SerializeField] AnimationCurve _radiusOvertime;

    float _startTime;
    float _currentTime;
    Material _material;

    private void Awake()
    {
        _startTime = Time.time;
        _material = this.GetComponent<MeshRenderer>().material;
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

        _currentTime = 0;
        UpdateVisuals();
    }

    private void Update()
    {
        _currentTime = Time.time - _startTime;
        
        if (Time.time - _startTime > _lifeTime)
            Destroy(this.gameObject);

        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        float timeSinceStaredScaled = _currentTime / _lifeTime;

        transform.localScale = Mathf.Lerp(_startSize,_endingSize, _radiusOvertime.Evaluate(timeSinceStaredScaled))*Vector3.one*2f;
        _material.SetFloat("_Dissolve", timeSinceStaredScaled);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}
