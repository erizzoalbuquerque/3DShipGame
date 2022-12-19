using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    ShipController _shipController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _shipController.SetInput(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _shipController.Shoot(false);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            _shipController.Shoot(true);
    }
}
