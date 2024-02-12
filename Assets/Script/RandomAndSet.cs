using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RandomAndSet : MonoBehaviour
{
    public Text target;
    public Dropdown dd;
    public Dropdown rd;
    public InputField ifmin;
    public InputField ifmax;
    public ChangeRepetition changeRepetition;

    private int min = 0;
    private int max = 0;

    private List<string> ECN = new();

    private void Awake() {
        min = ArchiveData.Data.archiveData.min;
        ifmin.text = min.ToString();
        max = ArchiveData.Data.archiveData.max;
        ifmax.text = max.ToString();
        dd.value = ArchiveData.Data.archiveData.anitype;
        rd.value = ArchiveData.Data.archiveData.repetitionType;

        if (File.Exists(Application.streamingAssetsPath+@"/exclude.txt")){
            StreamReader _sr = new StreamReader(Application.streamingAssetsPath+@"/exclude.txt",System.Text.Encoding.UTF8);
            ECN = new(_sr.ReadToEnd().Split(" "));
        }
    }


    public void DODO()
    {
        int i = Random.Range(min,max+1);
        int r = 0;
        bool rs = true;
        while(ECN.Exists(t => t == i.ToString()) || changeRepetition.Exists(i.ToString()))
        {
            i = Random.Range(min,max+1);
            r++;
            if(r>=300){
                rs=false;
                break;
            }
        }
        if (rs){ target.text = i.ToString(); changeRepetition.addNUM(i.ToString());}
        else { target.text = "ERROR\n超出程序单次生成次数限制\n请降低重复次数限制或提高生成范围！" ;}
    } 

    public void setMIN(string _value)
    {
        int.TryParse(_value,out min);
        ArchiveData.Data.archiveData.min = min;
        ArchiveData.Do.SaveArchive();
    }
    public void setMAX(string _value)
    {
        int.TryParse(_value,out max);
        ArchiveData.Data.archiveData.max = max;
        ArchiveData.Do.SaveArchive();
    }

}
