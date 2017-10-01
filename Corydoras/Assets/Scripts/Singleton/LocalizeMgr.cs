﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using GlobalDefines;

public class LocalizeMgr : Singleton<LocalizeMgr> {
	protected LocalizeMgr() { }
	public string identify = "LocalizeMgr";

    private Dictionary<string, string> englishString = new Dictionary<string, string>();
    private Dictionary<string, string> japaneseString = new Dictionary<string, string>();
    private Dictionary<string, string> tchineseString = new Dictionary<string, string>();
    private Dictionary<string, string> schineseString = new Dictionary<string, string>();

    public string currentLang;

    public void Init() 
    {
		SystemLanguage localLanguage = PlatformMgr.GetSystemLanguage();
		if (localLanguage == SystemLanguage.Chinese) {
			currentLang = "SChinese";
		}
		else if (localLanguage == SystemLanguage.ChineseSimplified) {
			currentLang = "SChinese";
		}
		else if (localLanguage == SystemLanguage.ChineseTraditional) {
			currentLang = "TChinese";
		}
		else if (localLanguage == SystemLanguage.Japanese) {
			currentLang = "Japanese";
		}
		else {
			currentLang = "English";
		}
        
        LoadAllString();
    }

    void LoadAllString()
    {
        // Load English string
        englishString.Clear();
        TextAsset fileEnglish = Resources.Load<TextAsset>(FilePath.EnglishStringPath);
        if (fileEnglish == null) {
            Debug.LogError("[LocalizeMgr] LoadAllString: Load English string failed !" );
            return;
        }
        var jsonEnglish = JSON.Parse(fileEnglish.text) as JSONClass;
        foreach(var key in jsonEnglish.Keys) {
            englishString.Add(key, jsonEnglish[key]);
        }

		// Load Japanese string
        japaneseString.Clear();
        TextAsset fileJapanese = Resources.Load<TextAsset>(FilePath.JapaneseStringPath);
		if (fileJapanese == null)
		{
			Debug.LogError("[LocalizeMgr] LoadAllString: Load Japanese string failed !");
			return;
		}
		var jsonJapanese = JSON.Parse(fileJapanese.text) as JSONClass;
		foreach (var key in jsonJapanese.Keys)
		{
			japaneseString.Add(key, jsonJapanese[key]);
		}

		// Load TChinese string
        tchineseString.Clear();
        TextAsset fileTChinese = Resources.Load<TextAsset>(FilePath.TChineseStringPath);
		if (fileTChinese == null)
		{
			Debug.LogError("[LocalizeMgr] LoadAllString: Load TChinese string failed !");
			return;
		}
		var jsonTChinese = JSON.Parse(fileTChinese.text) as JSONClass;
		foreach (var key in jsonTChinese.Keys)
		{
			tchineseString.Add(key, jsonTChinese[key]);
		}

		// Load SChinese string
        schineseString.Clear();
        TextAsset fileSChinese = Resources.Load<TextAsset>(FilePath.SChineseStringPath);
		if (fileSChinese == null)
		{
			Debug.LogError("[LocalizeMgr] LoadAllString: Load SChinese String failed !");
			return;
		}
		var jsonSChinese = JSON.Parse(fileSChinese.text) as JSONClass;
		foreach (var key in jsonSChinese.Keys)
		{
			schineseString.Add(key, jsonSChinese[key]);
		}

    }

    public void SetLanguage(string lang)
    {
        currentLang = lang;
    }

    public string GetLanguage()
    {
        return currentLang;
    }

    public string GetString(string key)
    {
        switch(currentLang){
            case "English":
                return GetEnglishString(key);
            case "Japanese":
                return GetJapaneseString(key);
            case "TChinese":
                return GetTChineseString(key);
            case "SChinese":
                return GetSChineseString(key);
            default:
                return "Language Not Found";
        }
    }

    public string GetEnglishString(string key) 
    {
        if(englishString.ContainsKey(key)) {
            return englishString[key];
        }
        return "English String Not Found - " + key; 
    }

    public string GetJapaneseString(string key)
	{
        if(japaneseString.ContainsKey(key)) {
            return japaneseString[key];
        }
        return "Japanese String Not Found - " + key;
	}

	public string GetTChineseString(string key)
	{
        if(tchineseString.ContainsKey(key)) {
            return tchineseString[key];
        }
        return "TChinese String Not Found - " + key;
	}

	public string GetSChineseString(string key)
	{
        if(schineseString.ContainsKey(key)) {
            return schineseString[key];
        }
		return "SChinese String Not Found - " + key;
	}

}
