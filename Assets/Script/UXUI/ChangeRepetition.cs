using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRepetition : MonoBehaviour
{
    private int value=0;

    public string[] lastLimitRange = new string[39];
    public List<string> record = new();
    private int _point = -1;
    public int maxLimit = 10;

    public void DODO(int _value)
    {
        value = _value;
        ArchiveData.Data.archiveData.repetitionType = value;
        ArchiveData.Do.SaveArchive();

        switch(value)
        {
            case 0://none
                break;
            case 1://1-10
                maxLimit=10;
                string[] cache = new string[39];
                for (int i=0; i <= 9 && i < record.Count;i++)
                {
                    if(i > record.Count) break;
                    cache[9-i]=record[record.Count-i-1];
                }
                lastLimitRange = cache;
                _point=-1;
                break;
            case 2://1-30
                maxLimit = 30;
                cache = new string[39];
                for (int i=0; i <= 29 && i < record.Count;i++)
                {
                    if(i > record.Count) break;
                    cache[29-i]=record[record.Count-i-1];
                }
                lastLimitRange = cache;
                _point=-1;
                break;
            case 3://never repetition
                break;
            default:break;
        }

    }

    public bool Exists(string _value)
    {
        bool r = false;

        switch(value)
        {
            case 0://none
                break;
            case 1://1-10
                foreach(string a in lastLimitRange)
                    if (_value == a)
                        r = true;
                break;
            case 2://1-30
                foreach(string a in lastLimitRange)
                    if (_value == a)
                        r = true;
                break;
            case 3://never repetition
                if (record.Exists(t => t == _value))
                    r = true;
                break;
            default:break;
        }

        return r;
    }

    public void addNUM(string _value)
    {
        record.Add(_value);

        _point += 1;
        if(_point >= maxLimit) _point=0;

        lastLimitRange[_point] = _value;
    }

}
