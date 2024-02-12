using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettingsManager : MonobehaviourSingleton<LanguageSettingsManager>
{
    public LocalLanguage_Manager localLanguageTextManager{get{return LocalLanguage_Manager.instance;}}

    public Dropdown language;

    public void ChangeLocalLanguage(int cao)
    {
        switch (cao)
        {
            case 0://English(en)
                localLanguageTextManager.defaultLanguage = "en";
                break;
            case 1://Chinese(zh_cn)
                localLanguageTextManager.defaultLanguage = "zh_cn";
                break;
        }
        localLanguageTextManager.ChangeLocalLanguage(localLanguageTextManager.defaultLanguage);
    }
}
