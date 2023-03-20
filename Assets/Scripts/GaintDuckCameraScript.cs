using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaintDuckCameraScript : MonoBehaviour
{
    [SerializeField] Transform _ship;
    [SerializeField] Cinemachine.CinemachineVirtualCamera _defaultCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera _giantDuckCamera;
    [SerializeField] float _distanceToActivate = 80f;

    bool _activated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((_ship.position - this.transform.position).magnitude < _distanceToActivate)
            Activate();
        else
            Deactivate();
    }

    void Activate()
    {
        if (_activated)
            return;

        _defaultCamera.gameObject.SetActive(false);
        _giantDuckCamera.gameObject.SetActive(true);

        _activated = true;
    }

    void Deactivate()
    {
        if(!_activated)
            return;

        _defaultCamera.gameObject.SetActive(true);
        _giantDuckCamera.gameObject.SetActive(false);

        _activated = false;
    }
}
