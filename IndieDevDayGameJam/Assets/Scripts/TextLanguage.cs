using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguage : MonoBehaviour
{
    [TextArea]
    public string english;
    [TextArea]
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
