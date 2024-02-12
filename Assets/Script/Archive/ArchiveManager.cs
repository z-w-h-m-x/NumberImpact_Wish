using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveManager : MonobehaviourSingleton<ArchiveManager>
{
    
    private static bool isInit;

    public override void OnAwake()
    {
        Init();
    }

    public void Init()
    {
        ArchiveData.Data.archiveData = new ArchiveData.Archive();

        ArchiveData.Do.ReadArchive();

    }
}
    