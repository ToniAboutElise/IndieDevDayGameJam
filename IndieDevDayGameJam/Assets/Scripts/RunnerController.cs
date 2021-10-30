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
    public int currentTime;
    public Image timeLeftBar;
    public Text pointsText;
    public Text livesText;
    public TextureMapFakeVelocity textureMapFakeVelocity;

    public List<Image> trails = new List<Image>();
    public List<Image> checkPoint = new List<Image>();
    public int currentTrail = 0;

    private bool canSubstractTime = true;
    private bool canAdvanceInTrail = true;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        rushModeGameObject.SetActive(false);
        StartCoroutine(AddGameVelocity());
        livesText.text = runnerPlayer.lives.ToString();
        timeLeftBar.fillAmount = 1;
    }

    protected IEnumerator AdvanceInTrail()
    {
        canAdvanceInTrail = false;
        trails[currentTrail].fillAmount += 0.001f;
        yield return new WaitForSeconds(0.0001f);
        if (trails[currentTrail].fillAmount == 1)
        {
            if(currentTrail < trails.Count-1)
            {
                //checkPoint[currentTrail].gameObject.GetComponent<Animation>().Play();
                currentTrail++;
            }
            else
            {
                Win();
            }
        }
        canAdvanceInTrail = true;
    }

    protected void Win()
    {

    }

    protected IEnumerator AddGameVelocity()
    {
        yield return new WaitForSeconds(20);

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


        if(Input.GetAxis("Horizontal") == 0)
        { 
            if (Input.GetKey(KeyCode.LeftArrow) || leftButtonPressed == true)
            {
                //cylinder.transform.Rotate(new Vector3(0,0,-playerRotationVelocity));
                //runnerPlayer.transform.localPosition -= new Vector3(0.005f, 0, 0);
                runnerPlayer.rb.AddForce(new Vector3(-1.7f, 0, 0), ForceMode.Force);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || rightButtonPressed == true)
            {
                //cylinder.transform.Rotate(new Vector3(0, 0, playerRotationVelocity));
                //runnerPlayer.transform.localPosition += new Vector3(0.005f, 0, 0);
                runnerPlayer.rb.AddForce(new Vector3(1.7f, 0, 0), ForceMode.Force);
            }
        }
        else
        {
            runnerPlayer.rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * 1.7f, 0, 0), ForceMode.Force);
        }
    }

    public void UpdateTimeLeft(int pointsToAdd)
    {
        timeLeftBar.fillAmount += 0.1f;
    }

    protected IEnumerator TimeSubstraction()
    {
        canSubstractTime = false;
        yield return new WaitForSeconds(0.0001f);
        timeLeftBar.fillAmount -= 0.0005f;
        if (timeLeftBar.fillAmount == 0)
        {
            GameOver();
        }
        canSubstractTime = true;
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

    private void FixedUpdate()
    {
        Rotation();

        if(canSubstractTime == true)
        StartCoroutine(TimeSubstraction());

        if(canAdvanceInTrail == true)
        StartCoroutine(AdvanceInTrail());
    }
}

public class DifficultyLevel : MonoBehaviour
{
    public int freeSpaces;
    public float enemiesVelocity;
    public float playerRotationVelocity;
}