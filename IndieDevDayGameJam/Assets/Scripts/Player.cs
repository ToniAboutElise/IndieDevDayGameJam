using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ShootingBall shootingBallPrefab;
    public Transform ballTransform;
    public Camera playerCamera;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    protected void GyroControl()
    {
        playerCamera.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
    }

    public void ShootBall()
    {
        ShootingBall instance = Instantiate(shootingBallPrefab);
        instance.transform.position = ballTransform.position;
        instance.transform.rotation = ballTransform.rotation;
        instance.rigidbody.velocity = instance.transform.forward*1200*Time.deltaTime;
    }

    public void ResetCamera()
    {
        playerCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Update()
    {
        GyroControl();
    }
}
