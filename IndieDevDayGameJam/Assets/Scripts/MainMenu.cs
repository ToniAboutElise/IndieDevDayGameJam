using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public List<LevelButton> levelList = new List<LevelButton>();

    private string savedFile;

    private void Awake()
    {
        ResetLevelList();
    }

    protected void ResetLevelList()
    {
        savedFile = Application.persistentDataPath + "\\savedFile.txt";
        foreach (LevelButton lb in levelList)
        {
            lb.gameObject.SetActive(false);
        }

        CheckUnlockedLevels();
    }

    private void CheckUnlockedLevels()
    {
        Debug.Log(savedFile);
        if (!File.Exists(savedFile))
        {
            File.Create(savedFile);
        }

        StreamReader sr = new StreamReader(savedFile);

        string content = sr.ReadLine();
        int completedLevels = System.Int32.Parse(content);

        for(int i = 0; i < completedLevels; i++)
        {
            levelList[i].gameObject.SetActive(true);
        }

        sr.Close();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
