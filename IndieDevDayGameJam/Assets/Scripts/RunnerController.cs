using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunnerController : MonoBehaviour
{
    public RunnerPlayer runnerPlayer;
    public bool leftButtonPressed = false;
    public bool rightButtonPressed = false;
    public bool isRotating = false;
    public GameObject gameOverPanel;
    public GameObject cylinder;
    public GameObject rushModeGameObject;
    public Button leftButton;
    public Button rightButton;
    public float playerRotationVelocity = 2.5f;
    public float spawnCooldown = 1;
    public int spawnRate = 4;
    public int currentPoints;
    public Text pointsText;
    public Text livesText;
    public TextureMapFakeVelocity textureMapFakeVelocity;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        rushModeGameObject.SetActive(false);
        StartCoroutine(AddGameVelocity());
        livesText.text = runnerPlayer.lives.ToString();
    }

    protected IEnumerator AddGameVelocity()
    {
        yield return new WaitForSeconds(20);
        playerRotationVelocity += 0.1f;
        if(spawnCooldown > 0.6f)
        { 
            spawnCooldown -= 0.1f;
            StartCoroutine(AddGameVelocity());
        }
        else if (spawnCooldown > 0.5f)
        {
            spawnCooldown -= 0.2f;
            spawnRate = 6;
            rushModeGameObject.SetActive(true);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    protected void Rotation()
    {
        if(isRotating == false)
        { 
            if (Input.GetKey(KeyCode.LeftArrow) || leftButtonPressed == true)
            {
                cylinder.transform.Rotate(new Vector3(0,0,-playerRotationVelocity));
                //runnerPlayer.transform.localPosition -= new Vector3(0.01f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || rightButtonPressed == true)
            {
                cylinder.transform.Rotate(new Vector3(0, 0, playerRotationVelocity));
                //runnerPlayer.transform.localPosition += new Vector3(0.01f, 0, 0);
            }
        }
    }

    public void UpdateCurrentPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
        pointsText.text = currentPoints.ToString();
    }

    public void TouchLeftPress()
    {
        leftButtonPressed = true;
    }

    public void TouchLeftRelease()
    {
        leftButtonPressed = false;
    }

    public void TouchRightPress()
    {
        rightButtonPressed = true;
    }

    public void TouchRightRelease()
    {
        rightButtonPressed = false;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        Rotation();
    }
}

public class DifficultyLevel : MonoBehaviour
{
    public int freeSpaces;
    public float enemiesVelocity;
    public float playerRotationVelocity;
}