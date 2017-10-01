using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PlatformMgr {

#if UNITY_IOS
    [DllImport("__Internal")]  
    private static extern string CurIOSLang();
#endif
	
    public static SystemLanguage GetSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;

#if UNITY_EDITOR || UNITY_STANDALONE

#elif UNITY_IOS || UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            if (lang == SystemLanguage.Chinese) {
                string name = CurIOSLang();
                if (name.StartsWith("zh-Hans")) {
                    return SystemLanguage.ChineseSimplified;
                }  
            return SystemLanguage.ChineseTraditional;
            }
        }
        
#elif UNITY_ANDROID
#endif

        return lang;
    }
}
