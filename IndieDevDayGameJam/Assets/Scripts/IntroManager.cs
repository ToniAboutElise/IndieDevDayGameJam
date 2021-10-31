using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Text introText;
    public GameObject buttonStart;
    private string introString;
    public AudioSource beepAudioSource;

    private IEnumerator Start()
    {
        buttonStart.SetActive(false);
        introString = introText.text;
        introText.text = "";
        yield return new WaitForSeconds(1.5f);

        foreach(char c in introString)
        {
            float rand = Random.Range(0.4f, 1.8f);
            beepAudioSource.pitch = rand;
            if(beepAudioSource.isPlaying == false)
            { 
                beepAudioSource.Play();
            }
            yield return new WaitForSeconds(0.01f);
            introText.text += c;
        }
        yield return new WaitForSeconds(2);
        buttonStart.SetActive(true);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
