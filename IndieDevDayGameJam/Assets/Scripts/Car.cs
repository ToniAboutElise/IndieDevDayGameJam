using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : RunnerEntity
{
    public Renderer carRenderer;
    public Material[] material;
    private void Start()
    {
        enemiesVelocity *= 1.4f;
        SetColor();
    }

    protected void SetColor()
    {
        int rand = Random.Range(0, material.Length);
        carRenderer.material = material[rand];
    }

}
