using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Buoy : MonoBehaviour
{
    [SerializeField] float _buoyancy = 10f;
    [SerializeField] List<BuoyPart> buoyParts;

    Rigidbody _rb;
    float _submergence;

    public float Submergence { get => _submergence; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        ApplyBuoyForce();                
    }


    void ApplyBuoyForce()
    {
        float totalVolume = 0f;
        foreach (BuoyPart bp in buoyParts)
        {
            totalVolume += Mathf.Pow(bp.Radius, 3);
        }

        float submergence = 0;
        foreach (BuoyPart bp in buoyParts)
        {
            Vector3 centerPosition = GetBuoyPartCenterPosition(bp);

            float waterSurfaceHeight = WaveManager.Instance.GetWaterHeightAtPoint(centerPosition);

            float volumePercentage = (Mathf.Pow(bp.Radius, 3) / totalVolume);

            float rateVolumeDisplaced = bp.GetRateVolumeDisplaced(centerPosition.y, waterSurfaceHeight);

            float buoyForce = _rb.mass *  -Physics.gravity.y * _buoyancy * rateVolumeDisplaced * volumePercentage;
            _rb.AddForceAtPosition(buoyForce * Vector3.up, centerPosition);

            submergence += rateVolumeDisplaced * volumePercentage;

            //print(bp.GetRateVolumeDisplaced(centerPosition.y, waterSurfaceHeight));
            //print(buoyForce);
        }

        _submergence = submergence;
    }


    Vector3 GetBuoyPartCenterPosition(BuoyPart buoyPart)
    {
        return transform.TransformPoint(buoyPart.Offset);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (BuoyPart bp in buoyParts)
        {
            Gizmos.DrawWireSphere(GetBuoyPartCenterPosition(bp), bp.Radius);
        }
    }

    [System.Serializable]
    class BuoyPart
    {
        public float Radius { get => _radius; set => _radius = value; }
        public Vector3 Offset { get => _offset; set => _offset = value; }

        [SerializeField] float _radius = 1f;
        [SerializeField] Vector3 _offset = Vector3.zero;

        public float GetRateVolumeDisplaced(float centerHeight, float waterSurfaceHeight)
        {
            float rateVolumeDisplaced;

            //Calculate distance from water surface to the lowest point on the sphere.
            float submergedHeight = waterSurfaceHeight - (centerHeight - _radius);

            //float volumeSphere = 4f / 3f * Mathf.PI * Mathf.Pow(_radius, 3);

            if (submergedHeight <= 0) // Above Water
            {
                rateVolumeDisplaced = 0f;
            }
            else if (submergedHeight >= 2 * _radius) // Completely submerged
            {
                rateVolumeDisplaced = 1f;
            }
            else // 0 <= submergedHeight <= 2R
            {
                rateVolumeDisplaced = Mathf.Sin(submergedHeight * Mathf.PI / (4 * _radius));
            }

            return rateVolumeDisplaced;
        }
    }
}
