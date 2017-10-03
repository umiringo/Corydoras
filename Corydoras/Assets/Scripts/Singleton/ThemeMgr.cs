using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using GlobalDefines;

public class ThemeMgr : Singleton<ThemeMgr>
{
    protected ThemeMgr() { }
    public string identify = "ThemeMgr";

    private Dictionary<int, List<JSONClass>> themeHash = new Dictionary<int, List<JSONClass>>();

    public void Init()
    {
        TextAsset themeFile = Resources.Load<TextAsset>(FilePath.ThemePath);
        if(themeFile == null)
        {
            Debug.LogError("[ThemeMgr] ThemeFile not found!");
            return;
        }
        themeHash.Clear();
        JSONClass themeJson = JSON.Parse(themeFile.text) as JSONClass;
        JSONArray themeArray = themeJson["Theme"].AsArray;
        for (int i = 0; i < themeArray.Count; ++i)
        {
            JSONClass themeData = themeArray[i] as JSONClass;
            int season = themeData["Season"].AsInt;
            if (themeHash.ContainsKey(season) == false)
            {
                themeHash[season] = new List<JSONClass>();
            }
            themeHash[season].Add(themeData);
        }
    }

    public Color GetBgColor(int season, int index)
    {
        if (themeHash.ContainsKey(season) == false)
        {
            Debug.LogError("[ThemeMgr] No such season, season = " + season);
            return Color.black;
        }

        if(themeHash[season].Count <= index) 
        {
            Debug.LogError("[ThemeMgr] Season " + season + " has no index " + index);
            return Color.black;
        }
        JSONClass theme = themeHash[season][index];
        string clr = theme["Bg"];
        Color ret;
        ColorUtility.TryParseHtmlString("#" + clr, out ret);

        return ret;
    }

	public Color GetOutColor(int season, int index)
	{
		if (themeHash.ContainsKey(season) == false) {
			Debug.LogError("[ThemeMgr] No such season, season = " + season);
            return Color.blue;
		}

		if (themeHash[season].Count <= index) {
			Debug.LogError("[ThemeMgr] Season " + season + " has no index " + index);
			return Color.blue;
		}
		JSONClass theme = themeHash[season][index];
        string clr = theme["Out"];
		Color ret;
		ColorUtility.TryParseHtmlString("#" + clr, out ret);

		return ret;
	}

	public Color GetInnerColor(int season, int index)
	{
		if (themeHash.ContainsKey(season) == false) {
			Debug.LogError("[ThemeMgr] No such season, season = " + season);
            return Color.red;
		}

		if (themeHash[season].Count <= index) {
			Debug.LogError("[ThemeMgr] Season " + season + " has no index " + index);
            return Color.red;
		}
		JSONClass theme = themeHash[season][index];
		string clr = theme["Inner"];
		Color ret;
		ColorUtility.TryParseHtmlString("#" + clr, out ret);

		return ret;
	}

	public Color GetTextColor(int season, int index)
	{
		if (themeHash.ContainsKey(season) == false) {
			Debug.LogError("[ThemeMgr] No such season, season = " + season);
            return Color.yellow;
		}

		if (themeHash[season].Count <= index) {
			Debug.LogError("[ThemeMgr] Season " + season + " has no index " + index);
            return Color.yellow;
		}
		JSONClass theme = themeHash[season][index];
		string clr = theme["Text"];
		Color ret;
		ColorUtility.TryParseHtmlString("#" + clr, out ret);

		return ret;
	}
}
