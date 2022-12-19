using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CenterOfMassConfigurator : MonoBehaviour
{
    [SerializeField] Vector3 _centerOfMassLocalPosition = Vector3.zero;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.centerOfMass = _centerOfMassLocalPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.transform.TransformPoint(_centerOfMassLocalPosition), 0.1f);
    }
}
