using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSceneSetter : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<LanguageSetter>().CheckLanguage();
    }
}
