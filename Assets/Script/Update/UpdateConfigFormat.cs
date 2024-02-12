using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpdateConfigFormat
{
    [SerializeField]
    public List<string> UpdateURL = new(); 
    public string latestVersionFileName = "";

}