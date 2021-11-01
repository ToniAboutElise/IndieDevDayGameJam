using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguage : MonoBehaviour
{
    public string english;
    public string castellano;

    public void SetLanguage(LanguageSetter.Language language)
    {
        switch (language)
        {
            case LanguageSetter.Language.English:
                GetComponent<Text>().text = english;
                break;
            case LanguageSetter.Language.Castellano:
                GetComponent<Text>().text = castellano;
                break;
        }
    }
}
