using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    public Rigidbody rigidbody;

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    protected IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Chicken>())
        {
            collision.gameObject.GetComponent<Chicken>().ChickenHit();
            Destroy(gameObject);
        }
    }

}
