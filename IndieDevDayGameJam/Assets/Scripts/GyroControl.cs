using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    void Start()
    {
        Input.gyro.enabled = true;
    }

    public void ResetCamera()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Update()
    {
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
    }
}
