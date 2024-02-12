using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace ArchiveData
{
    public static class Do
    {
        private static string code =  "archive";

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern string GetCookie(string str);

        [DllImport("__Internal")]
        private static extern void SetCookie(string _index, string _value);

#endif

        public static void ReadArchive()
        {
#if UNITY_WEBGL && !UNITY_EDITOR

            if (GetCookie(code) == "" ) SaveArchive();
            Data.archiveData = JsonUtility.FromJson<Archive>(GetCookie(code));

#else
            if (!File.Exists(Config.ArchiveDataPath))
                SaveArchive();
            StreamReader sr = new StreamReader(Config.ArchiveDataPath,System.Text.Encoding.UTF8);
        
            Data.archiveData = JsonUtility.FromJson<Archive>(sr.ReadToEnd().ToString());

            sr.Close();
#endif
        }

        public static void SaveArchive()
        {
#if UNITY_WEBGL && !UNITY_EDITOR

            SetCookie(code,JsonUtility.ToJson(Data.archiveData));

#else
            StreamWriter _sw = new StreamWriter(Config.ArchiveDataPath,false,System.Text.Encoding.UTF8);
        
            _sw.Write(JsonUtility.ToJson(Data.archiveData));

            _sw.Close();
#endif
        }

    }
}