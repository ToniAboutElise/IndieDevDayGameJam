using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    public float xRotation;
    public float yRotation;
    public float zRotation;

    void Update()
    {
        transform.Rotate(xRotation * Time.deltaTime, yRotation * Time.deltaTime, zRotation * Time.deltaTime); 
    }
}
