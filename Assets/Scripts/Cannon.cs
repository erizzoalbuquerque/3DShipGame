using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject _shotPrefab;
    [SerializeField] Transform _shotStartPosition;
    [SerializeField] bool _addCannonVelocityToTheShot = false;
    [SerializeField] float _shotSpeed = 8f;
    [SerializeField] AnimationCurve _shotSpeedModifierCurve;
    [SerializeField] float _rechargeTime = 1f;
    [SerializeField] Transform _yawAxis = default;
    [SerializeField] float  _pitchSensitivity = 50f;
    [SerializeField] Transform _pitchAxis = default;

    static Plane _aimPlane = new Plane(Vector3.up, 0f);

    float _timeLastShot;
    Vector3 _aimPosition;
    float _cannonPitchNormalized;


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

        float shotSpeedModifier = _shotSpeedModifierCurve.Evaluate(_cannonPitchNormalized);

        Vector3 shotVelocity = _shotStartPosition.transform.forward * _shotSpeed * shotSpeedModifier;
        if (_addCannonVelocityToTheShot)
            shotVelocity += canoonBaseVelocity;

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

        float cannonPitch =  45f * (1 - Mathf.Exp(-deltaXZ.magnitude / _pitchSensitivity));
        SetPitch(cannonPitch);
    }

    void SetYaw(float angle)
    {
        _yawAxis.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    void SetPitch(float angle)
    {
        _pitchAxis.localRotation = Quaternion.Euler(new Vector3(-angle, 0f, 0f));
        _cannonPitchNormalized = angle / 45f;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.black;
    //     Gizmos.DrawSphere(_aimPosition, 0.1f);
    // }
}
