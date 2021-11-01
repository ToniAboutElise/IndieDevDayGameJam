using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSceneSetter : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<LanguageSetter>().CheckLanguage();
    }
}
