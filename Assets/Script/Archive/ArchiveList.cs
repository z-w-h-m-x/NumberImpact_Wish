using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiveData
{
    
    [Serializable]
    public class Archive
    {
        public int min = 0;
        public int max = 0;

        public int anitype = 3;

        public int repetitionType = 0;

        public int language;//In LocalLanguugae_Manager
        public bool autoCheckUpdate = true;
        public VersionFormat version;

    }

}