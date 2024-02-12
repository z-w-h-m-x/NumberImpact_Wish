using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class LocalLanguage_Manager : MonobehaviourSingleton<LocalLanguage_Manager>
{
    public List<TextAsset> localLanguageFiles = new List<TextAsset>();
    public string defaultLanguage;
    public List<string> fileInSA = new List<string>();
    private List<LocalLanguage_Text_Influencer> textInfluencerList = new List<LocalLanguage_Text_Influencer>();
    private Dictionary<string, string> localLanguageData = new Dictionary<string, string>();
    private Dictionary<string,string> localLanguageDataList = new Dictionary<string, string>();

    public override void OnAwake()
    {
        foreach(TextAsset _point in localLanguageFiles)//对本地语言文件进行分析
        {
            string[] a = _point.text.ToString().Split("\r\n");
            localLanguageDataList.Add(a[0], _point.text);
        }

        foreach(string name in fileInSA)
        {
            StreamReader sr = new(Application.streamingAssetsPath+@"/"+name,System.Text.Encoding.UTF8);
            string[] a = sr.ReadToEnd().Split("\r\n");
            localLanguageDataList.Add(a[0],string.Join("\r\n",a));
            sr.Close();
        }

        ReadLocalLanguageFileData();
        
    }

    public void ChangeLocalLanguage(string key)
    {
        defaultLanguage = key;
        localLanguageData.Clear();
        localLanguageData = new Dictionary<string, string>();

        ReadLocalLanguageFileData();

        foreach(LocalLanguage_Text_Influencer _influencer in textInfluencerList)
        {
            ChangeInfluencerText(_influencer);
        }
    }

    public void Register(LocalLanguage_Text_Influencer textInfluencer)//注册文本（方便后续进行控制）
    {
        if (textInfluencerList.Contains(textInfluencer) == true)
        {
            return;
        }
        textInfluencerList.Add(textInfluencer);
        ChangeInfluencerText(textInfluencer);
    }

    public void Cancel(LocalLanguage_Text_Influencer textInfluencer)//取消注册（防止后续控制时物体不存在）
    {
        if (textInfluencerList.Contains(textInfluencer))
        {
            textInfluencerList.Remove(textInfluencer);
        }
    }

    private void ReadLocalLanguageFileData()//读取本地语言文件
    {
        if (defaultLanguage=="") goto PassReader;
        
        string[] lLocalLDataFile = localLanguageDataList[defaultLanguage].Split("\r\n");
        for (int _point = 1; _point < lLocalLDataFile.Length; _point++)
        {
            string[] data_ = lLocalLDataFile[_point].Split("==");
            localLanguageData.Add(data_[0], data_[1]);
        }
PassReader:;
    }

    private void ChangeInfluencerText(LocalLanguage_Text_Influencer textInfluencer)
    {
        if (localLanguageData.ContainsKey(textInfluencer.key)){
            textInfluencer.ChangeText(localLanguageData[textInfluencer.key].ToString());
        }else{
            textInfluencer.ChangeText(textInfluencer.key);
        }
    }
}
