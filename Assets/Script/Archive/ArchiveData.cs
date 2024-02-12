using UnityEngine;
namespace ArchiveData
{
    public static class Data
    {
        public static Archive archiveData;
    }
    public class Config
    {
        public static string UsersFilePath
        {
            get
            {
                return Application.persistentDataPath + @"/Users.users";
            }
            set { }
        }
        public static string ArchiveDataPath
        {
            get
            {
                return Application.persistentDataPath +
                @"/114514.data1"
                ;
            }
        }
    }
}