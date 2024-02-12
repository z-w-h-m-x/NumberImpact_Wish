using System.Collections;
using System.IO;
using ArchiveData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.Rendering;

public class Update : MonobehaviourSingleton<Update>
{
    public VersionFormat version {get{return Load.instance.version;}}
    private UpdateConfigFormat config = new();
    public bool existNewVersion = false;

    public override void OnAwake()
    {
        base.OnAwake();

        if (ReadUpdateConfig())
        {
            PrintAError();

            StartCoroutine("TryUpdateConfig",DataPathConfig.URL_GithubRepository+"update.json");

            goto END;
        }

        if (Data.archiveData.autoCheckUpdate)
            StartCoroutine("CheckUpdate");
END:;
    }

    IEnumerator CheckUpdate()
    {
        if (config.UpdateURL.Count != 0)
        {
            foreach (string URL in config.UpdateURL)
            {
                WWW www = new WWW(URL+config.latestVersionFileName);
                yield return www;

                if (www.isDone)
                {
                    VersionFormat lasterVersion = JsonUtility.FromJson<VersionFormat>(www.bytes.ToSafeString());
                    if (lasterVersion == version)
                        break;
                    else
                        if (lasterVersion.major > version.major
                            || lasterVersion.minor > version.minor
                            || lasterVersion.patch > version.patch
                            || lasterVersion.day > version.day
                        )
                            StartCoroutine("StartUpdate",URL);
                        else
                            break;
                }
                else
                    break;
            }
        }
        else
            yield return null;
    }

    IEnumerator StartUpdate(string URL)
    {
        WWW www = new WWW(URL+DataPathConfig.update_commandListFileName);
        yield return www;

        yield return null;
    }

    public bool ReadUpdateConfig()
    {
        bool result = false;

        if (File.Exists(DataPathConfig.UpdateConfig))
        {
            StreamReader sr = new StreamReader(DataPathConfig.UpdateConfig,System.Text.Encoding.UTF8);
            config = JsonUtility.FromJson<UpdateConfigFormat>(sr.ReadToEnd().ToString());
            sr.Close();
        }

        if (config.UpdateURL.Count != 0) result = true;//只有有值时才通过

        return result;
    }

    IEnumerator TryUpdateConfig(string URL)
    {
        WWW www = new WWW(URL);
        yield return www;
        if (www.isDone)
        {
            StreamWriter sw = new StreamWriter(DataPathConfig.UpdateConfig,false,System.Text.Encoding.UTF8);
            sw.Write(www.bytes);
            sw.Close();
        }
        if (!ReadUpdateConfig())
            PrintAError();
    }

    public void PrintAError(){}

    public void WriteFile(byte[] bytes,string path,string filename)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        Stream stream = File.Create(Path.Combine(path,filename));
        stream.Write(bytes,0,bytes.Length);
        stream.Close();
        stream.Dispose();
    }
}