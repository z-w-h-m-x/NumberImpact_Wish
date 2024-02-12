using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalLanguage_Text_Influencer : MonoBehaviour
{
    public string key;
    public LLTextComponentType thisLLTCT;
    private Text tText;
    private TMP_Text tTextTMP;
    private LocalLanguage_Manager localLanguageTextManager{get {return LocalLanguage_Manager.instance;}}

    private void Awake()
    {
        if (GetComponent<Text>() != null)
        {
            thisLLTCT = LLTextComponentType.Legacy;
            tText = this.GetComponent<Text>();
        }
        else if(GetComponent<TMP_Text>() != null)
        {
            thisLLTCT = LLTextComponentType.TMP;
            tTextTMP = this.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError("GameObject : " + this.gameObject + " : Not Found Component Which About Text");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        localLanguageTextManager.Register(this);
    }

    public void ChangeKey(string newKey)
    {
        key = newKey;
    }

    public void ChangeText(string text)
    {
        switch (thisLLTCT){
            case LLTextComponentType.Legacy:
                tText.text = text;
                break;
            case LLTextComponentType.TMP:
                tTextTMP.text = text;
                break;
        }

    }

    public void Refresh()
    {
        this.Start();
        localLanguageTextManager.Cancel(this);
        localLanguageTextManager.Register(this);
    }

    public void OnDestroy()
    {
        localLanguageTextManager.Cancel(this);
    }

    public string GetKey()
    {
        return key;
    }

    public enum LLTextComponentType
    {
        Legacy = 1,
        TMP = 2
    }

}
