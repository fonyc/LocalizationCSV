using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLocalisation : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField] private string id;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateText();
        LocalisationSystem.Instance.OnLanguageChanged += UpdateText;
    }

    public void ChangeLanguage(int languageEnumId)
    {
        LocalisationSystem.Instance.SwitchLanguage(languageEnumId);
    }

    private string GetTextFromLocal(string _id)
    {
        return LocalisationSystem.Instance.LanguageDict[_id];
    }

    private void UpdateText()
    {
        text.text = GetTextFromLocal(id);
    }

    private void OnDisable()
    {
        LocalisationSystem.Instance.OnLanguageChanged -= UpdateText;
    }
}
