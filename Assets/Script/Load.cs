using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonobehaviourSingleton<Load>
{
    public Slider slider;
    public List<string> ablist = new();
    public List<AssetBundle> abPkg = new();
    public List<string> exclude = new();
    private List<int> excludeeI = new();
    private Dictionary<int, string> tips = new();
    public VersionFormat version = new();
    public string dataPath = Application.streamingAssetsPath;
    public override void OnAwake()
    {
        ExistLocalVersionAndDo();

        StreamReader st = new StreamReader(dataPath + "/version.json");
        version = JsonUtility.FromJson<VersionFormat>(st.ReadToEnd().Split("\n")[0]);
        st.Close();


        StartCoroutine("ReadList");

    }

    public bool ExistLocalVersionAndDo()
    {
        if (File.Exists(Application.persistentDataPath + "/canBeUse.txt"))
        {
            dataPath = Application.persistentDataPath;
            return true;
        }
        else
            return false;
    }


#if UNITY_WEBGL && !UNITY_EDITOR

    IEnumerable ReadList()
    {
        UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + "/ablist.txt");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(request.error);
        }else{

            string[] a = request.downloadHandler.text.ToString().Split("\n");
        
            ablist = new List<string>(a);

            StartCoroutine("LoadAB");
        }

    }

    IEnumerator LoadAB(){
        UnityWebRequest request;
        foreach (string _index in ablist)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle(Application.streamingAssetsPath + "/AB/webgl/" + _index);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(request.error);
            }else{

                int _iN = 0;

                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                if(abPkg.Contains(ab)){
                    _iN = abPkg.IndexOf(ab);
                    abPkg[_iN] = ab;
                }else{
                    abPkg.Add(ab);
                    _iN = abPkg.IndexOf(ab);
                }

                if (exclude.Contains(_index)) excludeeI.Add(_iN);

                Debug.Log("Loaded"+_index);
            }
        }

        for (int i=0; i < abPkg.Count; i++){

            if (excludeeI.Contains(i)) goto b;

            yield return abPkg[i].LoadAllAssets();

b:

            slider.value = (i+1) / abPkg.Count;
        }

        SceneManager.LoadScene("main");
    }
#else
    IEnumerator ReadList()
    {
        StreamReader st = new StreamReader(Application.streamingAssetsPath + "/ablist.txt");
        string[] a = st.ReadToEnd().Split("\n");
        st.Close();

        yield return new WaitForSeconds(0.1f);

        int iCount = 0;//计数，虽然可以用for的（（（）

        //load LoadAB config(ablist.txt)
        foreach (string i in a)
        {
            string[] temp = i.Split(" ");

            Debug.Log(temp[0]);

            ablist.Add(temp[0]);

            if (temp.Length == 3)
            {
                if (temp[1] == "N")
                    exclude.Add(temp[0]);
                tips[iCount] = temp[2];
            }
            else
            {
                if (temp[1] != "N")
                    tips[iCount] = temp[1];
                else
                    exclude.Add(temp[0]);
            }


            iCount++;
        }

        StartCoroutine("LoadAB");
    }
    IEnumerator LoadAB()
    {

        int iCount = 0; //in foreach

        foreach (string _index in ablist)
        {

            if (File.Exists(Application.streamingAssetsPath + "/AB/"+version+"/win/" + _index))
            {//AB存在就加载

                AssetBundle ab;
                int _iN = 0;

                if (tips.ContainsKey(iCount))
                    ChangeLoadText.instance.Change(tips[iCount]);


                yield return ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AB/"+version+"/win/" + _index);

                if (abPkg.Contains(ab))
                {
                    _iN = abPkg.IndexOf(ab);
                    abPkg[_iN] = ab;
                }
                else
                {
                    abPkg.Add(ab);
                    _iN = abPkg.IndexOf(ab);
                }

                if (exclude.Contains(_index)) excludeeI.Add(_iN);
            }
            else
            {
                ChangeLoadText.instance.Change("Error:Not Found Pivotal AB");
            }

            iCount++;
        }

        for (int i = 0; i < abPkg.Count; i++)
        {

            if (excludeeI.Contains(i)) goto b;

            abPkg[i].LoadAllAssets();
        b:
            slider.value = (i + 1) / abPkg.Count;
        }

        SceneManager.LoadScene("main");
    }
#endif
}
