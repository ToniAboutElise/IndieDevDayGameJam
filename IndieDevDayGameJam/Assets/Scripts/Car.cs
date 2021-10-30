using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : RunnerEntity
{
    public Renderer carRenderer;
    public Material[] material;
    public ParticleSystem frontParticles;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" && other.GetComponent<RunnerEntity>())
        {
            Destroy(other.gameObject);
            frontParticles.Play();
        }
    }

}
