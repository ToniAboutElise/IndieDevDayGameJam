using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBonus : RunnerEntity
{
    public int pointsValue = 100;

    public Animation bonusAnimation;

    public virtual void BonusGrabbed()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
