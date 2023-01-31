using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject _shotPrefab;
    [SerializeField] Transform _shotStartPosition;
    [SerializeField] float _shotSpeed = 8f;
    [SerializeField] float _rechargeTime = 1f;

    float _timeLastShot;

    // Start is called before the first frame update
    void Start()
    {
        _timeLastShot = 0;
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
        shot.GetComponent<Shot>().Setup(this.transform.forward * _shotSpeed + canoonBaseVelocity);
        _timeLastShot = Time.time;
    }
}
