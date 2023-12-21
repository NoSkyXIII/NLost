using Lean.Localization;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class TranslationSystem : MonoBehaviour
{
    [SerializeField] private LeanLocalization _localization;
    [SerializeField] private List<string> _languageNames;

    private int _currentIndex;
    private string _language;

    public void ChangeLanguage(int index)
    {
        if (_currentIndex + index >= _languageNames.Count)
        {
            _currentIndex = 0;

            SetLanguage(_languageNames[0]);
        }

        else if (_currentIndex + index < 0)
        {
            SetLanguage(_languageNames[_languageNames.Count - 1]);

            _currentIndex = _languageNames.Count - 1;
        }

        else
        {
            SetLanguage(_languageNames[_currentIndex + index]);

            _currentIndex += index;
        }
    }

    private void Start()
    {
        if (YandexGamesSdk.IsInitialized)
            DetectLanguage();

        {
            SetLanguage("en");
            _currentIndex = 0;
        }
    }

    private void DetectLanguage()
    {
        SetLanguage(_language = YandexGamesSdk.Environment.browser.lang);

        if (_language == "ru")
            _currentIndex = 1;

        else if( _language == "tr")
            _currentIndex = 2;
    }

    private void SetLanguage(string language)
    {
        _localization.SetCurrentLanguage(language);
    }
}
