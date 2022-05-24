using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem : MonoBehaviour
{
    private static LocalisationSystem instance;
    [SerializeField] private Languagues language = Languagues.English;
    private Dictionary<string, string> languageDict;

    public event Action OnLanguageChanged;

    public static LocalisationSystem Instance { get => instance; set => instance = value; }
    public Languagues Language { get => language; set => language = value; }
    public Dictionary<string, string> LanguageDict { get => languageDict; set => languageDict = value; }

    private void Awake()
    {
        //Singleton pattern

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(this);
            GetDictionaryLanguage();
        }
    }

    private void GetDictionaryLanguage()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        Dictionary<string, string> loadedDict = new Dictionary<string, string>();
        loadedDict = csvLoader.BuildDictionaryById(GetLanguageId());
        if (loadedDict != null) languageDict = loadedDict;
    }

    private string GetLanguageId()
    {
        switch (language)
        {
            case Languagues.English:
                return "en";
            case Languagues.Spanish:
                return "es";
            case Languagues.German:
                return "de";
        }
        Debug.LogWarning("There is no language with that key on .csv file");
        return "en";
    }

    public void SwitchLanguage(object newLanguage)
    {
        language = (Languagues)newLanguage;

        GetDictionaryLanguage();

        if(languageDict != null)OnLanguageChanged?.Invoke();
    }
}

public enum Languagues
{
    English = 0,
    Spanish = 1,
    German = 2
}
