using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MainMenu : MonoBehaviour
{
    public List<LevelButton> levelList = new List<LevelButton>();

    private string savedFile;

    private void Awake()
    {
        Cursor.visible = true;
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
        if (!File.Exists(savedFile))
        { 
            using (FileStream fs = File.Create(savedFile))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("0");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
        Debug.Log(savedFile);
        using (StreamReader sr = File.OpenText(savedFile))
        {
            string content = sr.ReadLine();
            Debug.Log("AAA " + content);
            //string content = sr.ReadLine();
            
            int completedLevels = System.Int32.Parse(content);
            Debug.Log(completedLevels);
            for (int i = 0; i < completedLevels+1; i++)
            {
                levelList[i].gameObject.SetActive(true);
                if(i != completedLevels)
                {
                    levelList[i].completedIndicator.SetActive(true);
                }
                else
                {
                    levelList[i].uncompletedIndicator.SetActive(true);
                }
            }
            sr.Close();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
                string savedFile = Application.persistentDataPath + "\\savedFile.txt";

                using (StreamWriter sw = new StreamWriter(savedFile, true))
                {
                    sw.Flush();
                    sw.WriteLine("3");
                    sw.Close();
                }
            }
    }
}
