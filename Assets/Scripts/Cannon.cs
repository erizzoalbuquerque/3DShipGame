using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject _shotPrefab;
    [SerializeField] Transform _shotStartPosition;
    [SerializeField] bool _addCannonVelocityToTheShot = false;
    [SerializeField] float _shotSpeed = 8f;
    [SerializeField] float _rechargeTime = 1f;
    [SerializeField] Transform _yawAxis = default;

    static Plane _aimPlane = new Plane(Vector3.up, 0f);

    float _timeLastShot;
    Vector3 _aimPosition;


    // Start is called before the first frame update
    void Start()
    {
        _timeLastShot = 0;
        _aimPosition = this.transform.TransformPoint(Vector3.forward * 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector3 canoonBaseVelocity)
    {
        if (Time.time - _timeLastShot < _rechargeTime)
            return;

        //Shot
        GameObject shot = Instantiate(_shotPrefab, _shotStartPosition.transform.position, Quaternion.identity);

        Vector3 shotVelocity = _shotStartPosition.transform.forward * _shotSpeed;
        if (_addCannonVelocityToTheShot)
            shotVelocity += canoonBaseVelocity * Vector3.Dot(canoonBaseVelocity.normalized, _shotStartPosition.transform.forward);

        shot.GetComponent<Shot>().Setup(shotVelocity);
        _timeLastShot = Time.time;
    }

    public void Aim(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        float enter = 0.0f;
        if (_aimPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            _aimPosition = hitPoint;
        }

        //Get delta between aim and ship projected on the XZ plane
        Vector3 deltaXZ = _aimPosition - _aimPlane.ClosestPointOnPlane(this.transform.position);

        Vector3 forward = this.transform.forward;

        float xzAngle = Vector3.SignedAngle(forward, deltaXZ, Vector3.up);
        SetYaw(xzAngle);
    }

    void SetYaw(float angle)
    {
        _yawAxis.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.black;
    //     Gizmos.DrawSphere(_aimPosition, 0.1f);
    // }
}
