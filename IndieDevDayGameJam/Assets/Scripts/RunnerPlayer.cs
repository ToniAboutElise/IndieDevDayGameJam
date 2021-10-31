using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
    public RunnerController controller;
    public BoxCollider boxCollider;
    public int lives = 3;

    public Rigidbody rb;

    public Animator vanAnimator;
    public Animator hitAnimator;

    public AudioSource claxonAudioSource;
    public AudioSource hitAudioSource;

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
            runnerBonus.bonusAnimation.Play("RunnerBonusGrab");
            controller.UpdateTimeLeft(other.GetComponent<RunnerBonus>().pointsValue);
            runnerBonus.BonusGrabbed();
        }
        else if (other.GetComponent<RunnerEntity>() && other.GetComponent<RunnerEntity>().entityType == RunnerEntity.EntityType.Enemy && playerState == PlayerState.Idle)
        {
            playerState = PlayerState.Invincible;
            StartCoroutine(HitByEnemy());
        }
    }

    protected void UpdateVanAnimator()
    {
        if(Input.GetAxis("Horizontal") > 0.15f)
        {
            vanAnimator.SetTrigger("left");
            vanAnimator.ResetTrigger("right");
            vanAnimator.ResetTrigger("idle");
        }
        else if (Input.GetAxis("Horizontal") < -0.15f)
        {
            vanAnimator.SetTrigger("right");
            vanAnimator.ResetTrigger("left");
            vanAnimator.ResetTrigger("idle");
        }
        else
        {
            vanAnimator.SetTrigger("idle");
            vanAnimator.ResetTrigger("left");
            vanAnimator.ResetTrigger("right");
        }
    }

    protected IEnumerator HitByEnemy()
    {
        hitAudioSource.Play();
        hitAnimator.SetTrigger("Hit");
        yield return null;
        hitAnimator.ResetTrigger("Hit");
        if (lives != 0)
        {
            controller.timeLeftBar.fillAmount -= 0.1f;
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
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3"))
        {
            claxonAudioSource.Play();
        }
    }

    private void Update()
    {
        CheckPlayClaxon();
        UpdateVanAnimator();
    }

}