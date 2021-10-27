using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEntity : MonoBehaviour
{
    public Rigidbody rb;
    public float enemiesVelocity = 10;
    public bool autoVelocity = true;
    public float destroyRate = 8;

    public EntityType entityType;

    public enum EntityType
    {
        Enemy,
        Bonus,
        PuzzlePiece
    }

    private void Start()
    {
        StartCoroutine(DestroyEntity());
    }

    public IEnumerator DestroyEntity()
    {
        yield return new WaitForSeconds(destroyRate);
        Destroy(gameObject);
    }

    protected void AutomaticVelocity()
    {
        if(autoVelocity == true)
        { 
            rb.velocity = Vector3.forward * -enemiesVelocity;
        }
    }

    void Update()
    {
        AutomaticVelocity();
    }
}
