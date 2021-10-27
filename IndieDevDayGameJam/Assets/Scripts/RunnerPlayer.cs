using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
    public RunnerController controller;
    public BoxCollider boxCollider;
    public int lives = 3;

    public PlayerState playerState = PlayerState.Idle;

    public enum PlayerState
    {
        Idle,
        Invincible
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RunnerBonus>())
        {
            RunnerBonus runnerBonus = other.GetComponent<RunnerBonus>();
            runnerBonus.autoVelocity = false;
            runnerBonus.enemiesVelocity = 0;
            Destroy(runnerBonus.GetComponent<Rigidbody>());
            runnerBonus.transform.SetParent(null);
            runnerBonus.GetComponent<Animation>().Play("RunnerBonusGrab");
            controller.UpdateCurrentPoints(other.GetComponent<RunnerBonus>().pointsValue);
            runnerBonus.BonusGrabbed();
        }
        else if (other.GetComponent<RunnerEntity>() && other.GetComponent<RunnerEntity>().entityType == RunnerEntity.EntityType.Enemy && playerState == PlayerState.Idle)
        {
            playerState = PlayerState.Invincible;
            StartCoroutine(HitByEnemy());
        }
    }

    protected IEnumerator HitByEnemy()
    {
        if(lives != 0)
        { 
            lives--;
            controller.livesText.text = lives.ToString();            
            yield return new WaitForSeconds(2);
        }
        else
        {
            controller.GameOver();
        }

        playerState = PlayerState.Idle;

    }
}
