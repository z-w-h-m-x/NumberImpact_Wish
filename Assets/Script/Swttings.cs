using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class Swttings : MonoBehaviour {
    public Dropdown dp;
    public List<Dropdown.OptionData> nonone = new();
    public List<Dropdown.OptionData> onlyii = new();
    public int state=0;
    private void Awake() {
        if (File.Exists(Application.streamingAssetsPath+@"/"+ "nonone.txt" )) {dp.options = nonone;state=1;}//禁用无动画
        if (File.Exists(Application.streamingAssetsPath+@"/"+ "onlyii.txt" )) {dp.options = onlyii;state=3;}//只有Iron Ingot动画
        ArchiveData.Data.archiveData.anitype -= state;
        if (ArchiveData.Data.archiveData.anitype < 0 ) ArchiveData.Data.archiveData.anitype = 0;
    }
}