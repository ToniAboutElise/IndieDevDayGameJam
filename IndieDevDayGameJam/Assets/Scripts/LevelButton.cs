using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public GameObject textGameObject;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
