using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class CSVLoader
{
    private TextAsset csvFile;
    private char lineSeparator = '\n';
    private char surround = '"';
    private string[] fieldSeparator = { "\",\"" };

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localization");
    }

    public Dictionary<string, string> BuildDictionaryById(string languageId)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeparator);

        int attributeIndex = -1;

        string[] csvHeaders = lines[0].Split(fieldSeparator, StringSplitOptions.None);

        for (int x = 0; x < csvHeaders.Length; x++)
        {
            if (csvHeaders[x].Contains(languageId))
            {
                attributeIndex = x;
                break;
            }
        }

        if(attributeIndex == -1)
        {
            Debug.LogError("stringId '" + languageId + "' does not match with any keys in the .csv file");
            return null;
        }

        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int x = 1; x < lines.Length; x++)
        {
            string line = lines[x];
            string[] fields = CSVParser.Split(line);

            //Trim the field inital or final " "
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = fields[i].TrimStart(' ', surround);
                fields[i] = fields[i].TrimEnd('\r', surround);
            }

            //Add the values to the dictionary
            if(fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dict.ContainsKey(key)) continue;

                var value = fields[attributeIndex];

                dict.Add(key, value);

            }
        }

        return dict;
    }
}
