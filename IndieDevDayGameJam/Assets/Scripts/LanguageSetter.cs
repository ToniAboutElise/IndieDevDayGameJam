using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void CheckLanguage()
    {
        FindObjectOfType<TextLanguage>().SetLanguage(language);
    }

    public void SetLanguageEnglish()
    {
        language = Language.English;
    }

    public void SetLanguageCastellano()
    {
        language = Language.Castellano;
    }
}
