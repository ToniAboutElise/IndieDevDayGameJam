using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSetter : MonoBehaviour
{
    public Language language = Language.English;

    public enum Language
    {
        English,
        Castellano
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CheckLanguage(TextLanguage[] tl)
    {
        foreach (TextLanguage t in tl)
            t.SetLanguage(language);
    }

    public void SetLanguageEnglish()
    {
        language = Language.English;
    }

    public void SetLanguageCastellano()
    {
        language = Language.Castellano;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
