using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSceneSetter : MonoBehaviour
{
    public TextLanguage[] textLanguages;

    private void Start()
    {
        FindObjectOfType<LanguageSetter>().CheckLanguage(textLanguages);
    }
}
