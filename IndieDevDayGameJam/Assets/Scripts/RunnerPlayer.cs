using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
    public RunnerController controller;
    public BoxCollider boxCollider;
    public int lives = 3;

    public Rigidbody rb;

    public AudioSource claxonAudioSource;

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
            controller.UpdateTimeLeft(other.GetComponent<RunnerBonus>().pointsValue);
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
            controller.timeLeftBar.fillAmount -= 0.15f;
            yield return new WaitForSeconds(2);
        }
        else
        {
            controller.GameOver();
        }

        playerState = PlayerState.Idle;

    }

    protected void CheckPlayClaxon()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            claxonAudioSource.Play();
        }
    }

    private void Update()
    {
        CheckPlayClaxon();
    }

}
